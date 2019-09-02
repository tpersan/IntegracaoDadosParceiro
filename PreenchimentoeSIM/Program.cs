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
            var itens = 1000;

            while ((pagina * itens) <= totalAtual)
            {
                LogConsole.Mensagem($"{DateTime.Now} - Iniciando a Etapa 1 - Obtendo Clientes do B2B - Página {pagina}");
                var clientes = ClientesB2B.Obter(pagina, itens).ToList();

                LogConsole.Mensagem($"Iniciando a Etapa 2 - Carga de Clientes na base de Integracao - Página: {pagina} - Itens: {clientes.Count()}");

                foreach (var doCliente in clientes)
                {
                    if (ClientesIntegracao.EstahLah(doCliente.Documento))
                        continue;

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

                LogConsole.Aviso($"Feitos {(pagina * itens)} de {totalAtual}");
                

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
