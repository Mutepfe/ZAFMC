/*
    Migration 001: Add Membership Number to the Congregation table.

    - Adds nullable column [dbo].[Congregation].[CongMembershipNumber] NVARCHAR(20)
      (width matches the MembershipNumber TextBox MaxLength="20" on Congregation.aspx).
      Nullable with no uniqueness constraint because existing rows have no value.
    - Alters [dbo].[AddNewCongregation] and [dbo].[UpdateCongregation] to accept
      @CongMembershipNumber (default NULL so older callers keep working) and persist it.
      All other proc logic is unchanged.

    Idempotent: safe to run more than once.
    Rollback: 001_AddCongMembershipNumber_rollback.sql
    Run: sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -i 001_AddCongMembershipNumber.sql
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF COL_LENGTH('dbo.Congregation', 'CongMembershipNumber') IS NULL
    ALTER TABLE [dbo].[Congregation] ADD [CongMembershipNumber] NVARCHAR(20) NULL;
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
