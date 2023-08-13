CREATE PROCEDURE [dbo].[Employee_GetById]
	@Id int
AS
BEGIN

	SELECT 
		E.Id, 
		E.FirstName, 
		E.LastName, 
		E.Patronymic,
		E.Email
	FROM dbo.[Employee] AS E
	WHERE Id=@Id AND IsDeleted=0
END
