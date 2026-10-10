/*
    Migration 004: login history, PDF417 barcode, member photo / ID document stored as bytes.
    - Creates [dbo].[UserLoginHistory] (one row per successful login, shown in "User LogIn History"
      on the User Account Info screen). [dbo].[LogIn] holds the registered users, so history rows
      are NOT written there (they would look like extra users).
    - Creates [dbo].[AddUserLoginHistory]: inserts one history row; Firstname / Surname come from
      [dbo].[LogIn]; the date and time is the server's (SYSDATETIME()).
    - Creates [dbo].[SetCongregationBarcode]: stores the PDF417 PNG in [dbo].[Congregation].[CongBarcode]
      of one member; returns the number of updated rows.
    - Adds to [dbo].[Congregation] (all NULL, existing rows are untouched): CongPhotoData varbinary(max),
      CongPhotoContentType nvarchar(100), CongPassIDData varbinary(max), CongPassIDContentType nvarchar(100),
      CongPassIDFileName nvarchar(260). The existing CongPhoto / CongPassID (path text) are unchanged.
    - Alters [dbo].[AddNewCongregation] and [dbo].[UpdateCongregation]: new OPTIONAL parameters for the
      photo / ID bytes (the old page code still works). Update only replaces the bytes when new, non-empty
      bytes are supplied (same rule as photo / fingerprint / barcode).
    Idempotent: safe to run more than once. Depends on 003_WidenDropdownColumns_RowCounts.sql.
    Rollback: 004_LoginHistory_Barcode_MemberFiles_rollback.sql
    Run (-b stops on the first error, -I is required for the filtered index):
      sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 004_LoginHistory_Barcode_MemberFiles.sql
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
IF NOT EXISTS (SELECT 1 FROM sys.parameters WHERE object_id = OBJECT_ID('dbo.UpdateCongregation') AND name = N'@RowsAffected')
    THROW 50006, N'Migration 003 has not been applied (dbo.UpdateCongregation has no @RowsAffected).', 1;
GO
-- Login history ------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.UserLoginHistory', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[UserLoginHistory]
    (
        [Id]            int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY CLUSTERED,
        [Username]      nvarchar(20)  NOT NULL,  -- login username (ID Number / Passport)
        [Firstname]     nvarchar(15)  NULL,
        [Surname]       nvarchar(20)  NULL,
        [MacAddress]    nvarchar(40)  NULL,      -- MAC of the machine running the app (AA-BB-CC-DD-EE-FF)
        [ClientIP]      nvarchar(45)  NULL,      -- browser address as seen by the server
        [LoginDateTime] datetime2(0)  NOT NULL CONSTRAINT [DF_UserLoginHistory_LoginDateTime] DEFAULT (SYSDATETIME())
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.UserLoginHistory') AND name = N'IX_UserLoginHistory_LoginDateTime')
    CREATE NONCLUSTERED INDEX [IX_UserLoginHistory_LoginDateTime] ON [dbo].[UserLoginHistory] ([LoginDateTime] DESC, [Id] DESC);
GO
CREATE OR ALTER PROCEDURE [dbo].[AddUserLoginHistory]
    @Username   nvarchar(20),
    @MacAddress nvarchar(40) = NULL,
    @ClientIP   nvarchar(45) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[UserLoginHistory] ([Username], [Firstname], [Surname], [MacAddress], [ClientIP], [LoginDateTime])
    SELECT @Username,
           (SELECT TOP 1 l.[LogInFirstname] FROM [dbo].[LogIn] l WHERE l.[LogInPassportID] = @Username),
           (SELECT TOP 1 l.[LogInSurname]   FROM [dbo].[LogIn] l WHERE l.[LogInPassportID] = @Username),
           @MacAddress, @ClientIP, SYSDATETIME();
END
GO
-- Barcode ------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[SetCongregationBarcode]
    @CongPassportID nvarchar(15),
    @CongBarcode    varbinary(max),
    @RowsAffected   int = NULL OUTPUT -- 1 = stored, 0 = no member with that Passport / ID
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Congregation] SET [CongBarcode] = @CongBarcode WHERE [CongPassportID] = @CongPassportID;
    SET @RowsAffected = @@ROWCOUNT;
END
GO
-- Photo / ID document bytes ------------------------------------------------------------------------
IF COL_LENGTH('dbo.Congregation', 'CongPhotoData') IS NULL
    ALTER TABLE [dbo].[Congregation] ADD [CongPhotoData] varbinary(max) NULL;
GO
IF COL_LENGTH('dbo.Congregation', 'CongPhotoContentType') IS NULL
    ALTER TABLE [dbo].[Congregation] ADD [CongPhotoContentType] nvarchar(100) NULL;
GO
IF COL_LENGTH('dbo.Congregation', 'CongPassIDData') IS NULL
    ALTER TABLE [dbo].[Congregation] ADD [CongPassIDData] varbinary(max) NULL;
GO
IF COL_LENGTH('dbo.Congregation', 'CongPassIDContentType') IS NULL
    ALTER TABLE [dbo].[Congregation] ADD [CongPassIDContentType] nvarchar(100) NULL;
GO
IF COL_LENGTH('dbo.Congregation', 'CongPassIDFileName') IS NULL
    ALTER TABLE [dbo].[Congregation] ADD [CongPassIDFileName] nvarchar(260) NULL;
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
@CongMembershipNumber as nvarchar(20) = NULL OUTPUT, -- any incoming value is ignored; returns the generated number
@CongPhotoData as varbinary(max) = NULL,          -- uploaded photo (JPEG / PNG)
@CongPhotoContentType as nvarchar(100) = NULL,
@CongPassIDData as varbinary(max) = NULL,         -- uploaded ID document (JPEG / PNG / PDF)
@CongPassIDContentType as nvarchar(100) = NULL,
@CongPassIDFileName as nvarchar(260) = NULL

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
         ([CongTitle],[CongName],[CongSurname],[CongDOB],[CongGender],[CongPassportID],[CongStatus],[CongProfession],[CongKin],[CongKinContact],[CongCell],[CongAddress],[CongEmail],[CongPosition],[CongDateAppointed],[CongManagerial],[CongDateElected],[CongProvince], [CongDistrict],[CongZone], [CongSection], [CongSnrLeader], [CongViceLeader],[CongPhoto],[CongPassID], [CongregationID],[CongFingerprint],[CongBarcode],[CongMembershipNumber],
          [CongPhotoData],[CongPhotoContentType],[CongPassIDData],[CongPassIDContentType],[CongPassIDFileName] )     
 VALUES (@CongTitle,@CongName,@CongSurname,@CongDOB,@CongGender,@CongPassportID,@CongStatus,@CongProfession,@CongKin,@CongKinContact,@CongCell,@CongAddress,@CongEmail,@CongPosition,@CongDateAppointed,@CongManagerial,@CongDateElected,@CongProvince,@CongDistrict,@CongZone,@CongSection,@CongSnrLeader,@CongViceLeader,@CongPhoto,@CongPassID,@CongregationID,@CongFingerprint,@CongBarcode,@CongMembershipNumber,
         NULLIF(@CongPhotoData, 0x), CASE WHEN DATALENGTH(@CongPhotoData) > 0 THEN @CongPhotoContentType END,
         NULLIF(@CongPassIDData, 0x), CASE WHEN DATALENGTH(@CongPassIDData) > 0 THEN @CongPassIDContentType END,
         CASE WHEN DATALENGTH(@CongPassIDData) > 0 THEN @CongPassIDFileName END )     
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
@RowsAffected as int = NULL OUTPUT, -- number of updated rows (0 = no member with that Passport / ID)
@CongPhotoData as varbinary(max) = NULL,          -- new photo; NULL / empty keeps the stored one
@CongPhotoContentType as nvarchar(100) = NULL,
@CongPassIDData as varbinary(max) = NULL,         -- new ID document; NULL / empty keeps the stored one
@CongPassIDContentType as nvarchar(100) = NULL,
@CongPassIDFileName as nvarchar(260) = NULL
AS 
BEGIN
 SET NOCOUNT ON
 SET @CongregationID=NEWID()
 -- Photo, Passport / ID image, fingerprint, barcode and the photo / ID bytes are only replaced when a
 -- new, non-empty value is supplied; an empty / NULL value keeps what is stored.
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
[CongBarcode]=CASE WHEN @CongBarcode IS NULL OR DATALENGTH(@CongBarcode) = 0 THEN [CongBarcode] ELSE @CongBarcode END,
[CongPhotoData]=CASE WHEN @CongPhotoData IS NULL OR DATALENGTH(@CongPhotoData) = 0 THEN [CongPhotoData] ELSE @CongPhotoData END,
[CongPhotoContentType]=CASE WHEN @CongPhotoData IS NULL OR DATALENGTH(@CongPhotoData) = 0 THEN [CongPhotoContentType] ELSE @CongPhotoContentType END,
[CongPassIDData]=CASE WHEN @CongPassIDData IS NULL OR DATALENGTH(@CongPassIDData) = 0 THEN [CongPassIDData] ELSE @CongPassIDData END,
[CongPassIDContentType]=CASE WHEN @CongPassIDData IS NULL OR DATALENGTH(@CongPassIDData) = 0 THEN [CongPassIDContentType] ELSE @CongPassIDContentType END,
[CongPassIDFileName]=CASE WHEN @CongPassIDData IS NULL OR DATALENGTH(@CongPassIDData) = 0 THEN [CongPassIDFileName] ELSE @CongPassIDFileName END
 WHERE [CongPassportID] = @CongPassportID
 SET @RowsAffected = @@ROWCOUNT
END
GO
SELECT N'objects' AS [Report], name, type_desc FROM sys.objects
WHERE object_id IN (OBJECT_ID('dbo.UserLoginHistory'), OBJECT_ID('dbo.AddUserLoginHistory'), OBJECT_ID('dbo.SetCongregationBarcode'))
ORDER BY name;
SELECT N'columns' AS [Report], c.name, t.name AS [type], c.max_length, c.is_nullable
FROM sys.columns c JOIN sys.types t ON t.user_type_id = c.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Congregation')
  AND c.name IN (N'CongPhotoData', N'CongPhotoContentType', N'CongPassIDData', N'CongPassIDContentType', N'CongPassIDFileName')
ORDER BY c.column_id;
GO
