--This function calculates the budget used on a specific project.
--For each time entry on project @P_ID multiply by that employee's rate
CREATE FUNCTION dbo.GetBudgetUsed(@P_ID INT)
  RETURNS INT
AS
  BEGIN
    DECLARE @TOTAL FLOAT;
    SELECT @TOTAL = SUM(EMPLOYEE.HOURLY_WAGE * TIMES.HOURS_WORKED)
    FROM TIMES, EMPLOYEES,
        WHERE((@P_ID = TIMES.PROJECT_ID)
              AND
              (TIMES.EID = EMPLOYEES.EMPLOYEE_ID))
    RETURN @TOTAL

  END;
