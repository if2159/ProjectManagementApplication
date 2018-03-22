CREATE TABLE PROJECTS (
  PROJECT_ID                     INT         NOT NULL,
  NAME                   VARCHAR(70) NOT NULL,
  BUDGET                 FLOAT       NOT NULL,
  BUDGET_USED            FLOAT       NOT NULL,
  CONTROLLING_DEPARTMENT INT         NOT NULL,
  EID                    INT,
  CHANGE_DATE            DATETIME,
  COMMENT                VARCHAR(250),
  CONTROLLING_TEAM       INT,
  START_DATE              DATETIME NOT NULL,
  STATUS_TYPE            VARCHAR(35) NOT NULL CHECK (STATUS_TYPE IN ('Complete', 'In Dev',
                                                                     'In QA', 'Waiting', 'In Review')),
  MODIFIED       AS (EID + ' ' + CHANGE_DATE),
  CURRENT_STATUS AS (STATUS_TYPE + ' :  ' + COMMENT),

  PRIMARY KEY (ID, NAME),
  FOREIGN KEY (EID) REFERENCES USERS (EMPLOYEE_ID),
  FOREIGN KEY (CONTROLLING_DEPARTMENT) REFERENCES DEPARTMENTS (ID),
  FOREIGN KEY (TEAM) REFERENCES TEAMS (TEAM_ID)
)
