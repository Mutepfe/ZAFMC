/*
    Migration 003: full dropdown values, safe Update, affected-row counts.
    - Widens [dbo].[Congregation].[CongPosition] (varchar(15)) and [CongManagerial] (varchar(20))
      to varchar(50), so values such as 'Muvhangeri(Evangelist)' and 'Music, Praise & Worship'
      are no longer truncated. Widening is metadata-only and keeps every stored value.
    - Repairs stored values that are a truncated prefix of EXACTLY ONE declared dropdown value
      (length exactly 15 / 20, case-sensitive prefix match). Ambiguous and off-list values are
      only reported, never changed.
    - Alters [dbo].[AddNewCongregation]: @CongPosition / @CongManagerial widened to varchar(50).
    - Alters [dbo].[UpdateCongregation]: widened params; keeps the existing CongPhoto, CongPassID,
      CongFingerprint and CongBarcode unless a new non-empty value is supplied; returns the number
      of updated rows in the new optional @RowsAffected OUTPUT parameter. The membership number
      is still never changed.
    - Alters [dbo].[DeleteCongregation]: returns the number of deleted rows in the new optional
      @RowsAffected OUTPUT parameter.
    Idempotent: safe to run more than once. Depends on 002_MembershipNumberRegistry.sql.
    Rollback: 003_WidenDropdownColumns_RowCounts_rollback.sql
    Run (-b stops on the first error, -I is required for the filtered index):
      sqlcmd -S "JACOB\SQLEXPRESS2025" -d ZION -E -C -b -I -i 003_WidenDropdownColumns_RowCounts.sql
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
IF OBJECT_ID('dbo.IssueMembershipNumber', 'P') IS NULL
    THROW 50004, N'Migration 002 has not been applied (dbo.IssueMembershipNumber is missing).', 1;
GO
IF COL_LENGTH('dbo.Congregation', 'CongPosition') < 50
    ALTER TABLE [dbo].[Congregation] ALTER COLUMN [CongPosition] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL;
GO
IF COL_LENGTH('dbo.Congregation', 'CongManagerial') < 50
    ALTER TABLE [dbo].[Congregation] ALTER COLUMN [CongManagerial] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL;
GO
-- Repair values that were cut to the old column size (varchar(15) / varchar(20)).
SET XACT_ABORT ON;
DECLARE @Declared TABLE (ColumnName sysname NOT NULL, FullValue varchar(50) NOT NULL, OldSize int NOT NULL);
-- Every declared value of the RankPosition dropdown (Congregation.aspx)
INSERT INTO @Declared (ColumnName, FullValue, OldSize) VALUES
 (N'CongPosition', 'Bishop', 15), (N'CongPosition', 'Vice-Bishop', 15), (N'CongPosition', 'Snr. High Priest', 15),
 (N'CongPosition', 'High Priest', 15), (N'CongPosition', 'Priest', 15), (N'CongPosition', 'Muungamiri', 15),
 (N'CongPosition', 'Mufundisi(Pastor)', 15), (N'CongPosition', 'Muvhangeri(Evangelist)', 15),
 (N'CongPosition', 'Muparidzi(Preacher)', 15), (N'CongPosition', 'Gosa(Decon)', 15), (N'CongPosition', 'VeMweya(Prophet)', 15);
-- Every declared value of the ManagerialPost dropdown (Congregation.aspx)
INSERT INTO @Declared (ColumnName, FullValue, OldSize) VALUES
 (N'CongManagerial', 'Secretary General', 20), (N'CongManagerial', 'Projects Development', 20),
 (N'CongManagerial', 'Music, Praise & Worship', 20), (N'CongManagerial', 'Social Service', 20),
 (N'CongManagerial', 'Security', 20), (N'CongManagerial', 'ICT', 20), (N'CongManagerial', 'Education Skills', 20),
 (N'CongManagerial', 'Health', 20), (N'CongManagerial', 'Chairman', 20), (N'CongManagerial', 'Vice-Chairman', 20),
 (N'CongManagerial', 'Treasurer', 20), (N'CongManagerial', 'Secretary', 20), (N'CongManagerial', 'Vice-Secretary', 20);

IF OBJECT_ID('tempdb..#Stored') IS NOT NULL DROP TABLE #Stored;
SELECT c.[CongPassportID], N'CongPosition' AS ColumnName, c.[CongPosition] AS StoredValue
INTO #Stored
FROM [dbo].[Congregation] c WHERE c.[CongPosition] IS NOT NULL
UNION ALL
SELECT c.[CongPassportID], N'CongManagerial', c.[CongManagerial]
FROM [dbo].[Congregation] c WHERE c.[CongManagerial] IS NOT NULL;

IF OBJECT_ID('tempdb..#Repair') IS NOT NULL DROP TABLE #Repair;
SELECT s.[CongPassportID], s.ColumnName, s.StoredValue AS OldValue,
       (SELECT MIN(d.FullValue) FROM @Declared d
        WHERE d.ColumnName = s.ColumnName AND LEN(d.FullValue) > d.OldSize
          AND LEFT(d.FullValue, d.OldSize) COLLATE Latin1_General_CS_AS = s.StoredValue COLLATE Latin1_General_CS_AS) AS NewValue,
       (SELECT COUNT(*) FROM @Declared d
        WHERE d.ColumnName = s.ColumnName AND LEN(d.FullValue) > d.OldSize
          AND LEFT(d.FullValue, d.OldSize) COLLATE Latin1_General_CS_AS = s.StoredValue COLLATE Latin1_General_CS_AS) AS Matches
INTO #Repair
FROM #Stored s
WHERE LEN(s.StoredValue) = (SELECT MAX(d.OldSize) FROM @Declared d WHERE d.ColumnName = s.ColumnName)
  AND NOT EXISTS (SELECT 1 FROM @Declared d
                  WHERE d.ColumnName = s.ColumnName
                    AND d.FullValue COLLATE Latin1_General_CS_AS = s.StoredValue COLLATE Latin1_General_CS_AS);
DELETE FROM #Repair WHERE Matches = 0;

SELECT N'repair preview' AS [Report], [CongPassportID], ColumnName, OldValue, NewValue, Matches FROM #Repair ORDER BY [CongPassportID], ColumnName;

DECLARE @posRepaired int, @manRepaired int;
BEGIN TRAN;
UPDATE c SET c.[CongPosition] = r.NewValue
FROM [dbo].[Congregation] c
JOIN #Repair r ON r.[CongPassportID] = c.[CongPassportID] AND r.ColumnName = N'CongPosition' AND r.Matches = 1
WHERE c.[CongPosition] COLLATE Latin1_General_CS_AS = r.OldValue COLLATE Latin1_General_CS_AS;
SET @posRepaired = @@ROWCOUNT;
UPDATE c SET c.[CongManagerial] = r.NewValue
FROM [dbo].[Congregation] c
JOIN #Repair r ON r.[CongPassportID] = c.[CongPassportID] AND r.ColumnName = N'CongManagerial' AND r.Matches = 1
WHERE c.[CongManagerial] COLLATE Latin1_General_CS_AS = r.OldValue COLLATE Latin1_General_CS_AS;
SET @manRepaired = @@ROWCOUNT;
COMMIT;

SELECT N'repaired' AS [Report], @posRepaired AS [CongPositionRows], @manRepaired AS [CongManagerialRows];
SELECT N'ambiguous - left alone' AS [Report], [CongPassportID], ColumnName, OldValue, Matches FROM #Repair WHERE Matches > 1 ORDER BY [CongPassportID], ColumnName;
SELECT N'off-list - left alone' AS [Report], s.[CongPassportID], s.ColumnName, s.StoredValue
FROM #Stored s
WHERE NOT EXISTS (SELECT 1 FROM @Declared d
                  WHERE d.ColumnName = s.ColumnName
                    AND d.FullValue COLLATE Latin1_General_CS_AS = s.StoredValue COLLATE Latin1_General_CS_AS)
  AND NOT EXISTS (SELECT 1 FROM #Repair r
                  WHERE r.[CongPassportID] = s.[CongPassportID] AND r.ColumnName = s.ColumnName AND r.Matches = 1)
ORDER BY s.[CongPassportID], s.ColumnName;

DROP TABLE #Repair;
DROP TABLE #Stored;
GO
SET XACT_ABORT OFF;
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
ALTER PROCEDURE [dbo].[DeleteCongregation]
@DelCONG as varchar(15),
@RowsAffected as int = NULL OUTPUT -- number of deleted rows (0 = no member with that Passport / ID)
AS 
BEGIN
    DELETE FROM [dbo].Congregation WHERE [CongPassportID]= @DelCONG
    SET @RowsAffected = @@ROWCOUNT
END
GO
SELECT N'columns' AS [Report], c.name, t.name AS [type], c.max_length, c.is_nullable, c.collation_name
FROM sys.columns c JOIN sys.types t ON t.user_type_id = c.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Congregation') AND c.name IN (N'CongPosition', N'CongManagerial');
SELECT N'parameters' AS [Report], OBJECT_NAME(p.object_id) AS [proc], p.name, t.name AS [type], p.max_length, p.is_output
FROM sys.parameters p JOIN sys.types t ON t.user_type_id = p.user_type_id
WHERE p.object_id IN (OBJECT_ID('dbo.AddNewCongregation'), OBJECT_ID('dbo.UpdateCongregation'), OBJECT_ID('dbo.DeleteCongregation'))
  AND p.name IN (N'@CongPosition', N'@CongManagerial', N'@RowsAffected')
ORDER BY [proc], p.parameter_id;
GO
