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

            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.produto");

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
        public List<Produto> ListarProdutoDoOrcamento(int codigoDoOrcamento)
        {
            List<Produto> lista = new List<Produto>();

            MySqlDataReader reader = bd.ExecuteSelect(@"
                                                        SELECT p.pro_codigo, p.pro_categoria, p.pro_descricao, p.pro_valor_venda, p.pro_marca
                                                        FROM eng2banco.produto p 
                                                        INNER JOIN eng2banco.produtos_orcamento po on po.pro_codigo = p.pro_codigo 
                                                                                                and po.orc_codigo = "+ codigoDoOrcamento);

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

            MySqlDataReader reader = bd.ExecuteSelect(@"SELECT * FROM eng2banco.orcamento o
                                                        inner join eng2banco.cliente c on c.cli_codigo = o.cli_codigo");

            while (reader.Read())
            {
                Orcamento orcamento = new Orcamento();
                orcamento.Id = (int)reader["orc_codigo"];
                orcamento.Valor_Total = (decimal)reader["orc_valor_total"];
                orcamento.Data_Validade = (DateTime)reader["orc_data_validae"];
                orcamento.cli_Id = (int)reader["cli_codigo"];
                orcamento.Valor_Desconto = (decimal)reader["orc_valor_desconto"];
                orcamento.nomeCliente = (string)reader["cli_nome"];
                lista.Add(orcamento);
            }

            return lista;
        }

        public Orcamento selectOrcamento(int orc_codigo)
        {
            string sql = @"SELECT * FROM eng2banco.orcamento o
                                                        inner join eng2banco.cliente c on c.cli_codigo = o.cli_codigo
                                                        where orc_codigo = @orc_codigo";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@orc_codigo", orc_codigo);

            MySqlDataReader reader = bd.ExecuteSelect(sql, parameter);

            Orcamento orcamento = new Orcamento();
            if (reader.Read())
            {
                orcamento.Id = (int)reader["orc_codigo"];
                orcamento.Valor_Total = (decimal)reader["orc_valor_total"];
                orcamento.Data_Validade = (DateTime)reader["orc_data_validae"];
                orcamento.cli_Id = (int)reader["cli_codigo"];
                orcamento.Valor_Desconto = (decimal)reader["orc_valor_desconto"];
                orcamento.nomeCliente = (string)reader["cli_nome"];
            }
            return orcamento;
        }

        public List<Produto> selectProduto(int orc_codigo)
        {
            string sql = @"SELECT * FROM eng2banco.produto p
                                                inner join eng2banco.produtos_orcamento po on po.pro_codigo = p.pro_codigo
                                    where orc_codigo = @orc_codigo";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@orc_codigo", orc_codigo);

            MySqlDataReader reader = bd.ExecuteSelect(sql, parameter);

            List<Produto> listProd = new List<Produto>();
            while (reader.Read())
            {
                Produto produto = new Produto();
                produto.Id = (int)reader["pro_codigo"];
                produto.Descricao = (string)reader["pro_descricao"];
                produto.quantidade = (int)reader["pro_orc_quantidade"];
                produto.ValorVenda = (decimal)reader["pro_orc_valor"];

                listProd.Add(produto);
            }
            return listProd;
        }

        public Cliente selectCliente(int cli_codigo)
        {
            string sql = @"SELECT * FROM eng2banco.cliente 
                                        where cli_codigo = @cli_codigo";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@cli_codigo", cli_codigo);

            MySqlDataReader reader = bd.ExecuteSelect(sql, parameter);

            Cliente cliente = new Cliente();
            if (reader.Read())
            {
                cliente.Codigo = (int)reader["cli_codigo"];
                cliente.Nome = (string)reader["cli_nome"];
            }
            return cliente;
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

        public int save(Orcamento orcamento)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            sql = @"INSERT INTO orcamento
                                    (
                                    orc_valor_total,
                                    orc_data_validae,
                                    col_codigo,
                                    cli_codigo,
                                    orc_valor_desconto)
                                    VALUES
                                    (
                                    @orc_valor_total,
                                    @orc_data_validae,
                                    @col_codigo,
                                    @cli_codigo,
                                    @orc_valor_desconto);";


            parameters.Add("@orc_valor_total",orcamento.Valor_Total );
            parameters.Add("@orc_data_validae", orcamento.Data_Validade);
            parameters.Add("@col_codigo", orcamento.col_Id);
            parameters.Add("@cli_codigo", orcamento.cli_Id);
            parameters.Add("@orc_valor_desconto", orcamento.Valor_Desconto);
            database.ExecuteNonQuery(sql, parameters);

            return database.UltimoID;
        }

        public int update(Orcamento orcamento)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            sql = @"UPDATE orcamento set orc_valor_total = @orc_valor_total, orc_data_validae = @orc_data_validae, col_codigo = @col_codigo,
                                        cli_codigo = @cli_codigo, orc_valor_desconto = @orc_valor_desconto
                                        WHERE orc_codigo = @orc_codigo";


            parameters.Add("@orc_valor_total", orcamento.Valor_Total);
            parameters.Add("@orc_data_validae", orcamento.Data_Validade);
            parameters.Add("@col_codigo", orcamento.col_Id);
            parameters.Add("@cli_codigo", orcamento.cli_Id);
            parameters.Add("@orc_valor_desconto", orcamento.Valor_Desconto);
            parameters.Add("@orc_codigo", orcamento.Id);
            database.ExecuteNonQuery(sql, parameters);

            return database.UltimoID;
        }

        public void saveProdutoOrcamento(Produto produto)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            sql = @"INSERT INTO produtos_orcamento
                                    (
                                    pro_codigo,
                                    orc_codigo,
                                    pro_orc_valor,
                                    pro_orc_quantidade)
                                    VALUES
                                    (
                                    @pro_codigo,
                                    @orc_codigo,
                                    @pro_orc_valor,
                                    @pro_orc_quantidade);";


            parameters.Add("@pro_codigo", produto.Id);
            parameters.Add("@orc_codigo", produto.orcamentoId);
            parameters.Add("@pro_orc_valor", produto.ValorVenda);
            parameters.Add("@pro_orc_quantidade", produto.quantidade);
            database.ExecuteNonQuery(sql, parameters);
        }

        public void updateProdutoOrcamento(Produto produto)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            sql = @"UPDATE produtos_orcamento set pro_orc_valor = @pro_orc_valor,
                                                  pro_orc_quantidade = @pro_orc_quantidade
                                                  WHERE pro_codigo = @pro_codigo and orc_codigo = @orc_codigo";

            parameters.Add("@pro_codigo", produto.Id);
            parameters.Add("@orc_codigo", produto.orcamentoId);
            parameters.Add("@pro_orc_valor", produto.ValorVenda);
            parameters.Add("@pro_orc_quantidade", produto.quantidade);
            database.ExecuteNonQuery(sql, parameters);
        }

        public bool Excluir(int codigo)
        {
            
            string sql = @"DELETE FROM eng2banco.produtos_orcamento
                            WHERE orc_codigo = @Codigo";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@Codigo", codigo);

            object retorno = bd.ExecuteSelectScalar(sql, parameter);

            sql = @"DELETE FROM eng2banco.orcamento
                            WHERE orc_codigo = @Codigo";

            retorno = bd.ExecuteSelectScalar(sql, parameter);

            if (retorno == null || Convert.ToInt32(retorno) == 0)
                return true;
            else
                return false;
        }
    }
}
