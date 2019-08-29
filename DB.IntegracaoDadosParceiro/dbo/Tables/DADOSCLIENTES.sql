﻿CREATE TABLE [dbo].[DADOSCLIENTES] (
    [ID]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [DOCUMENTO]        BIGINT        NOT NULL,
    [DOCUMENTOORIGEM]  BIGINT        NOT NULL,
    [SISTEMAORIGEM]    VARCHAR (30)  NULL,
    [DADOSDISPONIVEIS] VARCHAR (MAX) NOT NULL,
    [DATACRIACAO]      DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

