using IntegracaoDadosParceiro.Contratos.BaseIntegracao;

using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace PreenchimentoeSIM.Atualizacao
{
    public static class ClientesIntegracao
    {
        internal static void CriarCliente(Cliente cliente)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IntegracaoParceiro"].ToString()))
            {
                connection.Execute(
                    "PInserirCliente",
                    new { cliente.Documento, cliente.Nome, cliente.Cnpj },
                    //new { documento = cliente.Documento, nome = cliente.Nome, cnpj = cliente.Cnpj },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private const string COMANDO = @"EXEC PInserirCliente @documento, @nome, @cnpj";
    }
}
