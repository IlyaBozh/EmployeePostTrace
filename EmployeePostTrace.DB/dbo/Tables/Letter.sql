﻿CREATE TABLE [dbo].[Letter]
(
	[Id]             INT IDENTITY(1,1)     NOT NULL PRIMARY KEY,
	[Header]         NCHAR (50)            NOT NULL,
	[Sender]         NCHAR (50)            NOT NULL,
	[Recipient]      NCHAR (50)            NOT NULL,
	[Content]        NCHAR (50)            NOT NULL,
	[SendingDate]    DATETIME              NOT NULL,
	[IsIncoming]     BIT                   NOT NULL,
	[EmployeeId]     INT                   NOT NULL,
	[IsDelete]       BIT DEFAULT 0         NOT NULL
)
