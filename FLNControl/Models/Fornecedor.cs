using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLNControl.DAL.FornecedorDAL;

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
            FornecedorDAL dal = new FornecedorDAL();
            return dal.ListarTodosFornecedores();
        }
        public List<Fornecedor> ListarFornecedoresPorNome(string nome)
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.ListarTodosFornecedores();
        }
        public List<Fornecedor> ListarFornecedoresPorEmail(string email)
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.ListarFornecedoresPorEmail(email);
        }
        public List<Fornecedor> ListarFornecedoresPorNomeVendedor(string nomeVendedor)
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.ListarFornecedoresPorNomeVendedor(nomeVendedor);
        }
        public List<Fornecedor> ListarFornecedoresPorTelefoneVendedor(string telefone)
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.ListarFornecedoresPorTelefoneVendedor(telefone);
        }

        public Fornecedor BuscarFornecedorPorCodigo(int codigo)
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.BuscarFornecedorPorCodigo(codigo);
        }
        public bool GravarFornecedorCompleto()
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.GravarFornecedorCompleto(this);
        }
        public bool ExcluirFornecedorFisico(int codigo)
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.ExcluirFornecedorFisico(codigo);
        }
        public bool AlterarFornecedorCompleto()
        {
            FornecedorDAL dal = new FornecedorDAL();
            return dal.AlterarFornecedorCompleto(this);
        }
    }
}
