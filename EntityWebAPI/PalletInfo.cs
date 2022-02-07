using System;
using System.Collections.Generic;
using System.Text;

namespace EntityWebAPI
{
    public class PalletInfo 
    {
        public int CodPallet { get; set; }
        public string Caderno { get; set; }
        public string TipoPallet { get; set; }
        public int QtdeExemplares { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Operador { get; set; }
        public int StatusPallet { get; set; }
        public int NumOrdem { get; set; }//Chave Estrangeira
        public int Atividade { get; set; }//Chave Estrangeira
    }
}
