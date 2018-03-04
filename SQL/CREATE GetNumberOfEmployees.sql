--Calculate the number of employees in department @D_ID
CREATE FUNCTION dbo.GetNumberOfEmployees(@D_ID INT)
  RETURNS INT
AS
  BEGIN
    DECLARE @TOTAL INT;
    SELECT @TOTAL = sum(ID)
    FROM DEPARTMENTS AS D, TEAMS AS T, EMPLOYEES AS E
    WHERE
      D.ID = T.DEPARTMENT_ID AND T.TEAM_ID = E.TEAM_ID;
    RETURN @TOTAL

  END;
