using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Endereco
    {
        int codigo;
        string rua;
        string bairro;
        string tipo;
        string cep;
        string descricao;
        int codigoCidade;
        int codigoCliente;
        int codigoColaborador;
        int codigoFornecedor;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Rua { get => rua; set => rua = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public int CodigoCidade { get => codigoCidade; set => codigoCidade = value; }
        public int CodigoCliente { get => codigoCliente; set => codigoCliente = value; }
        public int CodigoColaborador { get => codigoColaborador; set => codigoColaborador = value; }
        public int CodigoFornecedor { get => codigoFornecedor; set => codigoFornecedor = value; }

        public Endereco(int codigo, string rua, string bairro, string tipo, string cep, string descricao, int codigoCidade, int codigoCliente, int codigoColaborador, int codigoFornecedor)
        {
            this.codigo = codigo;
            this.rua = rua;
            this.bairro = bairro;
            this.tipo = tipo;
            this.cep = cep;
            this.descricao = descricao;
            this.codigoCidade = codigoCidade;
            this.codigoCliente = codigoCliente;
            this.codigoColaborador = codigoColaborador;
            this.codigoFornecedor = codigoFornecedor;
        }
        public Endereco(string rua, string bairro, string tipo, string cep, string descricao, int codigoCidade, int codigoCliente, int codigoColaborador, int codigoFornecedor)
        {
            this.codigo = 0;
            this.rua = rua;
            this.bairro = bairro;
            this.tipo = tipo;
            this.cep = cep;
            this.descricao = descricao;
            this.codigoCidade = codigoCidade;
            this.codigoCliente = codigoCliente;
            this.codigoColaborador = codigoColaborador;
            this.codigoFornecedor = codigoFornecedor;
        }
        public Endereco()
        {
        }

        public List<Endereco> ListarTodosEnderecos() {
            List<Endereco> enderecos = new List<Endereco>();

            enderecos.Add(new Endereco (1, "Rua não sei qual é", "Bairro desconheço", "Casa do Cliente", "19200-000", "descrição, não sei", 1, 1, 0, 0));
            enderecos.Add(new Endereco (2, "Rua não sei qual é", "Bairro desconheço", "Casa do Colaborador", "19200-000", "descrição, não sei", 1, 0, 1, 0));
            enderecos.Add(new Endereco (3, "Rua não sei qual é", "Bairro desconheço", "Loja do Fornecedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco (4, "Rua não sei qual é", "Bairro desconheço", "Casa do Vendedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco (5, "Rua não sei qual é", "Bairro desconheço", "Endereco de entrega", "19200-000", "descrição, não sei", 1, 1, 0, 0));


            return enderecos;         
        }
        public List<Endereco> ListarTodosEnderecosDeCliente(int codigoCliente = 0)
        {
            List<Endereco> enderecos = new List<Endereco>();

            enderecos.Add(new Endereco(1, "Rua não sei qual é", "Bairro desconheço", "Casa do Cliente", "19200-000", "descrição, não sei", 1, 1, 0, 0));
            enderecos.Add(new Endereco(2, "Rua não sei qual é", "Bairro desconheço", "Casa do Colaborador", "19200-000", "descrição, não sei", 1, 0, 1, 0));
            enderecos.Add(new Endereco(3, "Rua não sei qual é", "Bairro desconheço", "Loja do Fornecedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(4, "Rua não sei qual é", "Bairro desconheço", "Casa do Vendedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(5, "Rua não sei qual é", "Bairro desconheço", "Endereco de entrega", "19200-000", "descrição, não sei", 1, 1, 0, 0));

            if (codigoCliente <= 0) {
                List<Endereco> retorno = new List<Endereco>();
                enderecos.ForEach(end =>
                {
                    if (end.codigoCliente > 0)
                        retorno.Add(end);
                });
                return retorno;
            }
            else
            {
                List<Endereco> retorno = new List<Endereco>();
                enderecos.ForEach(end =>
                {
                    if (end.codigoCliente == codigoCliente)
                        retorno.Add(end);
                });
                return retorno;
            }
        }
        public List<Endereco> ListarTodosEnderecosDeColaborador(int codigoColaborador = 0)
        {
            List<Endereco> enderecos = new List<Endereco>();

            enderecos.Add(new Endereco(1, "Rua não sei qual é", "Bairro desconheço", "Casa do Cliente", "19200-000", "descrição, não sei", 1, 1, 0, 0));
            enderecos.Add(new Endereco(2, "Rua não sei qual é", "Bairro desconheço", "Casa do Colaborador", "19200-000", "descrição, não sei", 1, 0, 1, 0));
            enderecos.Add(new Endereco(3, "Rua não sei qual é", "Bairro desconheço", "Loja do Fornecedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(4, "Rua não sei qual é", "Bairro desconheço", "Casa do Vendedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(5, "Rua não sei qual é", "Bairro desconheço", "Endereco de entrega", "19200-000", "descrição, não sei", 1, 1, 0, 0));

            if (codigoColaborador <= 0)
            {
                List<Endereco> retorno = new List<Endereco>();
                enderecos.ForEach(end =>
                {
                    if (end.codigoColaborador > 0)
                        retorno.Add(end);
                });
                return retorno;
            }
            else
            {
                List<Endereco> retorno = new List<Endereco>();
                enderecos.ForEach(end =>
                {
                    if (end.codigoColaborador == codigoColaborador)
                        retorno.Add(end);
                });
                return retorno;
            }
        }
        public List<Endereco> ListarTodosEnderecosDeFornecedor(int codigoFornecedor = 0)
        {
            List<Endereco> enderecos = new List<Endereco>();

            enderecos.Add(new Endereco(1, "Rua não sei qual é", "Bairro desconheço", "Casa do Cliente", "19200-000", "descrição, não sei", 1, 1, 0, 0));
            enderecos.Add(new Endereco(2, "Rua não sei qual é", "Bairro desconheço", "Casa do Colaborador", "19200-000", "descrição, não sei", 1, 0, 1, 0));
            enderecos.Add(new Endereco(3, "Rua não sei qual é", "Bairro desconheço", "Loja do Fornecedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(4, "Rua não sei qual é", "Bairro desconheço", "Casa do Vendedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(5, "Rua não sei qual é", "Bairro desconheço", "Endereco de entrega", "19200-000", "descrição, não sei", 1, 1, 0, 0));

            if (codigoFornecedor <= 0)
            {
                List<Endereco> retorno = new List<Endereco>();
                enderecos.ForEach(end =>
                {
                    if (end.codigoColaborador > 0)
                        retorno.Add(end);
                });
                return retorno;
            }
            else
            {
                List<Endereco> retorno = new List<Endereco>();
                enderecos.ForEach(end =>
                {
                    if (end.codigoColaborador == codigoFornecedor)
                        retorno.Add(end);
                });
                return retorno;
            }
        }
        public Endereco PesquisarEnderecoPorCodigo(int codigo = 0)
        {
            List<Endereco> enderecos = new List<Endereco>();

            enderecos.Add(new Endereco(1, "Rua não sei qual é", "Bairro desconheço", "Casa do Cliente", "19200-000", "descrição, não sei", 1, 1, 0, 0));
            enderecos.Add(new Endereco(2, "Rua não sei qual é", "Bairro desconheço", "Casa do Colaborador", "19200-000", "descrição, não sei", 1, 0, 1, 0));
            enderecos.Add(new Endereco(3, "Rua não sei qual é", "Bairro desconheço", "Loja do Fornecedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(4, "Rua não sei qual é", "Bairro desconheço", "Casa do Vendedor", "19200-000", "descrição, não sei", 1, 0, 0, 1));
            enderecos.Add(new Endereco(5, "Rua não sei qual é", "Bairro desconheço", "Endereco de entrega", "19200-000", "descrição, não sei", 1, 1, 0, 0));

            if (codigo <= 0)
                return null;
            else
            {
                Endereco retorno = new Endereco();
                enderecos.ForEach(end =>
                {
                    if (end.codigo == codigo)
                        retorno = end;
                });
                return retorno;
            }
        }

        public bool GravarEnderecoCompleto()
        {
            this.codigo = 10;
            return this.codigo > 0;
        }
    }
}
