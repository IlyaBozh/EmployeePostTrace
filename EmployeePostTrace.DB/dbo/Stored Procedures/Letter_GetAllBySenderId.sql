CREATE PROCEDURE [dbo].[Letter_GetAllBySenderId]
	@SenderId int
AS
BEGIN

	SELECT Id, Header, SendingDate
	FROM dbo.[Letter]
	WHERE SenderId=@SenderId AND IsDeleted=0

END
