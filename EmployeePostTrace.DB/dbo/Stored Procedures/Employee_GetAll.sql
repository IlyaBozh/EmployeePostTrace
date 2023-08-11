CREATE PROCEDURE [dbo].[Employee_GetAll]

AS
BEGIN

	SELECT 
		Id, 
		FirstName, 
		LastName, 
		Patronymic
	FROM dbo.Employee
	WHERE IsDeleted = 0

END
