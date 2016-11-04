CREATE SEQUENCE SingleInsertTest_Id_Seq;
CREATE TABLE SingleInsertTest (
    Id INT NOT NULL PRIMARY KEY DEFAULT nextval('SingleInsertTest_Id_Seq'),
	Text VARCHAR(256),
	Time TIMESTAMPTZ
);
ALTER SEQUENCE SingleInsertTest_Id_Seq OWNED BY SingleInsertTest.Id;
GO
