CREATE PROCEDURE [dbo].[Letter_Delete]
	@id int,
	@isDeleted bit
AS
BEGIN

	UPDATE dbo.[Letter]
	SET isDeleted = @isDeleted
	WHERE Id = @Id

END
