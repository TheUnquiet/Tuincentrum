BEGIN TRANSACTION;

BEGIN TRY
	IF OBJECT_ID('dbo.offerte_product', 'U') IS NOT NULL
	BEGIN
		DROP TABLE dbo.offerte_product;
	END

	CREATE TABLE [dbo].offerte_product
	(
		[offerte_id] INT NOT NULL,
		[product_id] INT NOT NULL,
		[aantal] INT NOT NULL,
		CONSTRAINT [FK_Offerte_id] FOREIGN KEY ([offerte_id]) REFERENCES [Offerte]([id]),
		CONSTRAINT [FK_Product_id] FOREIGN KEY ([product_id]) REFERENCES Product([id]),
		PRIMARY KEY ([offerte_id], [product_id])
	)

	COMMIT
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH;