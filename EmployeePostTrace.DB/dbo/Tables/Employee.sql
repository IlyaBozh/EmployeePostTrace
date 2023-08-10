CREATE TABLE [dbo].[Employee]
(
	[Id]               INT IDENTITY(1,1)     NOT NULL PRIMARY KEY,
	[FirstName]        NCHAR (50)            NOT NULL,
    [LastName]         NCHAR (50)            NOT NULL,
    [Patronymic]       NCHAR (50)            NOT NULL,
	[Email]            NCHAR (50)            NOT NULL,
	[Password]         NCHAR (255)           NOT NULL,
	[IsDeleted]        BIT DEFAULT 0         NOT NULL
)
