CREATE TABLE [dbo].[Product]
(
	[id] INT NOT NULL PRIMARY KEY,
	[naam_nl] NVARCHAR(255) NOT NULL,
	[naam_w] NVARCHAR(255) NOT NULL,
	[prijs] FLOAT NOT NULL,
	[beschrijving] NVARCHAR(255) NOT NULL
)