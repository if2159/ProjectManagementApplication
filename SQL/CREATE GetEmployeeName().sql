--This function retrieves an employee's name for a user @U_ID
CREATE FUNCTION dbo.GetEmployeeName(@U_ID INT)
RETURNS VARCHAR(73)
AS
BEGIN
	DECLARE @NAME VARCHAR(73);
   Select @NAME = NAME
	from EMPLOYEES
	where @U_ID = EMPLOYEES.EMPLOYEE_ID
	RETURN @NAME
END;
