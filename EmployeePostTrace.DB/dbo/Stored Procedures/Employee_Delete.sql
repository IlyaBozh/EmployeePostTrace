CREATE PROCEDURE [dbo].[Employee_Delete]
		@id int,
		@isDeleted bit
AS
BEGIN

	UPDATE dbo.[Employee]
	SET isDeleted = @isDeleted
	WHERE Id = @Id

END
