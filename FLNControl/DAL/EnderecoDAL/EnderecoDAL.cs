using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL;
using FLNControl.Models;


namespace FLNControl.DAL.EnderecoDAL
{
    public class EnderecoDAL
    {
        MySqlPersistence bd = new MySqlPersistence();
        public List<Endereco> Mapeamento(DbDataReader dr)
        {
            List<Endereco> enderecos = new List<Endereco>();

            while (dr.Read())
            {
                Endereco endereco = new Endereco();
                endereco.Codigo = Convert.ToInt32(dr["end_codigo"].ToString());
                endereco.Rua = dr["end_rua"].ToString();
                endereco.Bairro = dr["end_bairro"].ToString();
                endereco.Tipo = dr["end_tipo"].ToString();
                endereco.Cep = dr["end_cep"].ToString();
                endereco.Descricao = dr["end_descricao"].ToString();
                endereco.CodigoCidade = Convert.ToInt32(dr["cid_codigo"].ToString());
                endereco.CodigoCliente = Convert.ToInt32(dr["cli_codigo"].ToString());
                endereco.CodigoColaborador = Convert.ToInt32(dr["col_codigo"].ToString());
                endereco.CodigoFornecedor = Convert.ToInt32(dr["for_codigo"].ToString());

                enderecos.Add(endereco);
            }
            bd.Close();

            return enderecos;
        }
        public List<Endereco> ListarTodosEnderecos()
        {
            string sql = "select * from endereco";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Endereco> ListarTodosEnderecosDeCliente(int codigoCliente)
        {
            string sql = "select * from endereco where cli_codigo = " + codigoCliente;

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Endereco> ListarTodosEnderecosDeColaborador(int codigoColaborador)
        {
            string sql = "select * from endereco where col_codigo = " + codigoColaborador;

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Endereco> ListarTodosEnderecosDeFornecedor(int codigoFornecedor)
        {
            string sql = "select * from endereco where for_codigo = " + codigoFornecedor;

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }

        public Endereco PesquisarEnderecoPorCodigo(int codigoEndereco) 
        {
            string sql = "select * from endereco where end_codigo = " + codigoEndereco;

            DbDataReader dr = bd.ExecuteSelect(sql);

            Endereco endereco = new Endereco();
            endereco.Codigo = Convert.ToInt32(dr["end_codigo"].ToString());
            endereco.Rua = dr["end_rua"].ToString();
            endereco.Bairro = dr["end_bairro"].ToString();
            endereco.Tipo = dr["end_tipo"].ToString();
            endereco.Cep = dr["end_cep"].ToString();
            endereco.Descricao = dr["end_descricao"].ToString();
            endereco.CodigoCidade = Convert.ToInt32(dr["cid_codigo"].ToString());
            endereco.CodigoCliente = Convert.ToInt32(dr["cli_codigo"].ToString());
            endereco.CodigoColaborador = Convert.ToInt32(dr["col_codigo"].ToString());
            endereco.CodigoFornecedor = Convert.ToInt32(dr["for_codigo"].ToString());

            return endereco;
        }
        public bool GravarEnderecoCompleto(Endereco novo)
        {
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"INSERT INTO endereco
                                        (
                                        end_rua,
                                        end_bairro,
                                        end_tipo,
                                        end_cep,
                                        end_descricao,
                                        cid_codigo,
                                        cli_codigo,
                                        col_codigo,
                                        for_codigo)
                                        VALUES
                                        (
                                        @rua,
                                        @bairro,
                                        @tipo,
                                        @cep,
                                        @descricao,
                                        @codigoCidade,
                                        @codigoCliente,
                                        @codigoColaborador,
                                        @codigoFornecedor)
                                        ";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@rua", novo.Rua);
            parameter.Add("@bairro", novo.Bairro);
            parameter.Add("@tipo", novo.Tipo);
            parameter.Add("@cep", novo.Cep);
            parameter.Add("@descricao", novo.Descricao);
            parameter.Add("@codigoCidade", novo.CodigoCidade);
            parameter.Add("@codigoCliente", novo.CodigoCliente);
            parameter.Add("@codigoColaborador", novo.CodigoColaborador);
            parameter.Add("@codigoFornecedor", novo.CodigoFornecedor);

            bd.ExecuteNonQuery(sql, parameter);

            novo.Codigo = bd.UltimoID;

            return novo.Codigo == 0;
        }
        public bool ExcluirEnderecoFisico(int codigo)
        {
            string sql = "delete endereco where end_codigo" + codigo;

            return (bd.ExecuteNonQuery(sql) > 0);
        }
        public bool AlterarEnderecoCompleto(Endereco novo)
        {
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"UPDATE endereco SET 
                                            end_rua = @rua,
                                            end_bairro = @bairro,
                                            end_tipo = @tipo,
                                            end_cep = @cep,
                                            end_descricao = @descricao,
                                            cid_codigo = @codigoCidade,
                                            cli_codigo = @codigoCliente,
                                            col_codigo = @codigoColaborador,
                                            for_codigo = @codigoFornecedor
                                            WHERE end_codigo = @codigo
                                            ";


            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@codigo", novo.Codigo);
            parameter.Add("@rua", novo.Rua);
            parameter.Add("@bairro", novo.Bairro);
            parameter.Add("@tipo", novo.Tipo);
            parameter.Add("@cep", novo.Cep);
            parameter.Add("@descricao", novo.Descricao);
            parameter.Add("@codigoCidade", novo.CodigoCidade);
            parameter.Add("@codigoCliente", novo.CodigoCliente);
            parameter.Add("@codigoColaborador", novo.CodigoColaborador);
            parameter.Add("@codigoFornecedor", novo.CodigoFornecedor);

            return bd.ExecuteNonQuery(sql, parameter) != 0;
        }
    }
}
