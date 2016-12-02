CREATE TABLE dbo.UpdateByTest (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	[Text] NVARCHAR(256),
	[Time] DATETIMEOFFSET
)
GO
INSERT dbo.UpdateByTest VALUES (N'Fail', '1978-03-08');
GO
