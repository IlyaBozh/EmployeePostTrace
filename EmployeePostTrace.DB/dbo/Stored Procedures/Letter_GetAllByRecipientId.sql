CREATE PROCEDURE [dbo].[Letter_GetAllByRecipientId]
	@RecipientId int
AS
BEGIN

	SELECT Id, Header, SendingDate
	FROM dbo.[Letter]
	WHERE RecipientId=@RecipientId AND IsDeleted=0

END
