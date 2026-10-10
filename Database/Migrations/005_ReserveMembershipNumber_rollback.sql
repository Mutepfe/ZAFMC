/*
    Rollback for migration 005 (Membership Number reserved by Generate Barcode / Save).

    !! REVERT THE PAGE CODE FIRST !!
    Revert Congregation.aspx.cs and CongregationRules.cs to the pre-005 versions and redeploy BEFORE
    running this script: the 005 page code calls dbo.ReserveMembershipNumber and passes
    @ReservedMembershipNumber to dbo.AddNewCongregation.
    Run this BEFORE 004_LoginHistory_Barcode_MemberFiles_rollback.sql.

    What it does:
    - Restores [dbo].[AddNewCongregation] to its exact pre-005 (004) definition
      (backup: .agents/tasks/db-backup/005_before_AddNewCongregation.sql).
    - Drops [dbo].[ReserveMembershipNumber].
    - Leaves [dbo].[MembershipNumberRegistry] untouched: reserved but unused numbers (CongPassportID NULL)
      stay in the ledger, so they are never issued again.
    Idempotent: safe to run more than once.
    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 005_ReserveMembershipNumber_rollback.sql
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
DROP PROCEDURE IF EXISTS [dbo].[ReserveMembershipNumber];
GO
SELECT N'objects' AS [Report], name, type_desc FROM sys.objects
WHERE object_id IN (OBJECT_ID('dbo.ReserveMembershipNumber'), OBJECT_ID('dbo.AddNewCongregation'))
ORDER BY name;
GO
