using Dapper;
using IntegracaoDadosParceiro.Contratos.Exportacao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PreenchimentoeSIM.Consulta
{
    internal static class ClienteB2B
    {
        internal static Segurado Obter(long cpf)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalB2B"].ToString()))
            {
                var resultados = connection.QueryMultiple(COMANDO, new { @CPF = cpf });

                var segurado = resultados.Read<Segurado>().FirstOrDefault();

                segurado.Endereco.AddRange(resultados.Read<Endereco>());
                segurado.Email.AddRange(resultados.Read<string>());
                segurado.Telefones.AddRange(resultados.Read<Telefone>());
                segurado.Coberturas.AddRange(resultados.Read<Cobertura>());

                return segurado;
            }

        }

        private const string COMANDO = @"
SELECT Segurado.Id, Segurado.Nome, Segurado.Documento, Segurado.DataNascimento, Segurado.Sexo
	, Segurado.EstadoCivil, Segurado.Cbo, Segurado.Profissao
FROM PESSOA Segurado
WHERE Segurado.TipoDocumento = 'CPF'
  AND Segurado.Documento = @cpf;

SELECT DISTINCT 
   Logradouro, NumeroLogradouro, Bairro, Cidade, UF, CEP, Complemento
FROM PESSOA Segurado
	INNER JOIN Inscricao I ON I.Segurado_PessoaId = Segurado.Id
	INNER JOIN Relacionamento ON Relacionamento.PropostaId = i.PropostaId
WHERE Segurado.TipoDocumento = 'CPF' AND Segurado.Documento = @cpf;

SELECT DISTINCT 
   Email 
FROM PESSOA Segurado
	INNER JOIN Inscricao I ON I.Segurado_PessoaId = Segurado.Id
	INNER JOIN Relacionamento ON Relacionamento.PropostaId = i.PropostaId
WHERE Segurado.TipoDocumento = 'CPF' AND Segurado.Documento = @cpf;

SELECT DISTINCT 
   Ddd, Telefone as Numero
FROM PESSOA Segurado
	INNER JOIN Inscricao I ON I.Segurado_PessoaId = Segurado.Id
	INNER JOIN Relacionamento ON Relacionamento.PropostaId = i.PropostaId
WHERE Segurado.TipoDocumento = 'CPF' AND Segurado.Documento = @cpf;

SELECT
	 SUM(CC.CAPITAL) AS Capital
	, CC.InicioVigencia
	, AC.Descricao AS TipoCobertura
	, CASE WHEN (CC.DataCancelamento IS NOT NULL) THEN 'ATIVO' ELSE 'CANCELADO' END AS Status

FROM PESSOA Segurado
	INNER JOIN INSCRICAO I ON I.Segurado_PESSOAID = Segurado.ID 
	INNER JOIN CoberturaContratada CC ON CC.InscricaoId = I.Id
	INNER JOIN CoberturaAgrupadorCobertura CAC ON CAC.CoberturaId = CC.CoberturaId
	INNER JOIN AgrupadorCobertura AC ON AC.Id = CAC.AgrupadorCoberturaId
WHERE Segurado.TIPODOCUMENTO = 'CPF'
  AND Segurado.DOCUMENTO = @cpf
GROUP BY InicioVigencia, Descricao, DataCancelamento

";

    }
}
