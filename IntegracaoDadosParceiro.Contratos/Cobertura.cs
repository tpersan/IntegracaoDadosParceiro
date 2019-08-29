using System;

namespace IntegracaoDadosParceiro.Contratos.Exportacao
{
    public class Cobertura
    {
        public decimal Capital { get; set; }
        public DateTime InicioVigencia { get; set; }
        public string TipoCobertura { get; set; }
        public string Status { get; set; }
    }

}
