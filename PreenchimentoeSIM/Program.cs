using IntegracaoDadosParceiro.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            var pessoas = ObterPessoasDoB2B();


            //if (args?.Count <= 0)
            //    throw new InvalidOperationException("Deve possuir o parametro CPF");

            //var cpf = args[0];

            //LogConsole.Mensagem("CPF: ");
            //var cpfDigitado = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(cpfDigitado))
            //    throw new InvalidOperationException("CPF Vazio!");

            //var cpf = Convert.ToInt64(cpfDigitado);
            long cpf = 90715470272;

            if (cpf <= 0)
                throw new InvalidOperationException("CPF Mals!");

            var segurado = ClienteB2B.Obter(cpf);

            if (segurado != null)
            {
                LogConsole.Mensagem("Resultado:");

                LogConsole.Mensagem(JsonConvert.SerializeObject(segurado, Formatting.Indented, settings: new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy",
                    NullValueHandling = NullValueHandling.Ignore,
                }));
            }

            Console.ReadKey();

        }
    }
}
