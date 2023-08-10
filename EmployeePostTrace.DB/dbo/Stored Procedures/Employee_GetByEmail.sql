CREATE PROCEDURE [dbo].[Employee_GetByEmail]
	@Email nvarchar(50)
AS
BEGIN

	SELECT 
		Id,
		FirstName,
		LastName,
		Patronymic,
		Email,
		[Password]
	FROM dbo.[Employee]
	WHERE Email = @Email AND IsDeleted = 0

END
