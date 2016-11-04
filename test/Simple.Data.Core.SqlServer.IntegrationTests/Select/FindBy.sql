CREATE TABLE dbo.FindByTest (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	[Type] NVARCHAR(16),
	[Text] NVARCHAR(256),
	[Time] DATETIMEOFFSET
)
GO
INSERT dbo.FindByTest VALUES (N'Original', N'Primary Phase', '1978-03-08');
INSERT dbo.FindByTest VALUES (N'Original', N'Secondary Phase', '1978-12-24');
INSERT dbo.FindByTest VALUES (N'Adaptation', N'Tertiary Phase', '2004-09-21');
GO
