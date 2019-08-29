using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoDadosParceiro.Base.Extensao
{
    public static class ExtensaoIList
    {
        public static T Find<T>(this IList<T> lista, Func<T, bool> pesquisa)
        {
            return lista.FirstOrDefault(pesquisa);
        }

        public static List<List<T>> Fatiar<T>(this List<T> source, int tamanhoDaFatia)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / tamanhoDaFatia)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
