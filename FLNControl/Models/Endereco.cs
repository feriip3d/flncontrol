using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLNControl.DAL.EnderecoDAL;

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
            EnderecoDAL dal = new EnderecoDAL();
            return dal.ListarTodosEnderecos();
        }
        public List<Endereco> ListarTodosEnderecosDeCliente(int codigoCliente = 0)
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.ListarTodosEnderecosDeCliente(codigoCliente);
        }
        public List<Endereco> ListarTodosEnderecosDeColaborador(int codigoColaborador = 0)
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.ListarTodosEnderecosDeColaborador(codigoColaborador);
        }
        public List<Endereco> ListarTodosEnderecosDeFornecedor(int codigoFornecedor = 0)
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.ListarTodosEnderecosDeFornecedor(codigoFornecedor);
        }
        public Endereco PesquisarEnderecoPorCodigo(int codigo = 0)
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.PesquisarEnderecoPorCodigo(codigo);
        }

        public bool GravarEnderecoCompleto()
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.GravarEnderecoCompleto(this);
        }
        public bool ExcluirEnderecoFisico(int codigo)
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.ExcluirEnderecoFisico(codigo);
        }
        public bool AlterarEnderecoCompleto()
        {
            EnderecoDAL dal = new EnderecoDAL();
            return dal.AlterarEnderecoCompleto(this);
        }
    }
}
