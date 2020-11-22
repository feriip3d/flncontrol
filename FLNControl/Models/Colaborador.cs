using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace engenharia.Models.Colaborador
{
    public class Colaborador
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public int dependentes { get; set; }
        public DateTime data_nasc { get; set; }
        public DateTime data_admissao { get; set; }
        public DateTime data_demissao { get; set; }
        public string rg { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string cargo { get; set; }
        public string senha { get; set; }
        public string login { get; set; }
        public string status { get; set; }
        public string nivel { get; set; }

        public Colaborador()
        {
        }


    }
}