using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.Models.Colaborador;

namespace FLNControl.Models.Orcamento
{
    public class Orcamento
    {
        public int Id { get; set; }
        public decimal Valor_Total { get; set; }
        public DateTime Data_Validade { get; set; }
        public int col_Id { get; set; }
        public int cli_Id { get; set; }
        public decimal Valor_Desconto { get; set; }
        public string nomeCliente { get; set; }


    }
}
