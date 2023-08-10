CREATE PROCEDURE [dbo].[Letter_GetById]
	@Id int
AS
BEGIN

	SELECT Id, Header, Sender, Content, SendingDate
	FROM dbo.[Letter]
	WHERE Id=@Id

END
