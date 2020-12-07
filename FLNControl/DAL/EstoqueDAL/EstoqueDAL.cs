﻿using System;
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
            string sql = @"SELECT * FROM estoque WHERE produto_prod_id = @pProdId LIMIT 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@pProdId", produto.Id);

            DbDataReader result = database.ExecuteSelect(sql, parameters);
            if (result.HasRows)
            {
                estoque = new Estoque();
                result.Read();

                estoque.Id = Convert.ToInt32(result["estoque_id"]);
                estoque.Lote = result["estoque_lote"].ToString();
                estoque.Status = result["estoque_status"].ToString();
                estoque.IdProduto = Convert.ToInt32(result["produto_prod_id"]);
                estoque.QtdeEstoque = Convert.ToDecimal(result["estoque_quantidade"]);
            }

            database.Close();

            return estoque;
        }

        public int save(Estoque estoque)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            if (estoque.Id == 0)
            {
                sql = @"INSERT INTO 
                    estoque(estoque_lote, estoque_status, produto_prod_id, 
                        estoque_quantidade)
                    VALUES(@pEstoqueLote, @pEstoqueStatus, @pEstoqueProdId,
                        @pEstoqueQtde
                    )";
            }
            else
            {
                sql = @"UPDATE 
                    estoque SET estoque_lote = @pEstoqueLote, 
                        estoque_status = @pEstoqueStatus, 
                        produto_prod_id = @pEstoqueProdId,
                        estoque_quantidade = @pEstoqueQtde
                    WHERE estoque_id = @pEstoqueId";
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