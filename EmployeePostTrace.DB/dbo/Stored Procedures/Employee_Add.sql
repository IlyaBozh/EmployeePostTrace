CREATE PROCEDURE [dbo].[Employee_Add]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Patronymic nvarchar(50),
	@Email nvarchar(50),
	@Password nvarchar(255)
AS
BEGIN
	INSERT INTO dbo.[Employee](
		FirstName,
		LastName,
		Patronymic,
		Email,
		[Password])
	VALUES(
		@FirstName,
		@LastName,
		@Patronymic,
		@Email,
		@Password)

	SELECT @@IDENTITY
END