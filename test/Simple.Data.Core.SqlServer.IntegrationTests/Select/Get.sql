CREATE TABLE dbo.GetTest (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	[Text] NVARCHAR(256),
	[Time] DATETIMEOFFSET
)
GO
INSERT dbo.GetTest VALUES (N'Pass', '1978-03-08');
GO
