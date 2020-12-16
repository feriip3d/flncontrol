using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL;
using FLNControl.Models;

namespace FLNControl.DAL.FornecedorDAL
{
    public class FornecedorDAL
    {
        MySqlPersistence bd = new MySqlPersistence();

        public List<Fornecedor> Mapeamento(DbDataReader dr)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (dr.Read())
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Codigo = Convert.ToInt32(dr["for_codigo"].ToString());
                fornecedor.Nome = dr["for_nome"].ToString();
                fornecedor.Cnpj = dr["for_cnpj"].ToString();
                fornecedor.Telefone = dr["for_telefone"].ToString();
                fornecedor.Dados_bancarios = dr["for_dados_bancarias"].ToString();
                fornecedor.Email = dr["for_email"].ToString();
                fornecedor.Site = dr["for_site"].ToString();
                fornecedor.NomeVendedor = dr["for_nome_vendedor"].ToString();
                fornecedor.TelefoneVendedor = dr["for_telefone_vendedor"].ToString();

                fornecedores.Add(fornecedor);
            }
            bd.Close();

            return fornecedores;
        }
        public List<Fornecedor> ListarTodosFornecedores()
        {
            string sql = "select * from fornecedore";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Fornecedor> ListarFornecedoresPorNome(string nome)
        {
            string sql = "select * from fornecedore where for_nome like '" + nome + "%'";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Fornecedor> ListarFornecedoresPorEmail(string email)
        {
            string sql = "select * from fornecedore where for_email like '" + email + "%'";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Fornecedor> ListarFornecedoresPorNomeVendedor(string nomeVendedor)
        {
            string sql = "select * from fornecedore where for_nome_vendedor like '" + nomeVendedor + "%'";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }
        public List<Fornecedor> ListarFornecedoresPorTelefoneVendedor(string telefone)
        {
            string sql = "select * from fornecedore where for_telefone like '" + telefone + "'%";

            return this.Mapeamento(bd.ExecuteSelect(sql));
        }

        public Fornecedor BuscarFornecedorPorCodigo(int codigo)
        {
            string sql = "select * from fornecedore where for_codigo like " + codigo ;

            DbDataReader dr = bd.ExecuteSelect(sql);

            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Codigo = Convert.ToInt32(dr["for_codigo"].ToString());
            fornecedor.Nome = dr["for_nome"].ToString();
            fornecedor.Cnpj = dr["for_cnpj"].ToString();
            fornecedor.Telefone = dr["for_telefone"].ToString();
            fornecedor.Dados_bancarios = dr["for_dados_bancarias"].ToString();
            fornecedor.Email = dr["for_email"].ToString();
            fornecedor.Site = dr["for_site"].ToString();
            fornecedor.NomeVendedor = dr["for_nome_vendedor"].ToString();
            fornecedor.TelefoneVendedor = dr["for_telefone_vendedor"].ToString();

            return fornecedor;
        }
        public bool GravarFornecedorCompleto(Fornecedor novo)
        {
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"INSERT INTO fornecedore
                                        (for_nome,
                                        for_cnpj,
                                        for_telefone,
                                        for_dados_bancarias,
                                        for_email,
                                        for_site,
                                        for_nome_vendedor,
                                        for_telefone_vendedor)
                                        VALUES
                                        (@nome,
                                        @cnpj,
                                        @telefone,
                                        @dadosBancarios,
                                        @email,
                                        @site,
                                        @nomeVendedor,
                                        @telefoneVendedor)";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@nome", novo.Nome);
            parameter.Add("@cnpj", novo.Cnpj);
            parameter.Add("@telefone", novo.Telefone);
            parameter.Add("@dadosBancarios", novo.Dados_bancarios);
            parameter.Add("@email", novo.Email);
            parameter.Add("@site", novo.Site);
            parameter.Add("@nomeVendedor", novo.NomeVendedor);
            parameter.Add("@telefoneVendedor", novo.Telefone);

            bd.ExecuteNonQuery(sql, parameter);

            novo.Codigo = bd.UltimoID;

            return novo.Codigo == 0;
        }
        public bool ExcluirFornecedorFisico(int codigo)
        {
            string sql = "delete fornecedore where for_codigo" + codigo;

            return (bd.ExecuteNonQuery(sql) > 0);
        }
        public bool AlterarFornecedorCompleto(Fornecedor novo)
        {
            MySqlPersistence bd = new MySqlPersistence();

            string sql = @"UPDATE fornecedore SET 
                                            for_nome = @nome,
                                            for_cnpj = @cnpj,
                                            for_telefone = @telefone,
                                            for_dados_bancarias = @dadosBancarios,
                                            for_email = @email,
                                            for_site = @site,
                                            for_nome_vendedor = @nomeVendedor,
                                            for_telefone_vendedor = @telefoneVendedor
                                            WHERE for_codigo = @codigo
                                            ";

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@codigo", novo.Codigo);
            parameter.Add("@nome", novo.Nome);
            parameter.Add("@cnpj", novo.Cnpj);
            parameter.Add("@telefone", novo.Telefone);
            parameter.Add("@dadosBancarios", novo.Dados_bancarios);
            parameter.Add("@email", novo.Email);
            parameter.Add("@site", novo.Site);
            parameter.Add("@nomeVendedor", novo.NomeVendedor);
            parameter.Add("@telefoneVendedor", novo.Telefone);

            return bd.ExecuteNonQuery(sql, parameter) != 0;
        }

    }
}
