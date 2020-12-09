using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FLNControl.Persistence
{
    public class Database
    {
        private MySqlConnection _connection { get; set; }
        private MySqlCommand _command { get; set; }
        private int _lastId;

        public int LastId
        {
            get => _lastId;
            set => _lastId = value;
        }

        public Database()
        {
            _connection = new MySqlConnection(@"SERVER=den1.mysql6.gear.host;DATABASE=eng2banco;UID=eng2banco;PWD=La4e1iOp3!!6;");
            _command = _connection.CreateCommand();
        }

        public void OpenConnection()
        {
            _connection.Open();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            OpenConnection();
            _command.CommandText = sql;

            if (parameters != null)
                foreach (var parameter in parameters)
                    _command.Parameters.AddWithValue(parameter.Key, parameter.Value);

            int affectedRows = _command.ExecuteNonQuery();
            _lastId = (int)_command.LastInsertedId;
            CloseConnection();

            return affectedRows;
        }

        public MySqlDataReader ExecuteReader(string sql, Dictionary<string, object> parameters = null)
        {
            OpenConnection();
            _command.CommandText = sql;

            if (parameters != null)
                foreach (var parameter in parameters)
                    _command.Parameters.AddWithValue(parameter.Key, parameter.Value);

            MySqlDataReader dataReader = _command.ExecuteReader();

            return dataReader;
        }
    }
}