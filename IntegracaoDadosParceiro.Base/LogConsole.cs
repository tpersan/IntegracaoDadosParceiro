using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoDadosParceiro.Base
{
    public static class LogConsole
    {
        private static void Logar(ConsoleColor cor, string mensagem)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
        }
        public static void Mensagem(string mensagem)
        {
            Logar(ConsoleColor.White, mensagem);
        }
        public static void Aviso(string mensagem)
        {
            Logar(ConsoleColor.Yellow, mensagem);
        }
        public static void Erro(string mensagem)
        {
            Logar(ConsoleColor.Red, mensagem);
        }

    }
}
