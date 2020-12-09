using engenharia.Models.CaixaMovimentacao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace engenharia.Models.Post
{
    public class Comentario
    {
        public int _id { get; set; }
        public string _texto { get; set; }
        public string _usuario { get; set; }

        public Comentario()
        {
        }


    }
}
