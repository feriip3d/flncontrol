using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLNControl.Models
{
    public class Transporte
    {
        private List<Produto> _produto;
        private List<Cliente> _cliente;
        private Orcamento.Orcamento _orcamento;

        public List<Produto> produto { get; set; }
        public List<Cliente> cliente { get; set; }

        public Orcamento.Orcamento orcamento { get; set; }
    }
}
