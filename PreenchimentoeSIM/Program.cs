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

            LogConsole.Mensagem("Iniciando a Etapa 1 - Obtendo Clientes do B2B");
            var clientes = ClientesB2B.Obter().ToList();

            LogConsole.Mensagem("Iniciando a Etapa 2 - Carga de Clientes na base de Integracao");

            Parallel.ForEach(clientes, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 4 }, doCliente =>
            //foreach (var doCliente in clientes)
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

            });

            LogConsole.Mensagem("Iniciando a Etapa 1 - Carga de Clientes do B2B");

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
