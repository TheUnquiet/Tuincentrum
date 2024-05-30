BEGIN TRANSACTION;

BEGIN TRY

	INSERT INTO offerte_product(offerte_id, product_id, aantal) VALUES(@offerte_id, @product_id, @aantal)

	COMMIT
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH;