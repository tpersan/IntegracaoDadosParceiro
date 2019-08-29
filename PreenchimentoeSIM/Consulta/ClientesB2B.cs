using Dapper;
using IntegracaoDadosParceiro.Contratos.BaseIntegracao;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace PreenchimentoeSIM.Consulta
{
    internal static class ClientesB2B
    {
        internal static IEnumerable<Cliente> Obter()
        {
            var CNPJ = ConfigurationManager.AppSettings["CNPJ"];
            IEnumerable<Cliente> data = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalB2B"].ToString()))
            {
                data =  connection.Query<Cliente>(COMANDO, new { @cnpj = CNPJ });
            }

            return data;
        }

        private const string COMANDO = @"
            SELECT DISTINCT
	            Segurado.Documento, 
	            Segurado.Nome 
            FROM PESSOA Segurado
            	INNER JOIN Inscricao I ON I.Segurado_PessoaId = Segurado.Id
            WHERE Segurado.TipoDocumento = 'CPF' ";

    }
}
