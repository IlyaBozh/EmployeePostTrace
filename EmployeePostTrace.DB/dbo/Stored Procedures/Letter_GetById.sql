CREATE PROCEDURE [dbo].[Letter_GetById]
	@Id int
AS
BEGIN

	SELECT Id, Header, Sender, Recipient, Content, SendingDate
	FROM dbo.[Letter]
	WHERE Id=@Id

END
