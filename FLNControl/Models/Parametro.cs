using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Parametro
    {
        private int _id;
        private string _razaoSocial;
        private string _nomeFantasia;
        private string _site;
        private string _email;
        private string _cnpj;
        private string _inscricaoEstadual;
        private string _telefone;
        private string _urlLogoGrande;
        private string _urlLogoPequeno;
        private string _endereco;
        private string _cidade;
        private string _UF;

        public string RazaoSocial
        {
            get => _razaoSocial;
            set => _razaoSocial = value;
        }

        public string NomeFantasia
        {
            get => _nomeFantasia;
            set => _nomeFantasia = value;
        }

        public string Site
        {
            get => _site;
            set => _site = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Cnpj
        {
            get => _cnpj;
            set => _cnpj = value;
        }

        public string InscricaoEstadual
        {
            get => _inscricaoEstadual;
            set => _inscricaoEstadual = value;
        }

        public string Telefone
        {
            get => _telefone;
            set => _telefone = value;
        }

        public string UrlLogoGrande
        {
            get => _urlLogoGrande;
            set => _urlLogoGrande = value;
        }

        public string UrlLogoPequeno
        {
            get => _urlLogoPequeno;
            set => _urlLogoPequeno = value;
        }

        public string Endereco
        {
            get => _endereco;
            set => _endereco = value;
        }

        public string Cidade
        {
            get => _cidade;
            set => _cidade = value;
        }

        public string Uf
        {
            get => _UF;
            set => _UF = value;
        }
        public int Id { get => _id; set => _id = value; }
    }
}
