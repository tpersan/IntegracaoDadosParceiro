
CREATE PROCEDURE [dbo].[PInserirDadosCliente]
	@documento bigint,
	@documentoOrigem bigint,
	@sistemaOrigem varchar(20),
	@dadosDisponiveis varchar(max)
AS

    DECLARE @existe BIT = 0;

    SELECT TOP(1)
        @existe = 1
    FROM DADOSCLIENTES C
	WHERE C.DOCUMENTO = @documento
	  AND C.DOCUMENTOORIGEM = @documentoOrigem;

	IF (ISNULL(@existe, 0) = 0)
    BEGIN
	   INSERT INTO DADOSCLIENTES 
			(DOCUMENTO, DOCUMENTOORIGEM, SISTEMAORIGEM, DADOSDISPONIVEIS, DATACRIACAO)
	   VALUES 
			(@documento, @documentoOrigem, @sistemaOrigem, @dadosDisponiveis, GetDate());
	END;

RETURN 0
