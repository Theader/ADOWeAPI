using System;
using System.Collections.Generic;
using System.Text;

namespace EntityWebAPI
{
    public class OpInfo
    {
        public int NumOrdem { get; set; }
        public string Produto { get; set; }
        public int Atividade { get; set; }
        public DateTime DataCadastro { get; set; }
        public int StatusOp { get; set; }
        public int CodCliente { get; set; } //Chave Estrangeira
    }
}
