CREATE PROCEDURE [dbo].[Employee_GetById]
	@Id int
AS
BEGIN

	SELECT 
		E.Id, 
		E.FirstName, 
		E.LastName, 
		E.Patronymic
	FROM dbo.[Employee] AS E

END
