using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLNControl.Models;
                
namespace FLNControl.Persistence.RepositoryInterface
{
    public interface IFornecedorRespository 
    {
        public Fornecedor BuscarFornecedorPorCodigo(int codigo);
        public Fornecedor GravarFornecedorCompleto(Fornecedor estoque);

    }
}
