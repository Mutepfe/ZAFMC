/*
    Rollback for migration 003 (wider dropdown columns, safe Update, affected-row counts).

    !! REVERT THE PAGE CODE FIRST !!
    Revert Congregation.aspx, Congregation.aspx.cs, Congregation.aspx.designer.cs to the pre-003
    (v2) versions and redeploy BEFORE running this script. The v3 page passes @RowsAffected,
    which the restored procs do not declare, so every Update and Delete fails with
    "too many arguments specified" until the page code is reverted.
    Run this BEFORE 002_MembershipNumberRegistry_rollback.sql.
    Repaired dropdown values (if any) are NOT un-repaired.

    What it does:
    - Restores [dbo].[AddNewCongregation], [dbo].[UpdateCongregation] and [dbo].[DeleteCongregation]
      to their exact pre-003 definitions (UpdateCongregation then overwrites photo, Passport / ID,
      fingerprint and barcode again on every update).
    - Narrows [CongPosition] back to varchar(15) and [CongManagerial] back to varchar(20), but
      REFUSES (THROW 50005) when any stored value is longer, instead of truncating data. The procs
      are restored first, so they are back even when the narrowing is refused.
    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 003_WidenDropdownColumns_RowCounts_rollback.sql
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
@CongPosition as varchar(15),
@CongDateAppointed as date,
@CongManagerial as varchar(20),
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
@CongPosition as varchar(15),
@CongDateAppointed as date,
@CongManagerial as varchar(20),
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
@CongMembershipNumber as nvarchar(20) = NULL -- ignored: the membership number is permanent
AS 
BEGIN
 SET NOCOUNT ON
 SET @CongregationID=NEWID()
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
[CongPhoto]=@CongPhoto,
[CongPassID]=@CongPassID,
[CongregationID]=@CongregationID,
[CongFingerprint]=@CongFingerprint,
[CongBarcode]=@CongBarcode
 WHERE [CongPassportID] = @CongPassportID
END
GO
ALTER PROCEDURE [dbo].[DeleteCongregation]
@DelCONG as varchar(15)
AS 
BEGIN
    DELETE FROM [dbo].Congregation WHERE [CongPassportID]= @DelCONG
END
GO
-- Narrow the columns again only when no stored value would be truncated.
IF EXISTS (SELECT 1 FROM [dbo].[Congregation] WHERE LEN([CongPosition]) > 15 OR LEN([CongManagerial]) > 20)
    THROW 50005, N'Values longer than varchar(15)/varchar(20) exist; narrowing would truncate data. Columns left at varchar(50).', 1;
GO
IF COL_LENGTH('dbo.Congregation', 'CongPosition') > 15
   AND NOT EXISTS (SELECT 1 FROM [dbo].[Congregation] WHERE LEN([CongPosition]) > 15)
    ALTER TABLE [dbo].[Congregation] ALTER COLUMN [CongPosition] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL;
GO
IF COL_LENGTH('dbo.Congregation', 'CongManagerial') > 20
   AND NOT EXISTS (SELECT 1 FROM [dbo].[Congregation] WHERE LEN([CongManagerial]) > 20)
    ALTER TABLE [dbo].[Congregation] ALTER COLUMN [CongManagerial] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL;
GO
