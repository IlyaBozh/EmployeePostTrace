CREATE PROCEDURE [dbo].[Letter_GetAllByRecipientId]
	@RecipientId int
AS
BEGIN

	SELECT Id, Sender, Header, SendingDate
	FROM dbo.[Letter]
	WHERE RecipientId=@RecipientId AND IsDeleted=0

END
