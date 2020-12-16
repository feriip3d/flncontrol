using engenharia.Models.Colaborador;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace engenharia.DAL.ColaboradorDAL
{
    public class ColaboradorDAL
    {
        MySqlPersistence bd = new MySqlPersistence();
        public object ValidarUsuario(string login, string senha)
        {
            string sql = @"select count(*) from colaborador
                           where col_login = @Login and col_senha = @Senha and col_status = 'ativo'";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Login", login);
            parameter.Add("@Senha", senha);

            object retorno = bd.ExecuteSelectScalar(sql, parameter);

            if (retorno == null || Convert.ToInt32(retorno) == 0)
                return false;
            else


                return true;
        }

        public bool Usuarios (string status)
        {
            string sql = @"SELECT COUNT(*) FROM eng2banco.colaborador where col_status = 'Ativo'";

            Int64 qtde_linhas = (Int64)bd.ExecuteSelectScalar(sql, null);

            if (qtde_linhas == 1)
                return false;
            else
                return true;
        }

        public bool ValidarUsuarioEdit(string novologin, string novasenha)
        {
            List<int> listQtde = new List<int>();

            string sql = @"select count(*) from eng2banco.login 
                           where col_login = @Login and col_lsenha = @Senha";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Login", novologin);
            parameter.Add("@Senha", novasenha);

            int qtde_linhas = (int)bd.ExecuteSelectScalar(sql, parameter);

            if (qtde_linhas == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GravarUsuario(string nome, string login, string senha, string cpf, string rg, DateTime data_nasc, string telefone, string email, string cargo, string status, string nivel)
        {
            int aux = 0;
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"insert into colaborador (col_nome, col_login, col_senha, col_cpf, col_rg, col_data_nascimento, col_telefone, col_email, col_cargo, col_status, col_nivel, col_data_adimissao)
                                                    values (@nome, @login, @senha, @cpf, @rg, @data_nasc, @telefone, @email, @cargo, @status, @nivel, @data_adm)";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@nome", nome);
            parameter.Add("@login", login);
            parameter.Add("@senha", senha);
            parameter.Add("@cpf", cpf);
            parameter.Add("@rg", rg);
            parameter.Add("@data_nasc", data_nasc);
            parameter.Add("@telefone", telefone);
            parameter.Add("@email", email);
            parameter.Add("@cargo", cargo);
            parameter.Add("@status", status);
            parameter.Add("@nivel", nivel);
            parameter.Add("@data_adm", DateTime.Today);

            int hasWows = bd.ExecuteNonQuery(sql, parameter);

            if (aux > 0)
                return true;
            else
                return false;
        }
        public bool EditarUsuario(string nome, string login, string senha, string cpf, string rg, DateTime data_nasc, string telefone, string email, string cargo, string status, string nivel, int id)
        {
            int aux = 0;
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"UPDATE colaborador SET
                            col_nome = @nome, col_senha = @senha, col_cpf = @cpf, col_rg = @rg, col_data_nascimento = @data_nasc,
                            col_telefone = @telefone, col_email = @email, col_cargo = @cargo, col_status = @status, col_nivel = @nivel
                            where col_codigo = @ID";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@nome", nome);
            parameter.Add("@ID", id);
            parameter.Add("@login", login);
            parameter.Add("@senha", senha);
            parameter.Add("@cpf", cpf);
            parameter.Add("@rg", rg);
            parameter.Add("@data_nasc", data_nasc);
            parameter.Add("@telefone", telefone);
            parameter.Add("@email", email);
            parameter.Add("@cargo", cargo);
            parameter.Add("@status", status);
            parameter.Add("@nivel", nivel);
            parameter.Add("@data_adm", DateTime.Today);

            int hasWows = bd.ExecuteNonQuery(sql, parameter);

            if (aux > 0)
                return false;
            else
                return true;
        }
        public bool verificarLoginExiste(string login)
        {
            string sql = @"select count(*) from colaborador
                           where col_login = @Login";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Login", login);

            int retorno = Convert.ToInt32(bd.ExecuteSelectScalar(sql, parameter));

            if (retorno == null || retorno > 0)
                return false;
            else
                return true;
        }

        public bool verificarLoginExisteEdit(int id, string login)
        {
            string sql = @"select * from colaborador
                                where col_login = @Login and col_codigo <> @ID";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Login", login);
            parameter.Add("@ID", id);

            int retorno = Convert.ToInt32(bd.ExecuteSelectScalar(sql, parameter));
     
            if (retorno == null || retorno > 0)
                return false;
            else
                return true;
        }

        public bool EditarUsuario(string novologin, string novasenha)
        {
            bool editar = false;

            string sql = @"	UPDATE eng2banco.login SET 
                        col_senha = @novaSenha,
                        col_login = @novoLogin,
                        col_nivel = 'todos',
                        col_status = 'ativo'
                        WHERE log_id = ;";
            //aqui
            return editar;
        }

        public string Gravar(string login, string senha)
        {
            string sql = @"select log_id, log_status count(*) from login where log_acesso = @login";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@login", login);

            object retorno = bd.ExecuteSelectScalar(sql, parameter);

            if (retorno != null && Convert.ToInt32(retorno) != 0)
            {
                return "Já existe um usuario com esse login.";
            }
            else
            {
                sql = @"INSERT INTO eng2banco.login (log_senha, log_acesso, log_nivel_acesso, log_status)
                        VALUES(@senha, @acesso, '1', 'A')";

                return "usuario cadastrado com sucesso";
            }

        }

        public List<Colaborador> Listar()
        {
            List<Colaborador> lista = new List<Colaborador>();

            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.colaborador");

            while (reader.Read())
            {
                Colaborador colaborador = new Colaborador();
                colaborador.id = (int)reader["col_codigo"];
                colaborador.nome = (string)reader["col_nome"];
                colaborador.cpf = (string)reader["col_cpf"];
                colaborador.data_nasc = (DateTime)reader["col_data_nascimento"];
                colaborador.data_admissao = (DateTime)reader["col_data_adimissao"];
                colaborador.rg = (string)reader["col_rg"];
                colaborador.telefone = (string)reader["col_telefone"];
                colaborador.email = (string)reader["col_email"];
                colaborador.cargo = (string)reader["col_cargo"];
                colaborador.senha = (string)reader["col_senha"];
                colaborador.login = (string)reader["col_login"];
                colaborador.status = (string)reader["col_status"];
                colaborador.nivel = (string)reader["col_nivel"];

                lista.Add(colaborador);
            }

            return lista;
        }

        public Colaborador Pegar(string login)
        {
            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.colaborador where col_login like '" + login + "'");
            Colaborador colaborador = null;
            if (reader.Read())
            {
                colaborador = new Colaborador();
                colaborador.id = (int)reader["col_codigo"];
                colaborador.nome = (string)reader["col_nome"];
                colaborador.cpf = (string)reader["col_cpf"];
                colaborador.data_nasc = (DateTime)reader["col_data_nascimento"];
                colaborador.data_admissao = (DateTime)reader["col_data_adimissao"];
                colaborador.rg = (string)reader["col_rg"];
                colaborador.telefone = (string)reader["col_telefone"];
                colaborador.email = (string)reader["col_email"];
                colaborador.cargo = (string)reader["col_cargo"];
                colaborador.senha = (string)reader["col_senha"];
                colaborador.login = (string)reader["col_login"];
                colaborador.status = (string)reader["col_status"];
                colaborador.nivel = (string)reader["col_nivel"];
            }

            return colaborador;
        }

        public void Dispose()
        {
            bd.Close();
        }
    }
}
