/*
    Rollback for migration 002 (Membership Number registry / auto-generation).

    !! REVERT THE PAGE CODE FIRST !!
    Revert Congregation.aspx, Congregation.aspx.cs, Congregation.aspx.designer.cs (and remove
    CongregationRules.cs + its <Compile> entry in ZAFMC.csproj) to the pre-002 versions and
    redeploy BEFORE running this script. The 002 page code passes @CongMembershipNumber as an
    OUTPUT parameter; the restored (001) AddNewCongregation does not declare it OUTPUT, so every
    Save fails until the page code is reverted.
    If you also roll back 001, run THIS script first, then 001_AddCongMembershipNumber_rollback.sql.
    If 003 was applied, run 003_WidenDropdownColumns_RowCounts_rollback.sql first.

    What it does:
    - Restores [dbo].[AddNewCongregation] and [dbo].[UpdateCongregation] to their exact pre-002
      (= 001) definitions.
    - Drops [dbo].[IssueMembershipNumber] and the filtered unique index
      [UX_Congregation_CongMembershipNumber].
    - KEEPS [dbo].[MembershipNumberRegistry] and the backfilled numbers by default, so no data
      and no "never reuse" history is lost. See the optional, commented-out steps at the end.
    Do not run it unless you really want to undo 002.
    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 002_MembershipNumberRegistry_rollback.sql
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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
@CongMembershipNumber as nvarchar(20) = NULL

AS 
BEGIN
 SET NOCOUNT ON
 SET @CongregationID=NEWID()
INSERT INTO [dbo].[Congregation]
         ([CongTitle],[CongName],[CongSurname],[CongDOB],[CongGender],[CongPassportID],[CongStatus],[CongProfession],[CongKin],[CongKinContact],[CongCell],[CongAddress],[CongEmail],[CongPosition],[CongDateAppointed],[CongManagerial],[CongDateElected],[CongProvince], [CongDistrict],[CongZone], [CongSection], [CongSnrLeader], [CongViceLeader],[CongPhoto],[CongPassID], [CongregationID],[CongFingerprint],[CongBarcode],[CongMembershipNumber] )     
 VALUES (@CongTitle,@CongName,@CongSurname,@CongDOB,@CongGender,@CongPassportID,@CongStatus,@CongProfession,@CongKin,@CongKinContact,@CongCell,@CongAddress,@CongEmail,@CongPosition,@CongDateAppointed,@CongManagerial,@CongDateElected,@CongProvince,@CongDistrict,@CongZone,@CongSection,@CongSnrLeader,@CongViceLeader,@CongPhoto,@CongPassID,@CongregationID,@CongFingerprint,@CongBarcode,@CongMembershipNumber )     

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
@CongMembershipNumber as nvarchar(20) = NULL
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
[CongBarcode]=@CongBarcode,
[CongMembershipNumber]=@CongMembershipNumber
 WHERE [CongPassportID] = @CongPassportID
END
GO
DROP PROCEDURE IF EXISTS [dbo].[IssueMembershipNumber];
GO
DROP INDEX IF EXISTS [UX_Congregation_CongMembershipNumber] ON [dbo].[Congregation];
GO
/*
    OPTIONAL (destructive, not run by default):
    WARNING: dropping the registry loses the history of every number ever issued, so numbers of
    deleted members could be issued again if generation is ever re-enabled.

UPDATE [dbo].[Congregation] SET [CongMembershipNumber] = NULL;
DROP TABLE IF EXISTS [dbo].[MembershipNumberRegistry];
*/