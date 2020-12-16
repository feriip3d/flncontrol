using System;
using System.Collections.Generic;
using System.Data.Common;
using engenharia.DAL;
using FLNControl.Models;

namespace engenharia.DAL.EstoqueDAL
{
    public class EstoqueDAL
    {
        public Estoque findEstoqueProduto(Produto produto)
        {
            Estoque estoque = null;
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM estoque WHERE pro_codigo = @pProdId LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProdId", produto.Id);

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            if (result.HasRows)
            {
                estoque = new Estoque();
                result.Read();

                estoque.Id = Convert.ToInt32(result["est_codigo"]);
                estoque.Lote = result["est_lote"].ToString();
                estoque.Status = result["est_status"].ToString();
                estoque.IdProduto = Convert.ToInt32(result["pro_codigo"]);
                estoque.QtdeEstoque = Convert.ToDecimal(result["est_quantidade"]);
            }

            database.Close();

            return estoque;
        }

        public void decrementarEstoque(int loteProd, int estoqueQtd)
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"UPDATE estoque SET est_quantidade = IF(est_quantidade > 0, est_quantidade - @pEstoqueQtd, 0) WHERE est_lote = @pLoteProd AND est_status = '1' LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pLoteProd", loteProd);
            parameters.Add("@pEstoqueQtd", estoqueQtd);

            database.ExecuteNonQuery(sql, parameters);
            database.Close();
        }

        public void decrementarEstoqueProd(int prodCodigo, int estoqueQtd)
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"UPDATE estoque SET est_quantidade = IF(est_quantidade > @pEstoqueQtd, est_quantidade - @pEstoqueQtd, est_quantidade) WHERE pro_codigo = @pProd AND est_status = '1' LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProd", prodCodigo);
            parameters.Add("@pEstoqueQtd", estoqueQtd);

            database.ExecuteNonQuery(sql, parameters);
            database.Close();
        }

        public void incrementarEstoque(int loteProd, int estoqueQtd)
        {
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"UPDATE estoque SET est_quantidade = est_quantidade + @pEstoqueQtd WHERE est_lote = @pLoteProd AND est_status = '1' LIMIT 1";
            
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pLoteProd", loteProd);
            parameters.Add("@pEstoqueQtd", estoqueQtd);

            database.ExecuteNonQuery(sql, parameters);
            database.Close();
        }

        public int save(Estoque estoque)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            if (estoque.Id == 0)
            {
                sql = @"INSERT INTO 
                    estoque(est_lote, est_status, pro_codigo, 
                        est_quantidade)
                    VALUES(@pEstoqueLote, @pEstoqueStatus, @pEstoqueProdId,
                        @pEstoqueQtde
                    )";
            }
            else
            {
                sql = @"UPDATE 
                    produto SET est_lote = @pEstoqueLote, 
                        est_status = @pEstoqueStatus, 
                        pro_codigo = @pEstoqueProdId,
                        est_quantidade = @pEstoqueQtde
                    WHERE est_codigo = @pEstoqueId";
                parameters.Add("@pEstoqueId", "'" + estoque.Id + "'");
            }

            parameters.Add("@pEstoqueLote", estoque.Lote);
            parameters.Add("@estoque_status", estoque.Status);
            parameters.Add("@produto_prod_id", estoque.IdProduto);
            parameters.Add("@estoque_quantidade", estoque.QtdeEstoque);
            database.ExecuteNonQuery(sql, parameters);

            return database.UltimoID;
        }
    }
}
