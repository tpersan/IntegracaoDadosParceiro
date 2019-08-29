using IntegracaoDadosParceiro.Contratos.Exportacao;
using System.Collections.Generic;

namespace IntegracaoDadosParceiro.Contratos.BaseIntegracao
{
    public class Cliente
    {
        public long Documento { get; set; }
        public string Nome { get; set; }
        public long Cnpj { get { return 33608308000173; } }
    }
}
