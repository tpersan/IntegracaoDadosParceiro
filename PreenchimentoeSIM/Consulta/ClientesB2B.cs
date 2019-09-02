using Dapper;
using IntegracaoDadosParceiro.Contratos.BaseIntegracao;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace PreenchimentoeSIM.Consulta
{
    internal static class ClientesB2B
    {
        internal static IEnumerable<Cliente> Obter(int pagina, int limite)
        {
            var CNPJ = ConfigurationManager.AppSettings["CNPJ"];
            IEnumerable<Cliente> data = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalB2B"].ToString()))
            {
                data =  connection.Query<Cliente>(COMANDO, 
                    new {
                        @cnpj = CNPJ,
                        @itensOff = (pagina-1) * limite,
                        @totalItens = limite
                    });
            }

            return data;
        }

        private const string COMANDO = @"
            SELECT DISTINCT Segurado.Id,
	                Segurado.Documento, 
	                Segurado.Nome 
            FROM PESSOA Segurado
            	INNER JOIN Inscricao I ON I.Segurado_PessoaId = Segurado.Id
            WHERE Segurado.TipoDocumento = 'CPF' 
              AND Segurado.Documento > 0
			ORDER BY Segurado.Id
			OFFSET @itensOff ROWS
            FETCH NEXT @totalItens ROWS ONLY  ";

    }
}
