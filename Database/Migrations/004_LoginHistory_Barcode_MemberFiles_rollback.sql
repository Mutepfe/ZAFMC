/*
    Rollback for migration 004 (login history, PDF417 barcode, member photo / ID bytes).

    !! REVERT THE PAGE CODE FIRST !!
    Revert Congregation.aspx(.cs), Default.aspx.cs, Site.Master, UserLoginHistory.cs and
    CongregationBarcode.cs to the pre-004 versions and redeploy BEFORE running this script:
    the 004 page code calls dbo.AddUserLoginHistory / dbo.SetCongregationBarcode, passes the new
    photo / ID parameters and reads dbo.UserLoginHistory.
    Run this BEFORE 003_WidenDropdownColumns_RowCounts_rollback.sql.

    What it does:
    - Restores [dbo].[AddNewCongregation] and [dbo].[UpdateCongregation] to their exact pre-004 (003)
      definitions, and drops [dbo].[SetCongregationBarcode] and [dbo].[AddUserLoginHistory].
      Barcodes already stored in CongBarcode stay (that column existed before 004).
    - Drops the CongPhotoData / CongPhotoContentType / CongPassIDData / CongPassIDContentType /
      CongPassIDFileName columns, but REFUSES (THROW 50007) when any member has stored photo / ID bytes,
      instead of deleting data.
    - Drops [dbo].[UserLoginHistory], but REFUSES (THROW 50008) when it holds rows.
      The procs are restored first, so they are back even when a drop is refused.
    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 004_LoginHistory_Barcode_MemberFiles_rollback.sql
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
SET NOCOUNT ON;
GO
ALTER PROCEDURE [dbo].[AddNewCongregation]
@CongTitle as varchar(5) ,
@CongName as varchar(15) ,
@CongSurname as varchar(20),
@CongDOB as date,
@CongGender as varchar(6),
@CongPassportID as nvarchar(15),
@CongStatus as varchar(10),
@CongProfession as varchar(20),
@CongKin as varchar(20),
@CongKinContact as nvarchar(20),
@CongCell as nvarchar(20),
@CongAddress as nvarchar(50),
@CongEmail as nvarchar(50),
@CongPosition as varchar(50),
@CongDateAppointed as date,
@CongManagerial as varchar(50),
@CongDateElected as date,
@CongProvince as varchar(30),
@CongDistrict as varchar(30),
@CongZone as varchar(30),
@CongSection as varchar(30),
@CongSnrLeader as varchar(20) ,
@CongViceLeader as varchar(20),
@CongPhoto as nvarchar(300),
@CongPassID as nvarchar(300),
@CongregationID as uniqueidentifier,
@CongFingerprint as image,
@CongBarcode as image,
@CongMembershipNumber as nvarchar(20) = NULL OUTPUT -- any incoming value is ignored; returns the generated number

AS 
BEGIN
 SET NOCOUNT ON
 SET XACT_ABORT ON
 SET @CongregationID=NEWID()
 SET @CongMembershipNumber = NULL
 BEGIN TRY
  BEGIN TRAN
  EXEC [dbo].[IssueMembershipNumber] @CongPassportID = @CongPassportID, @MembershipNumber = @CongMembershipNumber OUTPUT
INSERT INTO [dbo].[Congregation]
         ([CongTitle],[CongName],[CongSurname],[CongDOB],[CongGender],[CongPassportID],[CongStatus],[CongProfession],[CongKin],[CongKinContact],[CongCell],[CongAddress],[CongEmail],[CongPosition],[CongDateAppointed],[CongManagerial],[CongDateElected],[CongProvince], [CongDistrict],[CongZone], [CongSection], [CongSnrLeader], [CongViceLeader],[CongPhoto],[CongPassID], [CongregationID],[CongFingerprint],[CongBarcode],[CongMembershipNumber] )     
 VALUES (@CongTitle,@CongName,@CongSurname,@CongDOB,@CongGender,@CongPassportID,@CongStatus,@CongProfession,@CongKin,@CongKinContact,@CongCell,@CongAddress,@CongEmail,@CongPosition,@CongDateAppointed,@CongManagerial,@CongDateElected,@CongProvince,@CongDistrict,@CongZone,@CongSection,@CongSnrLeader,@CongViceLeader,@CongPhoto,@CongPassID,@CongregationID,@CongFingerprint,@CongBarcode,@CongMembershipNumber )     
  COMMIT
 END TRY
 BEGIN CATCH
  -- A failed insert (e.g. duplicate Passport / ID) also rolls back the registry row.
  IF @@TRANCOUNT > 0 ROLLBACK
  SET @CongMembershipNumber = NULL;
  THROW;
 END CATCH
END
GO
ALTER PROCEDURE [dbo].[UpdateCongregation]
@CongTitle as varchar(5) ,
@CongName as varchar(15) ,
@CongSurname as varchar(20),
@CongDOB as date,
@CongGender as varchar(6),
@CongPassportID as nvarchar(15),
@CongStatus as varchar(10),
@CongProfession as varchar(20),
@CongKin as varchar(20),
@CongKinContact as nvarchar(20),
@CongCell as nvarchar(20),
@CongAddress as nvarchar(50),
@CongEmail as nvarchar(50),
@CongPosition as varchar(50),
@CongDateAppointed as date,
@CongManagerial as varchar(50),
@CongDateElected as date,
@CongProvince as varchar(30),
@CongDistrict as varchar(30),
@CongZone as varchar(30),
@CongSection as varchar(30),
@CongSnrLeader as varchar(20) ,
@CongViceLeader as varchar(20),
@CongPhoto as nvarchar(300),
@CongPassID as nvarchar(300),
@CongregationID as uniqueidentifier,
@CongFingerprint as image,
@CongBarcode as image,
@CongMembershipNumber as nvarchar(20) = NULL, -- ignored: the membership number is permanent
@RowsAffected as int = NULL OUTPUT -- number of updated rows (0 = no member with that Passport / ID)
AS 
BEGIN
 SET NOCOUNT ON
 SET @CongregationID=NEWID()
 -- Photo, Passport / ID image, fingerprint and barcode are only replaced when a new, non-empty
 -- value is supplied; an empty / NULL value keeps what is stored.
 UPDATE [dbo].[Congregation]
 SET 
[CongTitle]=@CongTitle,
[CongName]=@CongName,
[CongSurname]=@CongSurname,
[CongDOB]=@CongDOB,
[CongGender]=@CongGender,
[CongPassportID]=@CongPassportID ,
[CongStatus]=@CongStatus,
[CongProfession]=@CongProfession,
[CongKin]=@CongKin,
[CongKinContact]=@CongKinContact,
[CongCell]=@CongCell,
[CongAddress]=@CongAddress,
[CongEmail]=@CongEmail,
[CongPosition]=@CongPosition,
[CongDateAppointed]=@CongDateAppointed,
[CongManagerial]=@CongManagerial,
[CongDateElected]=@CongDateElected,
[CongProvince]=@CongProvince,
[CongDistrict]=@CongDistrict,
[CongZone]=@CongZone,
[CongSection]=@CongSection,
[CongSnrLeader]=@CongSnrLeader,
[CongViceLeader]=@CongViceLeader,
[CongPhoto]=COALESCE(NULLIF(@CongPhoto, N''), [CongPhoto]),
[CongPassID]=COALESCE(NULLIF(@CongPassID, N''), [CongPassID]),
[CongregationID]=@CongregationID,
[CongFingerprint]=CASE WHEN @CongFingerprint IS NULL OR DATALENGTH(@CongFingerprint) = 0 THEN [CongFingerprint] ELSE @CongFingerprint END,
[CongBarcode]=CASE WHEN @CongBarcode IS NULL OR DATALENGTH(@CongBarcode) = 0 THEN [CongBarcode] ELSE @CongBarcode END
 WHERE [CongPassportID] = @CongPassportID
 SET @RowsAffected = @@ROWCOUNT
END
GO
DROP PROCEDURE IF EXISTS [dbo].[SetCongregationBarcode];
GO
DROP PROCEDURE IF EXISTS [dbo].[AddUserLoginHistory];
GO
-- Dynamic SQL: the columns may already be gone (second run), and a static reference would not compile.
IF COL_LENGTH('dbo.Congregation', 'CongPhotoData') IS NOT NULL AND COL_LENGTH('dbo.Congregation', 'CongPassIDData') IS NOT NULL
BEGIN
    DECLARE @hasBytes bit = 0;
    EXEC sp_executesql N'IF EXISTS (SELECT 1 FROM [dbo].[Congregation] WHERE [CongPhotoData] IS NOT NULL OR [CongPassIDData] IS NOT NULL) SET @h = 1;',
         N'@h bit OUTPUT', @h = @hasBytes OUTPUT;
    IF @hasBytes = 1
        THROW 50007, N'Members have stored photo / ID bytes; dropping the columns would delete them. Columns left in place.', 1;
END
GO
IF COL_LENGTH('dbo.Congregation', 'CongPhotoData') IS NOT NULL ALTER TABLE [dbo].[Congregation] DROP COLUMN [CongPhotoData];
IF COL_LENGTH('dbo.Congregation', 'CongPhotoContentType') IS NOT NULL ALTER TABLE [dbo].[Congregation] DROP COLUMN [CongPhotoContentType];
IF COL_LENGTH('dbo.Congregation', 'CongPassIDData') IS NOT NULL ALTER TABLE [dbo].[Congregation] DROP COLUMN [CongPassIDData];
IF COL_LENGTH('dbo.Congregation', 'CongPassIDContentType') IS NOT NULL ALTER TABLE [dbo].[Congregation] DROP COLUMN [CongPassIDContentType];
IF COL_LENGTH('dbo.Congregation', 'CongPassIDFileName') IS NOT NULL ALTER TABLE [dbo].[Congregation] DROP COLUMN [CongPassIDFileName];
GO
IF OBJECT_ID('dbo.UserLoginHistory', 'U') IS NOT NULL
    IF EXISTS (SELECT 1 FROM [dbo].[UserLoginHistory])
        THROW 50008, N'dbo.UserLoginHistory holds login history rows; dropping it would delete them. Table left in place.', 1;
GO
DROP TABLE IF EXISTS [dbo].[UserLoginHistory];
GO
