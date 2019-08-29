using IntegracaoDadosParceiro.Contratos.BaseIntegracao;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace PreenchimentoeSIM.Atualizacao
{
    public static class DadosClientesIntegracao
    {
        internal static void CriarDadosCliente(DadosCliente cliente)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IntegracaoParceiro"].ToString()))
            {
                connection.Execute(
                    sql: COMANDO,
                    param: new {
                        documento = cliente.Documento,
                        documentoOrigem = cliente.DocumentoOrigem,
                        sistemaOrigem = cliente.SistemaOrigem,
                        dados = cliente.DadosDisponiveis
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private const string COMANDO = @"PInserirDadosCliente 
                                            @documento 
                                            @documentoOrigem 
                                            @sistemaOrigem 
                                            @dados ";
    }
}
