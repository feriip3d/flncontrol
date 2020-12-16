using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Produto
    {
        private int _id;
        private string _categoria;
        private string _descricao;
        private string _marca;
        private decimal _valor_venda;
        private decimal _valor_compra;
        private Cliente _cliente;
        private int _quantidade;
        private int _orcamentoId;

        public Cliente cliente { get; set; }

        public int quantidade { get; set; }
        public int orcamentoId { get; set; }
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Categoria
        {
            get => _categoria;
            set => _categoria = value;
        }

        public string Descricao
        {
            get => _descricao;
            set => _descricao = value;
        }

        public string Marca
        {
            get => _marca;
            set => _marca = value;
        }

        public decimal ValorVenda
        {
            get => _valor_venda;
            set => _valor_venda = value;
        }

        public decimal ValorCompra
        {
            get => _valor_compra;
            set => _valor_compra = value;
        }
    }
}
