CREATE TABLE UpdateByTest (
    Id INT NOT NULL PRIMARY KEY
	Type VARCHAR(16),
	Text VARCHAR(256),
	Time TIMESTAMPTZ
);
GO
INSERT INTO UpdateByTest (Id, Type, Text, Time) VALUES (1, 'Original', 'Primary Phase', TIMESTAMP '1978-03-08' AT TIME ZONE 'UTC');
GO
INSERT INTO UpdateByTest (Id, Type, Text, Time) VALUES (2, 'Original', 'Secondary Phase', TIMESTAMP '1978-12-24' AT TIME ZONE 'UTC');
GO
INSERT INTO UpdateByTest (Id, Type, Text, Time) VALUES (3, 'Adaptation', 'Tertiary Phase', TIMESTAMP '2004-09-21' AT TIME ZONE 'UTC');
GO
