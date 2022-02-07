using System;
using System.Collections.Generic;
using System.Text;

namespace EntityWebAPI
{
    public class ClienteInfo
    {
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Status { get; set; }
    }
}
