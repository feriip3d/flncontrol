using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL;
using engenharia.DAL.EstoqueDAL;
using engenharia.DAL.ProdutoDAL;
using FLNControl.Models;
using MySql.Data.MySqlClient;

namespace FLNControl.DAL
{
    public class VendaDAL
    {
        public int gravarVenda(CarrinhoCompra carrinho)
        {
            MySqlPersistence database = new MySqlPersistence();
            database.Abrir();
            MySqlTransaction transaction = database.conexao.BeginTransaction();
            database.cmd.Transaction = transaction;
            int vendaid = 0;

            try
            {
                string sql = @"INSERT INTO venda(ven_forma_pagamento, ven_cli_nome, 
                ven_cli_cpf, ven_cli_numero, ven_cli_endereco, ven_cli_bairro, 
                ven_cli_cidade, ven_cli_uf) 
                VALUES (@pFormaPagamento, @pCliNome, @pCliCPF, @pCliNumero, @pCliEndereco,
                @pCliBairro, @pCliCidade, @pCliUF)";

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@pFormaPagamento", carrinho.FormaPagamento);
                parameters.Add("@pCliNome", carrinho.NomeCli);
                parameters.Add("@pCliCPF", carrinho.Cpf);
                parameters.Add("@pCliEndereco", carrinho.Endereco);
                parameters.Add("@pCliNumero", carrinho.Numero);
                parameters.Add("@pCliBairro", carrinho.Bairro);
                parameters.Add("@pCliCidade", carrinho.Cidade);
                parameters.Add("@pCliUF", carrinho.Uf);
                database.ExecuteNonQuery(sql, parameters);

                vendaid = database.UltimoID;
                decimal total = 0;

                foreach (var p in carrinho.ListaProdutos)
                {
                    parameters.Clear();
                    int codigoProduto = Convert.ToInt32(p[0]);
                    int qtde = Convert.ToInt32(p[1]);

                    ProdutoDAL pdal = new ProdutoDAL();
                    Produto prod = pdal.find(codigoProduto);

                    EstoqueDAL edal = new EstoqueDAL();
                    edal.decrementarEstoqueProd(codigoProduto, qtde);

                    total += prod.ValorVenda * qtde;

                    sql = @"INSERT INTO venda_has_produto(idvenda, idproduto, qtdeVendida, valorVenda)
                    VALUES(@pVendaId, @pProdutoId, @pQuantidade, @pValorVenda)";
                    parameters.Add("@pVendaId", vendaid);
                    parameters.Add("@pProdutoId", codigoProduto);
                    parameters.Add("@pQuantidade", qtde);
                    parameters.Add("@pValorVenda", prod.ValorVenda);
                    database.ExecuteNonQuery(sql, parameters);
                }

                switch (carrinho.FormaPagamento)
                {
                    case "1":
                        parameters.Clear();
                        sql = @"INSERT INTO contasreceber(con_rec_data_vencimento, 
                        con_rec_status, ven_codigo, con_rec_valor)
                        VALUES(NOW(), 0, @pVendaId, @pTotal)";
                        parameters.Add("@pVendaId", vendaid);
                        parameters.Add("@pTotal", total);
                        database.ExecuteNonQuery(sql, parameters);
                        break;

                    case "2":
                        decimal valor = total / carrinho.Parcelas;
                        for (int i = 1; i <= carrinho.Parcelas; i++)
                        {
                            parameters.Clear();

                            sql = $"INSERT INTO contasreceber(con_rec_data_vencimento," +
                                $"con_rec_status, ven_codigo, con_rec_valor)" +
                                $"VALUES((SELECT DATE_ADD(CONCAT(YEAR(NOW()),'-',MONTH(NOW()),'-{carrinho.Vencimento}'), INTERVAL @pMes MONTH)), 0, @pVendaId, @pTotal)";
                            parameters.Add("@pVendaId", vendaid);
                            parameters.Add("@pTotal", valor);
                            parameters.Add("@pDiaVenc", carrinho.Vencimento);
                            parameters.Add("@pMes", i);
                            database.ExecuteNonQuery(sql, parameters);
                        }
                        break;
                }

                transaction.Commit();
            } catch (Exception e)
            {
                transaction.Rollback();
            } finally
            {
                database.Close();
            }

            return vendaid;
        }

        public List<Object> findByName(string name)
        {
            List<Object> lista = new List<Object>();
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM venda WHERE ven_cli_nome LIKE @pCliNome LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pCliNome", "%"+name+"%");

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            if (result.HasRows)
            {
                result.Read();

                lista.Add(result["ven_codigo"].ToString());
                lista.Add(result["ven_data_venda"].ToString());
                lista.Add(Convert.ToInt32(result["ven_forma_pagamento"].ToString()));
                lista.Add(result["ven_cli_nome"].ToString());
                lista.Add(result["ven_cli_cpf"].ToString());
                lista.Add(result["ven_cli_endereco"].ToString());
                lista.Add(result["ven_cli_numero"].ToString());
                lista.Add(result["ven_cli_bairro"].ToString());
                lista.Add(result["ven_cli_cidade"].ToString());
                lista.Add(result["ven_cli_uf"].ToString());
            }

            return lista;
        }

        public List<Object> find(int id)
        {
            List<Object> lista = new List<Object>();
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM venda WHERE ven_codigo = @pVenCodigo LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pVenCodigo", id);

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            if (result.HasRows)
            {
                result.Read();

                lista.Add(result["ven_data_venda"].ToString());
                lista.Add(Convert.ToInt32(result["ven_forma_pagamento"].ToString()));
                lista.Add(result["ven_cli_nome"].ToString());
                lista.Add(result["ven_cli_cpf"].ToString());
                lista.Add(result["ven_cli_endereco"].ToString());
                lista.Add(result["ven_cli_numero"].ToString());
                lista.Add(result["ven_cli_bairro"].ToString());
                lista.Add(result["ven_cli_cidade"].ToString());
                lista.Add(result["ven_cli_uf"].ToString());
            }

            sql = @"SELECT * FROM venda_has_produto WHERE idvenda = @pVenCodigo";
            result = database.ExecuteSelect(sql, parameters);
            List<Object> listaProdutos = new List<Object>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    List<Object> prodInfo = new List<Object>();
                    prodInfo.Add(result["qtdeVendida"]);
                    prodInfo.Add(result["valorVenda"]);

                    sql = @"SELECT pro_descricao FROM produto WHERE pro_codigo = @prodCodigo LIMIT 1";
                    parameters.Clear();
                    parameters.Add("@prodCodigo", Convert.ToInt32(result["idProduto"].ToString()));
                    DbDataReader result2 = database.ExecuteSelect(sql, parameters);
                    if (result2.HasRows)
                    {
                        result2.Read();
                        prodInfo.Add(result2["pro_descricao"]);
                    }
                    listaProdutos.Add(prodInfo);
                }
                lista.Add(listaProdutos);
            }

            sql = @"SELECT * FROM contasreceber WHERE ven_codigo = @pVenCodigo";
            parameters.Clear();
            parameters.Add("@pVenCodigo", id);
            result = database.ExecuteSelect(sql, parameters);
            List<Object> parcelas = new List<Object>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    List<Object> parcelaInfo = new List<Object>();
                    parcelaInfo.Add(result["con_rec_data_vencimento"]);
                    parcelaInfo.Add(result["con_rec_status"]);
                    parcelaInfo.Add(result["con_rec_valor"]);

                    parcelas.Add(parcelaInfo);
                }
                lista.Add(parcelas);
            }

            database.Close();
            return lista;
        }
    }
}
