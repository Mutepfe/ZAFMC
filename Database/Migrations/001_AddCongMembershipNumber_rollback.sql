/*
    !! REVERT THE PAGE CODE FIRST !!
    Revert Congregation.aspx, Congregation.aspx.cs and Congregation.aspx.designer.cs to the
    pre-001 versions BEFORE running this script. The 001 page code passes @CongMembershipNumber;
    the restored procs do not declare it, so every Save/Update fails with
    "Procedure or function ... has too many arguments specified" until the code is reverted.
    If migration 002 was applied, run 002_MembershipNumberRegistry_rollback.sql FIRST.

    Rollback for migration 001: restores the original definitions of
    [dbo].[AddNewCongregation] and [dbo].[UpdateCongregation] (without @CongMembershipNumber).

    The [CongMembershipNumber] column is left in place by default so no data is lost.
    See the optional, commented-out step at the end to drop it (only when it holds no data).

    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -i 001_AddCongMembershipNumber_rollback.sql
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
@CongBarcode as image

AS 
BEGIN
 SET NOCOUNT ON
 SET @CongregationID=NEWID()
INSERT INTO [dbo].[Congregation]
         ([CongTitle],[CongName],[CongSurname],[CongDOB],[CongGender],[CongPassportID],[CongStatus],[CongProfession],[CongKin],[CongKinContact],[CongCell],[CongAddress],[CongEmail],[CongPosition],[CongDateAppointed],[CongManagerial],[CongDateElected],[CongProvince], [CongDistrict],[CongZone], [CongSection], [CongSnrLeader], [CongViceLeader],[CongPhoto],[CongPassID], [CongregationID],[CongFingerprint],[CongBarcode] )     
 VALUES (@CongTitle,@CongName,@CongSurname,@CongDOB,@CongGender,@CongPassportID,@CongStatus,@CongProfession,@CongKin,@CongKinContact,@CongCell,@CongAddress,@CongEmail,@CongPosition,@CongDateAppointed,@CongManagerial,@CongDateElected,@CongProvince,@CongDistrict,@CongZone,@CongSection,@CongSnrLeader,@CongViceLeader,@CongPhoto,@CongPassID,@CongregationID,@CongFingerprint,@CongBarcode )     

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
@CongBarcode as image
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

/*
    OPTIONAL: drop the column ONLY if no row holds a membership number.
    Also revert the SELECTs/parameters in Congregation.aspx(.cs) first, or the page will error.

IF COL_LENGTH('dbo.Congregation', 'CongMembershipNumber') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM [dbo].[Congregation] WHERE [CongMembershipNumber] IS NOT NULL)
    ALTER TABLE [dbo].[Congregation] DROP COLUMN [CongMembershipNumber];
*/
