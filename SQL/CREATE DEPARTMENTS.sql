CREATE TABLE DEPARTMENTS (
  ID       INT          NOT NULL IDENTITY (1, 1) PRIMARY KEY,
  NAME     VARCHAR(70)  NOT NULL,
  Street_Number VARCHAR(20) NOT NULL,
  Street_Name VARCHAR(50) NOT NULL,
  City VARCHAR(60) NOT NULL,
  State_Province_Region VARCHAR(50) NOT NULL,
  ZipCode VARCHAR(16) NOT NULL,
  Country VARCHAR(90) NOT NULL,
  LOCATION AS (Street_Number + ' ' + Street_Name + ' ' + City + ', ' + State_Province_Region + ' ' + ZipCode + ' ' + Country),
  UNIQUE (NAME),
  NumberOfEmployees              AS (dbo.GetNumberOfEmployees(ID))
);