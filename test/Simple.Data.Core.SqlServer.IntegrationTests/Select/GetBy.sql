CREATE TABLE dbo.GetByTest (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	[Text] NVARCHAR(256),
	[Time] DATETIMEOFFSET
)
GO
INSERT dbo.GetByTest VALUES (N'Pass', '1978-03-08');
GO