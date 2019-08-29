using IntegracaoDadosParceiro.Contratos.Exportacao;
using System.Collections.Generic;

namespace IntegracaoDadosParceiro.Contratos.BaseIntegracao
{
    public class DadosCliente
    {
        public long Documento { get; set; }

        public long DocumentoOrigem { get { return 33608308000173; } }

        public string SistemaOrigem { get { return "B2B"; } }

        public string DadosDisponiveis { get; set; }

    }
}
