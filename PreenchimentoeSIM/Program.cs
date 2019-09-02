using IntegracaoDadosParceiro.Base;
using IntegracaoDadosParceiro.Base.Extensao;
using IntegracaoDadosParceiro.Contratos.BaseIntegracao;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PreenchimentoeSIM.Atualizacao;
using PreenchimentoeSIM.Consulta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreenchimentoeSIM
{
    class Program
    {

        static void Main(string[] args)
        {
            var totalAtual = 500000;

            var pagina = 1;
            var itens = 300;

            while ((pagina * itens) <= totalAtual)
            {
                LogConsole.Mensagem($"{DateTime.Now} - Etapa 1 - Obtendo Clientes do B2B - Página {pagina}");
                var clientesDoB2b = ClientesB2B.Obter(pagina, itens).ToList();

                LogConsole.Mensagem($"{DateTime.Now} - Etapa 2 - Obtendo clientes que ainda não foram migrados");
                var clientesMigrados = ClientesIntegracao.EstahLah(clientesDoB2b.Select(c => c.Documento).ToList());

                var clientes = clientesDoB2b.Where(c => !clientesMigrados.Any(cm => cm == c.Documento));
                LogConsole.Mensagem($"{DateTime.Now} - Etapa 3 - Carga de Clientes na base de Integracao - Página: {pagina} - Itens: {clientes.Count()}");

                foreach (var doCliente in clientes)
                {
                    try
                    {
                        ClientesIntegracao.CriarCliente(doCliente);
                        var json = ObterDadosDoCliente(doCliente.Documento);
                        DadosClientesIntegracao.CriarDadosCliente(DeParaDadosCliente(doCliente, json));
                    }
                    catch (Exception e)
                    {
                        LogConsole.Erro($"Cliente: {doCliente.Documento} - {e.Message}");
                    }

                }

                LogConsole.Aviso($"{DateTime.Now} - RESULTADO - Feitos {(pagina * itens)} de {totalAtual}");


                pagina++;

                LogConsole.Mensagem("-------------------");
            }


            Console.ReadKey();

        }

        private static DadosCliente DeParaDadosCliente(Cliente doCliente, string json)
        {
            return new DadosCliente
            {
                DadosDisponiveis = json,
                Documento = doCliente.Documento
            };
        }

        private static string ObterDadosDoCliente(long cpf)
        {
            var segurado = DadosClientesB2B.Obter(cpf);

            if (segurado == null)
                throw new Exception("Cliente não encontrado no B2B");

            return JsonConvert.SerializeObject(segurado, Formatting.Indented, settings: new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy",
                NullValueHandling = NullValueHandling.Ignore,
            });
        }
    }
}
