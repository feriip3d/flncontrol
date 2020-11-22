using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Cidade
    {
        int codigo;
        string nome;
        string regiao;
        int codigoEstado;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Regiao { get => regiao; set => regiao = value; }
        public int CodigoEstado { get => codigoEstado; set => codigoEstado = value; }

        public Cidade(int codigo, string nome, string regiao, int codigoEstado)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.regiao = regiao;
            this.codigoEstado = codigoEstado;
        }
        public Cidade(string nome, string regiao, int codigoEstado)
        {
            this.codigo = 0;
            this.nome = nome;
            this.regiao = regiao;
            this.codigoEstado = codigoEstado;
        }
        public Cidade()
        {
        }

        public List<Cidade> ListarTodasCidade() {
            List<Cidade> cidades = new List<Cidade>();

            cidades.Add(new Cidade(1, "Pirapozinho", "Oeste Paulista",1));
            cidades.Add(new Cidade(2, "Presidente Prudente", "Oeste Paulista", 1));
            cidades.Add(new Cidade(3, "Tarabai", "Oeste Paulista", 1));
            cidades.Add(new Cidade(4, "Anhumas", "Oeste Paulista", 1));

            return cidades; 
        }
        public List<Cidade> ListarCidadesPorNome(string nome = "") {
            List<Cidade> cidades = new List<Cidade>();

            cidades.Add(new Cidade(1, "Pirapozinho", "Oeste Paulista", 1));
            cidades.Add(new Cidade(2, "Presidente Prudente", "Oeste Paulista", 1));
            cidades.Add(new Cidade(3, "Tarabai", "Oeste Paulista", 1));
            cidades.Add(new Cidade(4, "Anhumas", "Oeste Paulista", 1));

            if (nome == null || nome == "")
                return cidades;
            else
            {
                List<Cidade> retorno = new List<Cidade>();
                cidades.ForEach(cid => {
                    if (cid.nome == nome)
                        retorno.Add(cid);
                });
                return retorno; 
            }
        }
        public List<Cidade> ListarCidadesPorRegiao(string regiao = "")
        {
            List<Cidade> cidades = new List<Cidade>();

            cidades.Add(new Cidade(1, "Pirapozinho", "Oeste Paulista", 1));
            cidades.Add(new Cidade(2, "Presidente Prudente", "Oeste Paulista", 1));
            cidades.Add(new Cidade(3, "Tarabai", "Oeste Paulista", 1));
            cidades.Add(new Cidade(4, "Anhumas", "Oeste Paulista", 1));

            if (regiao == null || regiao == "")
                return cidades;
            else
            {
                List<Cidade> retorno = new List<Cidade>();
                cidades.ForEach(cid => {
                    if (cid.regiao == regiao)
                        retorno.Add(cid);
                });
                return retorno;
            }
        }
        public Cidade PesquisarCidadePorCodigo(int codigo = 0)
        {
            List<Cidade> cidades = new List<Cidade>();

            cidades.Add(new Cidade(1, "Pirapozinho", "Oeste Paulista", 1));
            cidades.Add(new Cidade(2, "Presidente Prudente", "Oeste Paulista", 1));
            cidades.Add(new Cidade(3, "Tarabai", "Oeste Paulista", 1));
            cidades.Add(new Cidade(4, "Anhumas", "Oeste Paulista", 1));

            if (codigo <= 0)
                return null;
            else
            {
                Cidade retorno = new Cidade();
                cidades.ForEach(cid => {
                    if (cid.codigo == codigo)
                        retorno = cid;
                });
                return retorno;
            }
        }

        public bool GravarCidadeCompleto() {
            this.codigo = 10;
            return this.codigo > 0;
        }
    }
}
