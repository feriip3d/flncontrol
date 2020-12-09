using engenharia.Controllers.ColaboradorController.engenharia.Controllers.ColaboradorController;
using engenharia.Models.Caixa;
using engenharia.Models.CaixaMovimentacao;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace engenharia.DAL.CaixaDAL
{
    public class CaixaDAL
    {
        MySqlPersistence bd = new MySqlPersistence();

        public Caixa Atual()
        {
            return Pegar("ORDER BY caixa_id DESC");
        }
        public Caixa Abrir(Caixa caixa, int col_id)
        {
            string sql = @"insert into caixa (caixa_data_abert, caixa_valor_inicial, caixa_valor_final, caixa_status)
                            values (@dt_abert, @valor_ini, @valor_fim, @status); 
                            SELECT last_insert_id();";
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@dt_abert", caixa.data_abertura);
            parameter.Add("@valor_ini", caixa.valor_inicial);
            parameter.Add("@valor_fim", caixa.valor_inicial);
            parameter.Add("@status", "Aberto");
            
            caixa.id = Convert.ToInt32(bd.ExecuteSelectScalar(sql, parameter));

            sql = @"insert into caixa_movimentacao (caixa_id, col_id, caixa_mov_data, caixa_mov_tipo, caixa_mov_valor, caixa_mov_motivo)
                                                    values (@id_caixa, @id_colaborador, @data, @tipo, @valor, @motivo)";
            parameter = new Dictionary<string, object>();
            parameter.Add("@id_caixa", caixa.id);
            parameter.Add("@id_colaborador", col_id);
            parameter.Add("@data", caixa.data_abertura);
            parameter.Add("@tipo", "Abertura");
            parameter.Add("@valor", caixa.valor_inicial);
            parameter.Add("@motivo", "Abertura do Caixa");

            bd.ExecuteNonQuery(sql, parameter);

            return caixa;
        }
        public void Fechar(Caixa caixa, int col_id)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            string sql = @"update caixa set caixa_data_fech = @data_fech, caixa_valor_final = @valor_final, 
                            caixa_status = @status where caixa_id = @id_caixa";
            parameter.Add("@data_fech", caixa.data_fechamento);
            parameter.Add("@valor_final", caixa.valor_final);
            parameter.Add("@status", "Fechado");
            parameter.Add("@id_caixa", caixa.id);

            bd.ExecuteNonQuery(sql, parameter);

            sql = @"insert into caixa_movimentacao (caixa_id, col_id, caixa_mov_data, caixa_mov_tipo, caixa_mov_valor, caixa_mov_motivo)
                                                    values (@id_caixa, @id_colaborador, @data, @tipo, @valor, @motivo)";
            parameter = new Dictionary<string, object>();
            parameter.Add("@id_caixa", caixa.id);
            parameter.Add("@id_colaborador", col_id);
            parameter.Add("@data", caixa.data_fechamento);
            parameter.Add("@tipo", "Fechamento");
            parameter.Add("@valor", caixa.valor_final);
            parameter.Add("@motivo", "Fechamento do Caixa");

            bd.ExecuteNonQuery(sql, parameter);
        }
        public void Atualizar(Movimentacao movimentacao)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            string sql = @"update caixa set caixa_valor_final = caixa_valor_final" +
                            (movimentacao.tipo == "Sangria" ? "-" : "+")
                            + "@valor_final where caixa_id = @id_caixa";
            parameter.Add("@valor_final", movimentacao.valor);
            parameter.Add("@id_caixa", movimentacao.caixa_id);

            bd.ExecuteNonQuery(sql, parameter);

            sql = @"insert into caixa_movimentacao (caixa_id, col_id, caixa_mov_data, caixa_mov_tipo, caixa_mov_valor, caixa_mov_motivo)
                                                    values (@id_caixa, @id_colaborador, @data, @tipo, @valor, @motivo)";
            parameter = new Dictionary<string, object>();
            parameter.Add("@id_caixa", movimentacao.caixa_id);
            parameter.Add("@id_colaborador", movimentacao.colaborador_id);
            parameter.Add("@data", movimentacao.data);
            parameter.Add("@tipo", movimentacao.tipo);
            parameter.Add("@valor", movimentacao.valor);
            parameter.Add("@motivo", movimentacao.motivo);

            bd.ExecuteNonQuery(sql, parameter);
        }
        public Caixa Pegar(string filtro = "")
        {
            string sql = @"SELECT * FROM eng2banco.caixa ";
            if (filtro != "")
                sql += filtro;

            MySqlDataReader reader = bd.ExecuteSelect(sql);
            Caixa caixa = new Caixa();
            if (reader.Read())
            {

                caixa.id = (int)reader["caixa_id"];
                caixa.data_abertura = (DateTime)reader["caixa_data_abert"];
                if (!Convert.IsDBNull(reader["caixa_data_fech"]))
                    caixa.data_fechamento = (DateTime)reader["caixa_data_fech"];
                caixa.valor_inicial = (decimal)reader["caixa_valor_inicial"];
                caixa.valor_final = (decimal)reader["caixa_valor_final"];
                caixa.status = (string)reader["caixa_status"];

                caixa.movimentacao = Listar("where caixa_id = " + caixa.id);
            }
            return caixa;
        }
        public List<Movimentacao> Listar(string filtro = "")
        {
            string sql = @"SELECT * FROM eng2banco.caixa_movimentacao ";
            if (filtro != "")
                sql += filtro;

            MySqlDataReader reader = bd.ExecuteSelect(sql);

            List<Movimentacao> lista = new List<Movimentacao>();
            while (reader.Read())
            {
                Movimentacao mov = new Movimentacao();
                mov.caixa_id = (int)reader["caixa_id"];
                mov.colaborador_id = (int)reader["col_id"];
                mov.data = (DateTime)reader["caixa_mov_data"];
                mov.motivo = (string)reader["caixa_mov_motivo"];
                mov.tipo = (string)reader["caixa_mov_tipo"];
                mov.valor = (decimal)reader["caixa_mov_valor"];

                lista.Add(mov);
            }

            return lista;
        }
    }
}
