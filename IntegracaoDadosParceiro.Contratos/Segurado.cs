using System;
using System.Collections.Generic;

namespace IntegracaoDadosParceiro.Contratos.Exportacao
{

    public class Segurado
    {
        private long Id { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Cbo { get; set; }
        public string Profissao { get; set; }
        private List<string> _email { get; set; }
        public List<string> Email
        {
            get { return _email ?? new List<string>(); }
            set { _email = value; }
        }

        private List<Endereco> _endereco;
        public List<Endereco> Endereco
        {
            get { return _endereco ?? (_endereco = new List<Endereco>()); }
            set { _endereco = value; }
        }

        private List<Telefone> _telefones;
        public List<Telefone> Telefones
        {
            get { return _telefones ?? (_telefones = new List<Telefone>()); }
            set { _telefones = value; }
        }

        private List<Cobertura> _coberturas;
        public List<Cobertura> Coberturas
        {
            get { return _coberturas ?? (_coberturas = new List<Cobertura>()); }
            set { _coberturas = value; }
        }


    }

}
