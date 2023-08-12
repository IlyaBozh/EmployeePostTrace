CREATE TABLE [dbo].[Letter]
(
	[Id]             INT IDENTITY(1,1)     NOT NULL PRIMARY KEY,
	[Header]         NCHAR (50)            NOT NULL,
	[Sender]         NCHAR (50)            NOT NULL,
	[Recipient]      NCHAR (50)            NOT NULL,
	[Content]        NCHAR (50)            NOT NULL,
	[SendingDate]    DATETIME              NOT NULL,
	[SenderId]       INT                   NOT NULL,
	[RecipientId]    INT                   NOT NULL,
	[IsDeleted]      BIT DEFAULT 0         NOT NULL,
	FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Employee] ([Id]),
	FOREIGN KEY ([RecipientId]) REFERENCES [dbo].[Employee] ([Id])
)
