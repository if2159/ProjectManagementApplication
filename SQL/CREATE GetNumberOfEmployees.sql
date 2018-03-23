--Calculate the number of employees in department @D_ID
CREATE FUNCTION dbo.GetNumberOfEmployees(@D_ID INT)
  RETURNS INT
AS
  BEGIN
    DECLARE @TOTAL INT;
    SELECT @TOTAL = count(EMPLOYEE_ID)
    FROM TEAMS AS T, EMPLOYEES AS E
    WHERE
      @D_ID = T.DEPARTMENT_ID AND T.TEAM_ID = E.TEAM_ID;
    RETURN @TOTAL

  END;
