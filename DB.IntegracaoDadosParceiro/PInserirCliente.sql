CREATE PROCEDURE [dbo].[PInserirCliente]
	@documento bigint,
	@nome varchar(200),
	@cnpj bigint
AS

    DECLARE @existe BIT = 0;

    SELECT TOP(1)
        @existe = 1
    FROM CLIENTES C
	WHERE C.DOCUMENTO = @documento
	  AND C.DOCUMENTOORIGEM = @cnpj;

	IF (ISNULL(@existe, 0) = 0)
    BEGIN	
	   INSERT INTO CLIENTES (DOCUMENTO, DOCUMENTOORIGEM, NOME)
	   VALUES (@documento, @cnpj, @nome)
	END;

RETURN 0
