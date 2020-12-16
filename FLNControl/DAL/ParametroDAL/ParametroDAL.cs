using engenharia.DAL;
using FLNControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace engenharia.DAL.ParametroDAL
{
    public class ParametroDAL
    {
        public Parametro find()
        {
            Parametro parametros = null;
            MySqlPersistence database = new MySqlPersistence();
            string sql = @"SELECT * FROM parametrizacao WHERE par_codigo = '1' LIMIT 1";

            DbDataReader result = database.ExecuteSelect(sql);
            if (result.HasRows)
            {
                parametros = new Parametro();
                result.Read();

                parametros.Id = Convert.ToInt32(result["par_codigo"].ToString());
                parametros.RazaoSocial = result["par_razao_social"].ToString();
                parametros.NomeFantasia = result["par_nome_fantasia"].ToString();
                parametros.Site = result["par_site"].ToString();
                parametros.Email = result["par_email"].ToString();
                parametros.Cnpj = result["par_cnpj"].ToString();
                parametros.InscricaoEstadual = result["par_inscricao_estadual"].ToString();
                parametros.Telefone = result["par_telefone"].ToString();
                parametros.UrlLogoGrande = result["par_url_logo_grande"].ToString();
                parametros.UrlLogoPequeno = result["par_url_logo_pequeno"].ToString();
                parametros.Endereco = result["par_endereco"].ToString();
                parametros.Cidade = result["par_cidade"].ToString();
                parametros.Uf = result["par_uf"].ToString();
            }

            database.Close();

            return parametros;
        }

        public int save(Parametro parametros)
        {
            MySqlPersistence database = new MySqlPersistence();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql;
            if(parametros.Id == 0)
            {
                sql = @"INSERT 
                parametrizacao(par_codigo, par_razao_social, par_nome_fantasia, par_site,
                    par_email, par_cnpj, par_inscricao_estadual, par_telefone,
                    par_url_logo_grande, par_url_logo_pequeno,
                    par_endereco, par_cidade, par_uf)
                VALUES('1', @pRazaoSocial, @pNomeFantasia, @pSite,
                    @pEmail, @pCnpj, @pInscEstadual, @pTelefone,
                    @pUrlLogoGrande, @pUrlLogoPequeno, @pEndereco,
                    @pCidade, @pUF)";
            } else
            {
                sql = @"UPDATE 
                parametrizacao SET par_razao_social = @pRazaoSocial, 
                    par_nome_fantasia = @pNomeFantasia, 
                    par_site = @pSite, 
                    par_email = @pEmail, 
                    par_cnpj = @pCnpj, 
                    par_inscricao_estadual = @pInscEstadual, 
                    par_telefone = @pTelefone, 
                    par_url_logo_grande = @pUrlLogoGrande, 
                    par_url_logo_pequeno = @pUrlLogoPequeno, 
                    par_endereco = @pEndereco, 
                    par_cidade = @pCidade, 
                    par_uf = @pUF
                WHERE par_codigo = '1'";
            }

            parameters.Add("@pRazaoSocial", parametros.RazaoSocial);
            parameters.Add("@pNomeFantasia", parametros.NomeFantasia);
            parameters.Add("@pSite", parametros.Site);
            parameters.Add("@pCnpj", parametros.Cnpj);
            parameters.Add("@pEmail", parametros.Email);
            parameters.Add("@pInscEstadual", parametros.InscricaoEstadual);
            parameters.Add("@pTelefone", parametros.Telefone);
            parameters.Add("@pUrlLogoGrande", parametros.UrlLogoGrande);
            parameters.Add("@pUrlLogoPequeno", parametros.UrlLogoPequeno);
            parameters.Add("@pEndereco", parametros.Endereco);
            parameters.Add("@pCidade", parametros.Cidade);
            parameters.Add("@pUF", parametros.Uf);
            database.ExecuteNonQuery(sql, parameters);

            return database.UltimoID;
        }
    }
}
