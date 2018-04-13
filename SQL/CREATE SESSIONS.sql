
CREATE TABLE SESSIONS (
  ENTRY_ID     INT      NOT NULL IDENTITY (1,1),
  EMPLOYEE_ID          NUMERIC(10) NOT NULL CHECK (EMPLOYEE_ID > 999999999),
  SESSION_ID   VARCHAR(36)    NOT NULL,
  ENTRY_DATE   DATETIME NOT NULL,
  EXPIRATION_DATE   DATETIME NOT NULL,
  PRIMARY KEY (ENTRY_ID),
  FOREIGN KEY (EMPLOYEE_ID) REFERENCES EMPLOYEES (EMPLOYEE_ID),
);
