CREATE PROCEDURE [dbo].[Letter_GetAllByEmployeeId]
	@Id int ,
	@IsIncoming bit
AS
BEGIN

	SELECT Id, Header, SendingDate
	FROM dbo.[Letter]
	WHERE Id=@Id AND IsIncoming=@IsIncoming AND IsDeleted=0

END
