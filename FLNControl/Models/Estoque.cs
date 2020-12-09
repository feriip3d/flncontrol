namespace FLNControl.Models
{
    public class Estoque
    {
        private int _id;
        private int _idProduto;
        private string _lote;
        private string _status;
        private decimal _qtdeEstoque;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int IdProduto
        {
            get => _idProduto;
            set => _idProduto = value;
        }

        public decimal QtdeEstoque
        {
            get => _qtdeEstoque;
            set => _qtdeEstoque = value;
        }

        public string Lote
        {
            get => _lote;
            set => _lote = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        // Utilizado em estornos
        public void acrescentarEstoque(decimal qtde)
        {
            QtdeEstoque += qtde;
        }

        // Utilizado em vendas
        public void decrementarEstoque(decimal qtde)
        {
            QtdeEstoque -= qtde;
        }
    }
}
