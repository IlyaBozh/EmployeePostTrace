CREATE PROCEDURE [dbo].[Employee_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Patronymic nvarchar(50)
AS
BEGIN

	UPDATE dbo.[Employee]
	SET FirstName = @FirstName,
		LastName = @LastName,
		Patronymic = @Patronymic

	WHERE Id = @Id

END
