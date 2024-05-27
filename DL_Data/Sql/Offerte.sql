BEGIN TRANSACTION;

BEGIN TRY
	IF OBJECT_ID('dbo.Offerte', 'U') IS NOT NULL
	BEGIN
		DROP TABLE dbo.Offerte
	END 

	CREATE TABLE [dbo].[Offerte]
	(
		[id] INT NOT NULL PRIMARY KEY IDENTITY,
		[datum] DATE NOT NULL,
		[afhaal] BIT NOT NULL,
		[aanleg] BIT NOT NULL,
		[aantal_producten] INT NOT NULL,
		[klantnummer] INT NOT NULL,

		CONSTRAINT [FK_Klantnummer] FOREIGN KEY ([klantnummer]) REFERENCES [Klant]([id])
	)
	
	COMMIT
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH;