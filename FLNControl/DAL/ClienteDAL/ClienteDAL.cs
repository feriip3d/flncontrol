using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL;
using FLNControl.Models;

namespace FLNControl.DAL.ClienteDAL
{
    public class ClienteDAL
    {
        MySqlPersistence bd = new MySqlPersistence();

        public List<Cliente> Mapeamento(DbDataReader dr)
        {
            List<Cliente> clientes = new List<Cliente>();

            while (dr.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Codigo = Convert.ToInt32(dr["cli_codigo"].ToString());
                cliente.Nome = dr["cli_nome"].ToString();
                cliente.Cpf = dr["cli_cpf"].ToString();
                cliente.Telefone = dr["cli_telefone"].ToString();
                cliente.Email = dr["cli_email"].ToString();
                cliente.DataNascimento = dr["cli_data_nascimento"].ToString();
                cliente.Fiado = Convert.ToInt32(dr["cli_fiado"].ToString());
                cliente.ValorLimiteFiado = Convert.ToDouble(dr["cli_valor_limite_fiado"].ToString());
                cliente.DiasVencimento = Convert.ToInt32(dr["cli_data_vencimento"].ToString());

                clientes.Add(cliente);
            }
            bd.Close();

            return clientes;
        }
        public List<Cliente> ListarTodosCliente()
        {
            
            string sql = "select * from cliente";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Cliente> ListarClientesPorNome(string nome)
        {
            string sql = "select * from cliente where cli_nome like '"+nome+"%'";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Cliente> ListarClientesPorCPF(string cpf)
        {
            string sql = "select * from cliente where cli_cpf like '" + cpf +"%'";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Cliente> ListarClientesPorTelefone(string telefone)
        {
            string sql = "select * from cliente where cli_telefone like '" + telefone + "%'";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public Cliente BuscarClientesPorCodigo(int codigo)
        {
            string sql = "select * from cliente where cli_codigo like '" + codigo + "'%";

            DbDataReader dr = bd.ExecuteSelect(sql);

            Cliente cliente = new Cliente();
            cliente.Codigo = Convert.ToInt32(dr["cli_codigo"].ToString());
            cliente.Nome = dr["cli_nome"].ToString();
            cliente.Cpf = dr["cli_cpf"].ToString();
            cliente.Telefone = dr["cli_telefone"].ToString();
            cliente.Email = dr["cli_email"].ToString();
            cliente.DataNascimento = dr["cli_data_nascimento"].ToString();
            cliente.Fiado = Convert.ToInt32(dr["cli_fiado"].ToString());
            cliente.ValorLimiteFiado = Convert.ToDouble(dr["cli_valor_limite_fiado"].ToString());
            cliente.DiasVencimento = Convert.ToInt32(dr["cli_data_vencimento"].ToString());

            return cliente;
        }
        public bool GravarClienteCompleto(Cliente novo)
        {
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"INSERT INTO cliente
                                        (cli_nome,
                                        cli_cpf,
                                        cli_telefone,
                                        cli_email,
                                        cli_data_nascimento,
                                        cli_fiado,
                                        cli_valor_limite_fiado,
                                        cli_data_vencimento)
                                        VALUES
                                        (@nome,
                                        @cpf,
                                        @telefone,
                                        @email,
                                        @dataNascimento,
                                        @Fiado,
                                        @valorLimiteFiado,
                                        @dataVencimento)";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@nome", novo.Nome);
            parameter.Add("@cpf", novo.Cpf);
            parameter.Add("@telefone", novo.Telefone);
            parameter.Add("@email", novo.Email);
            parameter.Add("@dataNascimento", novo.DataNascimento);
            parameter.Add("@Fiado", novo.Fiado);
            parameter.Add("@valorLimiteFiado", novo.ValorLimiteFiado);
            parameter.Add("@dataVencimento", novo.DiasVencimento);

            bd.ExecuteNonQuery(sql, parameter);

            novo.Codigo = bd.UltimoID;

            return novo.Codigo == 0;
        }
        public bool ExcluirCliente(int codigo)
        {
            string sql = "delete cliente where cli_codigo" + codigo;

            return (bd.ExecuteNonQuery(sql) > 0);

        }
        public bool AlterarClienteCompleto(Cliente novo)
        {
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"UPDATE cliente SET
                                        cli_nome = @nome,
                                        cli_cpf = @cpf,
                                        cli_telefone = @telefone,
                                        cli_email = @email,
                                        cli_data_nascimento = @dataNascimento,
                                        cli_fiado = @Fiado,
                                        cli_valor_limite_fiado = @valorLimiteFiado,
                                        cli_data_vencimento = @dataVencimento
                                        WHERE cli_codigo = @codigo";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@codigo", novo.Codigo);
            parameter.Add("@nome", novo.Nome);
            parameter.Add("@cpf", novo.Cpf);
            parameter.Add("@telefone", novo.Telefone);
            parameter.Add("@email", novo.Email);
            parameter.Add("@dataNascimento", novo.DataNascimento);
            parameter.Add("@Fiado", novo.Fiado);
            parameter.Add("@valorLimiteFiado", novo.ValorLimiteFiado);
            parameter.Add("@dataVencimento", novo.DataNascimento);

            return bd.ExecuteNonQuery(sql, parameter) != 0;
        }
    }
}
