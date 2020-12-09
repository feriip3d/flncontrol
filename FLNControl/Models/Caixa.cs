using engenharia.Models.CaixaMovimentacao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace engenharia.Models.Caixa
{
    public class Caixa
    {
        public int id { get; set; }
        public DateTime data_abertura { get; set; }
        public DateTime data_fechamento { get; set; }
        public decimal valor_inicial { get; set; }
        public decimal valor_final { get; set; }
        public string status { get; set; }
        public List<Movimentacao> movimentacao { get; set; }
        public Caixa()
        {
        }


    }
}