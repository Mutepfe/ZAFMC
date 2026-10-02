/*
    Migration 002: auto-generated, permanent, never-reused Membership Numbers.
    - Normalises blank [dbo].[Congregation].[CongMembershipNumber] values ('' -> NULL).
    - Adds [dbo].[MembershipNumberRegistry]: a permanent ledger of every membership number
      ever issued. Rows are NEVER deleted (also not when the member is deleted), so a number
      can never be issued twice.
    - Adds filtered unique index [UX_Congregation_CongMembershipNumber] on
      [dbo].[Congregation]([CongMembershipNumber]) WHERE NOT NULL (second guard).
    - Adds [dbo].[IssueMembershipNumber]: generates 'ZAFMC' + 8 random digits
      (CRYPT_GEN_RANDOM), retries until unused, records it in the registry.
    - Alters [dbo].[AddNewCongregation]: ignores any incoming membership value, issues a new
      number in the same transaction as the insert and returns it via
      @CongMembershipNumber OUTPUT (still optional, so older callers keep working).
    - Alters [dbo].[UpdateCongregation]: no longer changes [CongMembershipNumber]
      (the parameter is kept, but ignored, for compatibility).
    - Backfills a number for every existing row that has none.
    Idempotent: safe to run more than once. Depends on 001_AddCongMembershipNumber.sql.
    Rollback: 002_MembershipNumberRegistry_rollback.sql
    Run (-b stops on the first error, -I is required for the filtered index):
      sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 002_MembershipNumberRegistry.sql
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
IF COL_LENGTH('dbo.Congregation', 'CongMembershipNumber') IS NULL
    THROW 50003, N'Migration 001 has not been applied (dbo.Congregation.CongMembershipNumber is missing).', 1;
GO
-- Blank membership numbers are stored as NULL, never ''.
UPDATE [dbo].[Congregation] SET [CongMembershipNumber] = NULL WHERE [CongMembershipNumber] = N'';
GO
-- Permanent ledger of every membership number ever issued. Rows are never deleted.
IF OBJECT_ID('dbo.MembershipNumberRegistry', 'U') IS NULL
    CREATE TABLE [dbo].[MembershipNumberRegistry]
    (
        [MembershipNumber] NVARCHAR(20) NOT NULL
            CONSTRAINT [PK_MembershipNumberRegistry] PRIMARY KEY,
        [IssuedAt] DATETIME2 NOT NULL
            CONSTRAINT [DF_MembershipNumberRegistry_IssuedAt] DEFAULT SYSUTCDATETIME(),
        [CongPassportID] NVARCHAR(15) NULL, -- informational only, no FK (members can be deleted)
        CONSTRAINT [CK_MembershipNumberRegistry_Format] CHECK
            (LEN([MembershipNumber]) = 13
             AND [MembershipNumber] LIKE N'ZAFMC[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
    );
GO
-- Legacy guard: any number already stored must have the official format.
IF EXISTS (SELECT 1 FROM [dbo].[Congregation]
           WHERE [CongMembershipNumber] IS NOT NULL
             AND NOT (LEN([CongMembershipNumber]) = 13
                      AND [CongMembershipNumber] LIKE N'ZAFMC[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
    THROW 50002, N'dbo.Congregation contains membership numbers that are not ZAFMC + 8 digits. Fix them manually before running migration 002.', 1;
GO
INSERT INTO [dbo].[MembershipNumberRegistry] ([MembershipNumber], [CongPassportID])
SELECT c.[CongMembershipNumber], MIN(c.[CongPassportID])
FROM [dbo].[Congregation] c
WHERE c.[CongMembershipNumber] IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM [dbo].[MembershipNumberRegistry] r
                  WHERE r.[MembershipNumber] = c.[CongMembershipNumber])
GROUP BY c.[CongMembershipNumber];
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'UX_Congregation_CongMembershipNumber'
                 AND object_id = OBJECT_ID('dbo.Congregation'))
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Congregation_CongMembershipNumber]
        ON [dbo].[Congregation] ([CongMembershipNumber])
        WHERE [CongMembershipNumber] IS NOT NULL;
GO
-- Issues a new, never-used membership number and records it in the registry.
CREATE OR ALTER PROCEDURE [dbo].[IssueMembershipNumber]
@CongPassportID as nvarchar(15) = NULL,
@MembershipNumber as nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @candidate nvarchar(20), @attempt int = 0;
    SET @MembershipNumber = NULL;

    BEGIN TRAN;
    WHILE 1 = 1
    BEGIN
        SET @attempt += 1;
        IF @attempt > 100
            THROW 50001, N'Unable to generate a unique membership number.', 1;

        -- 8 cryptographically random digits (leading zeros kept), never RAND().
        SET @candidate = N'ZAFMC' + RIGHT(N'00000000' + CAST(
            (CAST(CRYPT_GEN_RANDOM(8) AS bigint) & CAST(0x7FFFFFFFFFFFFFFF AS bigint)) % 100000000
            AS nvarchar(8)), 8);

        INSERT INTO [dbo].[MembershipNumberRegistry] ([MembershipNumber], [CongPassportID])
        SELECT @candidate, @CongPassportID
        WHERE NOT EXISTS (SELECT 1 FROM [dbo].[MembershipNumberRegistry] WITH (UPDLOCK, HOLDLOCK)
                          WHERE [MembershipNumber] = @candidate)
          AND NOT EXISTS (SELECT 1 FROM [dbo].[Congregation]
                          WHERE [CongMembershipNumber] = @candidate);

        IF @@ROWCOUNT = 1 BREAK;
    END
    COMMIT;
    SET @MembershipNumber = @candidate;
END
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
-- Backfill: give every existing member without a number a newly issued one.
SET XACT_ABORT ON;
SELECT N'before' AS [Backfill],
       COUNT(*) AS [Rows],
       SUM(CASE WHEN [CongMembershipNumber] IS NULL THEN 1 ELSE 0 END) AS [WithoutNumber],
       (SELECT COUNT(*) FROM [dbo].[MembershipNumberRegistry]) AS [RegistryRows]
FROM [dbo].[Congregation];

DECLARE @pid nvarchar(15), @num nvarchar(20), @done int = 0;
DECLARE backfill CURSOR LOCAL FAST_FORWARD FOR
    SELECT [CongPassportID] FROM [dbo].[Congregation]
    WHERE [CongMembershipNumber] IS NULL
    ORDER BY [CongPassportID];
OPEN backfill;
FETCH NEXT FROM backfill INTO @pid;
WHILE @@FETCH_STATUS = 0
BEGIN
    BEGIN TRAN;
    EXEC [dbo].[IssueMembershipNumber] @CongPassportID = @pid, @MembershipNumber = @num OUTPUT;
    UPDATE [dbo].[Congregation] SET [CongMembershipNumber] = @num
    WHERE [CongPassportID] = @pid AND [CongMembershipNumber] IS NULL;
    IF @@ROWCOUNT = 1
    BEGIN
        COMMIT;
        SET @done += 1;
    END
    ELSE
        ROLLBACK;
    FETCH NEXT FROM backfill INTO @pid;
END
CLOSE backfill;
DEALLOCATE backfill;

SELECT N'after' AS [Backfill],
       @done AS [Backfilled],
       COUNT(*) AS [Rows],
       SUM(CASE WHEN [CongMembershipNumber] IS NULL THEN 1 ELSE 0 END) AS [WithoutNumber],
       COUNT(DISTINCT [CongMembershipNumber]) AS [DistinctNumbers],
       (SELECT COUNT(*) FROM [dbo].[MembershipNumberRegistry]) AS [RegistryRows]
FROM [dbo].[Congregation];
GO
