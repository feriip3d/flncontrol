using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Fornecedor
    {
        int codigo;
        string nome;
        string cnpj;
        string telefone;
        string dados_bancarios;
        string email;
        string site;
        string nomeVendedor;
        string telefoneVendedor;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Dados_bancarios { get => dados_bancarios; set => dados_bancarios = value; }
        public string Email { get => email; set => email = value; }
        public string Site { get => site; set => site = value; }
        public string NomeVendedor { get => nomeVendedor; set => nomeVendedor = value; }
        public string TelefoneVendedor { get => telefoneVendedor; set => telefoneVendedor = value; }
        public Fornecedor(int codigo, string nome, string cnpj, string telefone, string dados_bancarios, string email, string site, string nomeVendedor, string telefoneVendedor)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.cnpj = cnpj;
            this.telefone = telefone;
            this.dados_bancarios = dados_bancarios;
            this.email = email;
            this.site = site;
            this.nomeVendedor = nomeVendedor;
            this.telefoneVendedor = telefoneVendedor;
        }
        public Fornecedor(string nome, string cnpj, string telefone, string dados_bancarios, string email, string site, string nomeVendedor, string telefoneVendedor)
        {
            this.codigo = 0;
            this.nome = nome;
            this.cnpj = cnpj;
            this.telefone = telefone;
            this.dados_bancarios = dados_bancarios;
            this.email = email;
            this.site = site;
            this.nomeVendedor = nomeVendedor;
            this.telefoneVendedor = telefoneVendedor;
        }
        public Fornecedor()
        {
        }

        public List<Fornecedor> ListarTodosFornecedores() {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            fornecedores.Add(new Fornecedor(1, "Fornecedor Jorge", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(2, "Fornecedor Jorge2", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(3, "Fornecedor Jorge3", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(4, "Fornecedor Jorge4", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));

            return fornecedores;
        }
        public List<Fornecedor> ListarFornecedoresPorNome(string nome)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            fornecedores.Add(new Fornecedor(1, "Fornecedor Jorge", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(2, "Fornecedor Jorge2", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(3, "Fornecedor Jorge3", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(4, "Fornecedor Jorge4", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));


            if (nome == null || nome == "")
                return fornecedores;
            else
            {
                List<Fornecedor> retorno = new List<Fornecedor>();
                fornecedores.ForEach(forn =>
                {
                    if (forn.Nome == nome)
                        retorno.Add(forn);
                });
                return retorno;
            }
        }
        public List<Fornecedor> ListarFornecedoresPorEmail(string email)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            fornecedores.Add(new Fornecedor(1, "Fornecedor Jorge", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(2, "Fornecedor Jorge2", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(3, "Fornecedor Jorge3", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(4, "Fornecedor Jorge4", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));


            if (email == null || email == "")
                return fornecedores;
            else
            {
                List<Fornecedor> retorno = new List<Fornecedor>();
                fornecedores.ForEach(forn =>
                {
                    if (forn.Email == email)
                        retorno.Add(forn);
                });
                return retorno;
            }
        }
        public List<Fornecedor> ListarFornecedoresPorNomeVendedor(string nomeVendedor)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            fornecedores.Add(new Fornecedor(1, "Fornecedor Jorge", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(2, "Fornecedor Jorge2", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(3, "Fornecedor Jorge3", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(4, "Fornecedor Jorge4", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));


            if (nomeVendedor == null || nomeVendedor == "")
                return fornecedores;
            else
            {
                List<Fornecedor> retorno = new List<Fornecedor>();
                fornecedores.ForEach(forn =>
                {
                    if (forn.NomeVendedor == nomeVendedor)
                        retorno.Add(forn);
                });
                return retorno;
            }
        }
        public List<Fornecedor> ListarFornecedoresPorTelefoneVendedor(string telefone)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            fornecedores.Add(new Fornecedor(1, "Fornecedor Jorge", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(2, "Fornecedor Jorge2", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(3, "Fornecedor Jorge3", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(4, "Fornecedor Jorge4", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));


            if (telefone == null || telefone == "")
                return fornecedores;
            else
            {
                List<Fornecedor> retorno = new List<Fornecedor>();
                fornecedores.ForEach(forn =>
                {
                    if (forn.Telefone == telefone)
                        retorno.Add(forn);
                });
                return retorno;
            }
        }

        public Fornecedor BuscarFornecedorPorCodigo(int codigo)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            fornecedores.Add(new Fornecedor(1, "Fornecedor Jorge", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(2, "Fornecedor Jorge2", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(3, "Fornecedor Jorge3", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));
            fornecedores.Add(new Fornecedor(4, "Fornecedor Jorge4", "11111111111", "18996xxxxxx", "Dados Bancarios", "forcedor@email", "site.com.br", "Jorginho Vendas", "18996XXXXXX"));

            if (codigo < 0)
                return null;
            else
            {
                Fornecedor retorno = new Fornecedor();
                fornecedores.ForEach(forn =>
                {
                    if (forn.codigo == codigo)
                        retorno = forn;
                });
                return retorno;
            }
        }
        public bool GravarFornecedorCompleto()
        {
            this.Codigo = 1;
            return this.Codigo > 0;
        }
        public bool ExcluirFornecedorFisico(int codigo)
        {
            this.Codigo = 0;
            return this.Codigo > 0;
        }
    }
}
