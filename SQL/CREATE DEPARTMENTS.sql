CREATE TABLE DEPARTMENTS (
  ID       INT          NOT NULL IDENTITY (1, 1) PRIMARY KEY,
  NAME     VARCHAR(70)  NOT NULL,
  Location VARCHAR(250) NOT NULL,
  UNIQUE (NAME),
  NumberOfEmployees              AS (dbo.GetNumberOfEmployees(ID))
);