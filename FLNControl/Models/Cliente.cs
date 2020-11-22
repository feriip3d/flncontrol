using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Cliente
    {
        int codigo;
        string nome;
        string cpf;
        string telefone;
        string email;
        string dataNascimento;
        int fiado;
        double valorLimiteFiado;
        int diasVencimento;
        int parcelasLimiteFiado;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Email { get => email; set => email = value; }
        public string DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        public int Fiado { get => fiado; set => fiado = value; }
        public double ValorLimiteFiado { get => valorLimiteFiado; set => valorLimiteFiado = value; }
        public int DiasVencimento { get => diasVencimento; set => diasVencimento = value; }
        public int ParcelasLimiteFiado { get => parcelasLimiteFiado; set => parcelasLimiteFiado = value; }

        public Cliente(int codigo, string nome, string cpf, string telefone, 
            string email, string dataNascimento, int fiado, double valorLimiteFiado, 
            int diasVencimento, int parcelasLimiteFiado)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.email = email;
            this.dataNascimento = dataNascimento;
            this.fiado = fiado;
            this.valorLimiteFiado = valorLimiteFiado;
            this.diasVencimento = diasVencimento;
            this.parcelasLimiteFiado = parcelasLimiteFiado;
        }
        public Cliente(string nome, string cpf, string telefone, string email, 
            string dataNascimento, int fiado , double valorLimiteFiado , 
            int diasVencimento , int parcelasLimiteFiado )
        {
            this.codigo = 0;
            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.email = email;
            this.dataNascimento = dataNascimento;
            this.fiado = fiado;
            this.valorLimiteFiado = valorLimiteFiado;
            this.diasVencimento = diasVencimento;
            this.parcelasLimiteFiado = parcelasLimiteFiado;
        }
        public Cliente()
        {
        }

        public List<Cliente> ListarTodosCliente(string query) {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente(1, "Jorginho", "11111111111", "1899611111", "jorginho@email.com", "", 0, 2000.00,2,2));
            clientes.Add(new Cliente(2, "Jorginho2", "2222222222", "1899622222", "jorginho@email.com", "", 0, 2000.00, 2,2));
            clientes.Add(new Cliente(3, "Jorginho3", "33333333333", "1899633333", "jorginho@email.com", "", 0, 2000.00, 2,2));
            clientes.Add(new Cliente(4, "Jorginho4", "44444444444", "1899644444", "jorginho@email.com", "", 0, 2000.00, 2,2));
            clientes.Add(new Cliente(5, "Jorginho5", "55555555555", "189965555555", "jorginho@email.com", "", 0, 2000.00, 2,2));
            
            return clientes;    
        }
        public List<Cliente> ListarClientesPorNome(string nome)
        {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente(1, "Jorginho", "11111111111", "1899611111", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(2, "Jorginho2", "2222222222", "1899622222", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(3, "Jorginho3", "33333333333", "1899633333", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(4, "Jorginho4", "44444444444", "1899644444", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(5, "Jorginho5", "55555555555", "189965555555", "jorginho@email.com", "", 0, 2000.00, 2, 2));

            if (nome == null || nome == "")
                return clientes;
            else
            {
                List<Cliente> retorno = new List<Cliente>();
                clientes.ForEach(cli =>
                {
                    if (cli.Nome == nome)
                        retorno.Add(cli);
                });
                return retorno;
            }
        }
        public List<Cliente> ListarClientesPorCPF(string cpf)
        {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente(1, "Jorginho", "11111111111", "1899611111", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(2, "Jorginho2", "2222222222", "1899622222", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(3, "Jorginho3", "33333333333", "1899633333", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(4, "Jorginho4", "44444444444", "1899644444", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(5, "Jorginho5", "55555555555", "189965555555", "jorginho@email.com", "", 0, 2000.00, 2, 2));

            if (cpf == null || cpf == "")
                return clientes;
            else
            {
                List<Cliente> retorno = new List<Cliente>();
                clientes.ForEach(cli =>
                {
                    if (cli.Cpf == cpf)
                        retorno.Add(cli);
                });
                return retorno;
            }
        }
        public List<Cliente> ListarClientesPorTelefone(string telefone)
        {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente(1, "Jorginho", "11111111111", "1899611111", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(2, "Jorginho2", "2222222222", "1899622222", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(3, "Jorginho3", "33333333333", "1899633333", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(4, "Jorginho4", "44444444444", "1899644444", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(5, "Jorginho5", "55555555555", "189965555555", "jorginho@email.com", "", 0, 2000.00, 2, 2));

            if (telefone == null || telefone == "")
                return clientes;
            else
            {
                List<Cliente> retorno = new List<Cliente>();
                clientes.ForEach(cli =>
                {
                    if (cli.Telefone == telefone)
                        retorno.Add(cli);
                });
                return retorno;
            }
        }
        public Cliente BuscarClientesPorCodigo(int codigo)
        {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente(1, "Jorginho", "11111111111", "1899611111", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(2, "Jorginho2", "2222222222", "1899622222", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(3, "Jorginho3", "33333333333", "1899633333", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(4, "Jorginho4", "44444444444", "1899644444", "jorginho@email.com", "", 0, 2000.00, 2, 2));
            clientes.Add(new Cliente(5, "Jorginho5", "55555555555", "189965555555", "jorginho@email.com", "", 0, 2000.00, 2, 2));

            if (codigo < 0)
                return null;
            else
            {
                Cliente retorno = new Cliente();
                clientes.ForEach(cli =>
                {
                    if (cli.codigo == codigo)
                        retorno = cli;
                });
                return retorno;
            }
        }
        public bool GravarClienteCompleto()
        {
            this.Codigo = 1;
            return this.Codigo > 0;
        }
        public bool ExcluirCliente(int codigo)
        {
            this.Codigo = 0;
            return this.Codigo > 0;
        }
        
    }
}
