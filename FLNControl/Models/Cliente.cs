using FLNControl.DAL.ClienteDAL;
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

        public List<Cliente> ListarTodosCliente() {
            ClienteDAL dal = new ClienteDAL();
            return dal.ListarTodosCliente();
        }
        public List<Cliente> ListarClientesPorNome(string nome)
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.ListarClientesPorNome(nome);
        }
        public List<Cliente> ListarClientesPorCPF(string cpf)
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.ListarClientesPorCPF(cpf);
        }
        public List<Cliente> ListarClientesPorTelefone(string telefone)
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.ListarClientesPorTelefone(telefone);
        }
        public Cliente BuscarClientesPorCodigo(int codigo)
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.BuscarClientesPorCodigo(codigo);
        }

        public bool GravarClienteCompleto()
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.GravarClienteCompleto(this);
        }
        public bool ExcluirCliente(int codigo)
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.ExcluirCliente(codigo);
        }  
        public bool AlterarClienteCompleto()
        {
            ClienteDAL dal = new ClienteDAL();
            return dal.AlterarClienteCompleto(this);
        }
    }
}
