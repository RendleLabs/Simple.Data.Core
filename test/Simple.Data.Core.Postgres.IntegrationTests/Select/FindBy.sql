CREATE SEQUENCE FindByTest_Id_Seq;
CREATE TABLE FindByTest (
    Id INT NOT NULL PRIMARY KEY DEFAULT nextval('FindByTest_Id_Seq'),
	Type VARCHAR(16),
	Text VARCHAR(256),
	Time TIMESTAMPTZ
);
ALTER SEQUENCE FindByTest_Id_Seq OWNED BY FindByTest.Id;
GO
INSERT INTO FindByTest (Type, Text, Time) VALUES ('Original', 'Primary Phase', TIMESTAMP '1978-03-08' AT TIME ZONE 'UTC');
GO
INSERT INTO FindByTest (Type, Text, Time) VALUES ('Original', 'Secondary Phase', TIMESTAMP '1978-12-24' AT TIME ZONE 'UTC');
GO
INSERT INTO FindByTest (Type, Text, Time) VALUES ('Adaptation', 'Tertiary Phase', TIMESTAMP '2004-09-21' AT TIME ZONE 'UTC');
GO
