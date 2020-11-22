using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Estado
    {
        int codigo;
        string nome;
        string uf;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Uf { get => uf; set => uf = value; }

        public Estado(int codigo, string nome, string uf)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Uf = uf;
        }
        public Estado( string nome, string uf)
        {
            this.Codigo = 0;
            this.Nome = nome;
            this.Uf = uf;
        }
        public Estado()
        {
        }

        public List<Estado> ListarTodosEndereco() {
            List<Estado> estados = new List<Estado>();

            estados.Add(new Estado(1,"São Paulo","SP"));
            estados.Add(new Estado(2,"Santa Catarina","SC"));
            estados.Add(new Estado(3,"Rio de Janeiro","RJ"));
            estados.Add(new Estado(4,"Paraná","PR"));

            return estados;
        }
        public List<Estado> ListarEnderecoPorNome(string nome = "") {
            List<Estado> estados = new List<Estado>();
            estados.Add(new Estado(1, "São Paulo", "SP"));
            estados.Add(new Estado(2, "Santa Catarina", "SC"));
            estados.Add(new Estado(3, "Rio de Janeiro", "RJ"));
            estados.Add(new Estado(4, "Paraná", "PR"));


            if (nome == null || nome == "")
                return estados;
            else {
                List<Estado> retorno = new List<Estado>();
                estados.ForEach(est =>
                {
                    if (est.nome == nome)
                        retorno.Add(est);
                });
                return retorno;
            }
        }
        public List<Estado> ListarEnderecoPorUF(string uf = "")
        {
            List<Estado> estados = new List<Estado>();
            estados.Add(new Estado(1, "São Paulo", "SP"));
            estados.Add(new Estado(2, "Santa Catarina", "SC"));
            estados.Add(new Estado(3, "Rio de Janeiro", "RJ"));
            estados.Add(new Estado(4, "Paraná", "PR"));

            if (uf == null || uf == "")
                return estados;
            else
            {
                List<Estado> retorno = new List<Estado>();
                estados.ForEach(est =>
                {
                    if (est.Uf == uf)
                        retorno.Add(est);
                });
                return retorno;
            }
        }
        public Estado PesquisarEstadoPorCodigo(int codigo = 0)
        {
            List<Estado> estados = new List<Estado>();

            if (codigo.Equals(codigo) || codigo == 0)
                return null;
            else
            {
                estados.Add(new Estado(1, "São Paulo", "SP"));
                estados.Add(new Estado(2, "Santa Catarina", "SC"));
                estados.Add(new Estado(3, "Rio de Janeiro", "RJ"));
                estados.Add(new Estado(4, "Paraná", "PR"));
                Estado estado = new Estado();
                estados.ForEach(est =>
                {
                    if (est.codigo == codigo)
                        estado = est;
                });
                return estado;
            }
        }
        
        public bool GravarEstadoCompleto() 
        {
            this.codigo = 1;
            return this.codigo > 0;
        }
    }
}
