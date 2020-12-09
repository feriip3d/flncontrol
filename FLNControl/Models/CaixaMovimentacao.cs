using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace engenharia.Models.CaixaMovimentacao
{
    public class Movimentacao
    {
        public int caixa_id { get; set; }
        public int colaborador_id { get; set; }
        public DateTime data { get; set; }
        public decimal valor { get; set; }
        public string motivo { get; set; }
        public string tipo { get; set; }

        public Movimentacao()
        {
        }


    }
}