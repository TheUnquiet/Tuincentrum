CREATE TABLE [dbo].[Bestelling]
(
	[offerte_id] INT NOT NULL,
	[product_id] INT NOT NULL,
	[aantal] INT NOT NULL,
	CONSTRAINT [FK_Bestelling_Offerte] FOREIGN KEY ([offerte_id]) REFERENCES [Offerte]([id]),
	CONSTRAINT [FK_Bestelling_Product] FOREIGN KEY ([product_id]) REFERENCES Product([id]),
	PRIMARY KEY ([offerte_id], [product_id])
)