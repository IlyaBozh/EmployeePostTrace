CREATE PROCEDURE [dbo].[Letter_Update]
	@Id int,
	@Header nvarchar(50),
	@Content nvarchar(50)
AS
BEGIN

	UPDATE dbo.[Letter]
	SET Header = @Header,
		Content = @Content

	WHERE Id = @Id

END
