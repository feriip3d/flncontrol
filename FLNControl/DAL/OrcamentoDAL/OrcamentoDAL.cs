using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL;
using FLNControl.Models;
using FLNControl.Models.Orcamento;
using MySql.Data.MySqlClient;

namespace FLNControl.DAL.OrcamentoDAL
{
    public class OrcamentoDAL
    {
        MySqlPersistence bd = new MySqlPersistence();
        public List<Produto> ListarProduto()
        {
            List<Produto> lista = new List<Produto>();

            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.produto ");

            while (reader.Read())
            {
                Produto produto = new Produto();
                produto.Id = (int)reader["pro_codigo"];
                produto.Categoria = (string)reader["pro_categoria"];
                produto.Descricao = (string)reader["pro_descricao"];
                produto.ValorVenda = (decimal)reader["pro_valor_venda"];
                produto.Marca = (string)reader["pro_marca"];
                lista.Add(produto);
            }

                return lista;
        }

        public List<Orcamento> ListarOrcamento()
        {
            List<Orcamento> lista = new List<Orcamento>();

            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.orcamento ");

            while (reader.Read())
            {
                Orcamento orcamento = new Orcamento();
                orcamento.Id = (int)reader["orc_codigo"];
                orcamento.Valor_Total = (decimal)reader["orc_valor_total"];
                orcamento.Data_Validade = (DateTime)reader["orc_data_validae"];
                orcamento.cli_Id = (int)reader["cli_codigo"];
                orcamento.Valor_Desconto = (decimal)reader["orc_valor_desconto"];
                lista.Add(orcamento);
            }

            return lista;
        }

        public List<Cliente> ListarCliente()
        {
            List<Cliente> lista = new List<Cliente>();

            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.cliente");

            while (reader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Codigo = (int)reader["cli_codigo"];
                cliente.Nome = (string)reader["cli_nome"];
                cliente.Cpf = (string)reader["cli_cpf"];
                cliente.Telefone = (string)reader["cli_telefone"];
                cliente.DataNascimento = (string)reader["cli_data_nascimento"].ToString();
                cliente.Fiado = Convert.ToInt32(reader["cli_fiado"]);
                cliente.ValorLimiteFiado = Convert.ToInt32(reader["cli_valor_limite_fiado"]);

                lista.Add(cliente);
            }

            return lista;
        }

        public bool Excluir(int codigo)
        {
            
            string sql = @"DELETE FROM eng2banco.orcamento
                            WHERE orc_codigo = @Codigo";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Codigo", codigo);

            object retorno = bd.ExecuteSelectScalar(sql, parameter);

            if (retorno == null || Convert.ToInt32(retorno) == 0)
                return true;
            else
                return false;
        }
    }
}
