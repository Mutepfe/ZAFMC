/*
    Rollback for migration 006 (no stale barcode when the reserved Membership Number could not be used).

    Revert Congregation.aspx.cs to the pre-006 version before running this script: the 006 page code no
    longer stores NULL itself after a fallback, because the 006 procedure already does.

    What it does:
    - Restores [dbo].[AddNewCongregation] to its exact pre-006 (005) definition
      (backup: .agents/tasks/db-backup/006_before_AddNewCongregation.sql).
    Idempotent: safe to run more than once.
    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 006_FallbackBarcodeNull_rollback.sql
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
@CongMembershipNumber as nvarchar(20) = NULL OUTPUT, -- any incoming value is ignored; returns the number used
@CongPhotoData as varbinary(max) = NULL,          -- uploaded photo (JPEG / PNG)
@CongPhotoContentType as nvarchar(100) = NULL,
@CongPassIDData as varbinary(max) = NULL,         -- uploaded ID document (JPEG / PNG / PDF)
@CongPassIDContentType as nvarchar(100) = NULL,
@CongPassIDFileName as nvarchar(260) = NULL,
@ReservedMembershipNumber as nvarchar(20) = NULL  -- from dbo.ReserveMembershipNumber; used only while still free

AS 
BEGIN
 SET NOCOUNT ON
 SET XACT_ABORT ON
 SET @CongregationID=NEWID()
 SET @CongMembershipNumber = NULL
 BEGIN TRY
  BEGIN TRAN
  -- The reserved number is used only when it has the official format (case-sensitive), is still reserved
  -- in the registry (CongPassportID NULL) and no member has it; the locks keep that true until COMMIT.
  IF @ReservedMembershipNumber IS NOT NULL
     AND DATALENGTH(@ReservedMembershipNumber) = 26
     AND @ReservedMembershipNumber COLLATE Latin1_General_BIN2 LIKE N'ZAFMC[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
  BEGIN
   UPDATE r SET [CongPassportID] = @CongPassportID
   FROM [dbo].[MembershipNumberRegistry] r WITH (UPDLOCK, HOLDLOCK)
   WHERE r.[MembershipNumber] = @ReservedMembershipNumber
     AND r.[CongPassportID] IS NULL
     AND NOT EXISTS (SELECT 1 FROM [dbo].[Congregation] c WITH (UPDLOCK, HOLDLOCK)
                     WHERE c.[CongMembershipNumber] = @ReservedMembershipNumber)
   IF @@ROWCOUNT = 1 SET @CongMembershipNumber = @ReservedMembershipNumber
  END
  -- No (usable) reserved number: issue a fresh one, as before.
  IF @CongMembershipNumber IS NULL
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
  -- A failed insert (e.g. duplicate Passport / ID) also rolls back the registry change: a fresh number
  -- is not kept, and a reserved number is free again (CongPassportID NULL) for the next Save.
  IF @@TRANCOUNT > 0 ROLLBACK
  SET @CongMembershipNumber = NULL;
  THROW;
 END CATCH
END
GO
SELECT N'parameters' AS [Report], COUNT(*) AS [Count] FROM sys.parameters WHERE object_id = OBJECT_ID('dbo.AddNewCongregation');
GO
