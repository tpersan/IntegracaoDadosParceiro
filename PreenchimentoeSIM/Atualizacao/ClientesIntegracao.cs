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
        internal static bool EstahLah(long cpf)
        {
            var clienteEnviado = 0;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IntegracaoParceiro"].ToString()))
            {
                clienteEnviado = connection.QueryFirst<int>(
                    CONSULTA,
                    new { @cpf = cpf });
            }

            return clienteEnviado > 0;
        }

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

        private const string CONSULTA = @"
            SELECT TOP (1) 1
            FROM Clientes c
               INNER JOIN DadosClientes dc ON c.documento = dc.documento
            WHERE c.documento = @cpf ";
    }
}
