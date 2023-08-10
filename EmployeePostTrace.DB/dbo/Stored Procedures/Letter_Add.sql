CREATE PROCEDURE [dbo].[Letter_Add]
	@Header nvarchar(50),
	@Sender nvarchar(50),
	@Recipient nvarchar(50),
	@Content nvarchar(50),
	@SenderId tinyint,
	@RecipientId tinyint
AS
BEGIN
	INSERT INTO dbo.[Letter](
		Header,
		Sender,
		Recipient,
		Content,
		SendingDate,
		SenderId,
		RecipientId)
	VALUES(
		@Header,
		@Sender,
		@Recipient,
		@Content,
		GETDATE(),
		@SenderId,
		@RecipientId)

	SELECT @@IDENTITY
END
