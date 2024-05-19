CREATE TABLE [dbo].[Offerte]
(
	[id] INT NOT NULL PRIMARY KEY,
	[datum] DATE NOT NULL,
	[afhaal] BIT NOT NULL,
	[aanleg] BIT NOT NULL,
	[aantal_producten] INT NOT NULL

	CONSTRAINT [FK_Klantnummer] FOREIGN KEY ([klantnummer]) REFERENCES [Klant]([id])

)