using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace engenharia.DAL
{
    public class MySqlPersistence
    {
        public MySqlConnection conexao { get; set; }
        public MySqlCommand cmd { get; set; }
        public int UltimoID { get => ultimoID; set => ultimoID = value; }

        public string constr = "SERVER=den1.mysql6.gear.host;DATABASE=eng2banco;UID=eng2banco;PWD=La4e1iOp3!!6;";

        int ultimoID;

        public MySqlPersistence()
        {
            string STRCON = Environment.GetEnvironmentVariable("STRCON");
            conexao = new MySqlConnection(STRCON);
            cmd = conexao.CreateCommand();

        }

        public void Abrir()
        {
            try
            {
                conexao = new MySqlConnection(constr);
                conexao.Open();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            conexao.Close();
        }

        /// <summary>
        /// Executa um comando INSERT, SELECT DELETE or Stored Procedure
        /// </summary>
        /// <param name="sql">Comando SQL</param>
        /// <returns>Quantidade de Linhas Afetadas</returns>
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parametros = null)
        {
            Abrir();
            cmd = new MySqlCommand(sql, conexao);
            cmd.CommandText = sql;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            int qtdLinhasAfetadas = cmd.ExecuteNonQuery();
            UltimoID = (int)cmd.LastInsertedId;

            Close();
            return qtdLinhasAfetadas;
        }

        public object ExecuteSelectScalar(string select, Dictionary<string, object> parametros = null)
        {
            object valor = null;

            Abrir();
            cmd = new MySqlCommand(select, conexao);
            cmd.CommandText = select;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            valor = cmd.ExecuteScalar();

            Close();

            return valor;
        }
        public bool HasRows(string select, Dictionary<string, object> parameter = null)
        {
            Abrir();
            cmd = new MySqlCommand(select, conexao);

            int aux = 0;
            if (parameter != null)
            {
                foreach (var p in parameter)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }
            MySqlDataReader dr = cmd.ExecuteReader();

            Close();

            return dr.HasRows;
        }

        public MySqlDataReader ExecuteSelect(string select, Dictionary<string, object> parameter = null)
        {
            Abrir();
            cmd = new MySqlCommand(select, conexao);

            if (parameter != null)
            {
                foreach (var p in parameter)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            MySqlDataReader leitorDados = cmd.ExecuteReader();

            return leitorDados;
        }
    }
}
