using engenharia.DAL;
using FLNControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace engenharia.DAL.ProdutoDAL
{
    public class ProdutoDAL
    {
        public Produto find(int id)
        {
            Produto produto = null;
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM produto WHERE pro_codigo = @pProdId LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProdId", id);

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            if (result.HasRows)
            {
                produto = new Produto();
                result.Read();

                produto.Id = Convert.ToInt32(result["pro_codigo"]);
                produto.Descricao = result["pro_descricao"].ToString();
                produto.Categoria = result["pro_categoria"].ToString();
                produto.Marca = result["pro_marca"].ToString();
                produto.ValorVenda = Convert.ToDecimal(result["pro_valor_venda"]);
                produto.ValorCompra = Convert.ToDecimal(result["pro_valor_compra"]);
            }

            database.Close();

            return produto;
        }

        public List<Produto> findByDescription(string desc)
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM produto WHERE LOWER(pro_descricao) LIKE @pProdDesc";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProdDesc", "%" + desc.ToLower() + "%");

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            List<Produto> listaProduto = new List<Produto>();
            if (result.HasRows)
            {
                Produto produto;
                while (result.Read())
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt32(result["pro_codigo"]);
                    produto.Descricao = result["pro_descricao"].ToString();
                    produto.Categoria = result["pro_categoria"].ToString();
                    produto.Marca = result["pro_marca"].ToString();
                    produto.ValorVenda = Convert.ToDecimal(result["pro_valor_venda"]);
                    produto.ValorCompra = Convert.ToDecimal(result["pro_valor_compra"]);
                    listaProduto.Add(produto);
                }
            }

            database.Close();

            return listaProduto;
        }

        public List<Produto> findByCategory(string categ)
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM produto WHERE LOWER(pro_categoria) LIKE @pProdCateg";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProdCateg", "%" + categ.ToLower() + "%");

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            List<Produto> listaProduto = new List<Produto>();
            if (result.HasRows)
            {
                Produto produto;
                while (result.Read())
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt32(result["pro_codigo"]);
                    produto.Descricao = result["pro_descricao"].ToString();
                    produto.Categoria = result["pro_categoria"].ToString();
                    produto.Marca = result["pro_marca"].ToString();
                    produto.ValorVenda = Convert.ToDecimal(result["pro_valor_venda"]);
                    produto.ValorCompra = Convert.ToDecimal(result["pro_valor_compra"]);
                    listaProduto.Add(produto);
                }
            }

            database.Close();

            return listaProduto;
        }

        public List<Produto> findAll()
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM produto";

            DbDataReader result = database.ExecuteSelect(sql);
            List<Produto> listaProduto = new List<Produto>();
            if (result.HasRows)
            {
                Produto produto;
                while (result.Read())
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt32(result["pro_codigo"]);
                    produto.Descricao = result["pro_descricao"].ToString();
                    produto.Categoria = result["pro_categoria"].ToString();
                    produto.Marca = result["pro_marca"].ToString();
                    produto.ValorVenda = Convert.ToDecimal(result["pro_valor_venda"]);
                    produto.ValorCompra = Convert.ToDecimal(result["pro_valor_compra"]);
                    listaProduto.Add(produto);
                }
            }

            database.Close();

            return listaProduto;
        }

        public List<Produto> findByBrand(string brand)
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM produto WHERE LOWER(pro_marca) LIKE @pProdBrand";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProdBrand", "%" + brand.ToLower() + "%");

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            List<Produto> listaProduto = new List<Produto>();
            if (result.HasRows)
            {
                Produto produto;
                while (result.Read())
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt32(result["pro_codigo"]);
                    produto.Descricao = result["pro_descricao"].ToString();
                    produto.Categoria = result["pro_categoria"].ToString();
                    produto.Marca = result["pro_marca"].ToString();
                    produto.ValorVenda = Convert.ToDecimal(result["pro_valor_venda"]);
                    produto.ValorCompra = Convert.ToDecimal(result["pro_valor_compra"]);
                    listaProduto.Add(produto);
                }
            }

            database.Close();

            return listaProduto;
        }

        public int save(Produto produto)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            if (produto.Id == 0)
            {
                sql = @"INSERT INTO 
                    produto(pro_descricao, pro_categoria, pro_marca, 
                        pro_valor_compra, pro_valor_venda)
                    VALUES(@pProdDescricao, @pProdCategoria, @pProdMarca,
                        @pProdValorCompra, @pProdValorVenda
                    )";
            }
            else
            {
                sql = @"UPDATE 
                    produto SET pro_descricao = @pProdDescricao, 
                        pro_categoria = @pProdCategoria, 
                        pro_marca = @pProdMarca, 
                        pro_valor_compra = @pProdValorCompra,
                        pro_valor_venda = @pProdValorVenda
                    WHERE pro_codigo = @pProdId";
                parameters.Add("@pProdId", produto.Id);
            }

            parameters.Add("@pProdDescricao", produto.Descricao);
            parameters.Add("@pProdCategoria", produto.Categoria);
            parameters.Add("@pProdMarca", produto.Marca);
            parameters.Add("@pProdValorCompra", produto.ValorCompra);
            parameters.Add("@pProdValorVenda", produto.ValorVenda);
            database.ExecuteNonQuery(sql, parameters);

            return database.UltimoID;
        }

        public int delete(int id)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = @"DELETE FROM produto WHERE pro_codigo = @pProdId";
            parameters.Add("@pProdId", id);

            return database.ExecuteNonQuery(sql, parameters);
        }
    }
}
