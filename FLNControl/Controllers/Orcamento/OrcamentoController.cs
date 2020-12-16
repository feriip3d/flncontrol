using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL.ProdutoDAL;
using FLNControl.DAL.OrcamentoDAL;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers.Orcamento
{
    public class OrcamentoController : Controller
    {
        public IActionResult Index()
        {

            OrcamentoDAL model = new OrcamentoDAL();
            List<FLNControl.Models.Orcamento.Orcamento> lista = model.ListarOrcamento();
            return View( lista);
        }

        public IActionResult GravarOrcamento()
        {
            OrcamentoDAL model = new OrcamentoDAL();
            Transporte transp = new Transporte();
            transp.produto = model.ListarProduto();
            transp.cliente = model.ListarCliente();

            return View("GravarOrcamento", transp);
        }


        public IActionResult CadastrarOrcamento(int statusSelected, DateTime dataVencimento, List<string> listProdutos, List<string> valorVenda, List<int> quantidadeProdutos, List<int> listIdsProdutos)
        {
            OrcamentoDAL model = new OrcamentoDAL();
            Models.Orcamento.Orcamento or = new Models.Orcamento.Orcamento();
            List<Produto> listaProdutoBanco = new List<Produto>();

            or.cli_Id = statusSelected;
            or.Data_Validade = dataVencimento;
            or.Valor_Total = 0;

            for (int i = 0; i < listProdutos.Count; i++)
            {
                if(quantidadeProdutos[i] > 0)
                    or.Valor_Total += quantidadeProdutos[i] * Convert.ToDecimal(valorVenda[i]);

                Produto produtoAux = new Produto();
                produtoAux.Id = listIdsProdutos[i];
                produtoAux.quantidade = quantidadeProdutos[i];
                produtoAux.ValorVenda = Convert.ToDecimal( valorVenda[i]);
                listaProdutoBanco.Add(produtoAux);
            }
            or.Valor_Desconto = 0;
            or.col_Id = 7;

            if(or.Valor_Total != 0)
            {
                int retorno = model.save(or);
                listaProdutoBanco.ForEach(t=> {
                    t.orcamentoId = retorno;
                    model.saveProdutoOrcamento(t);
                });
            }
            return RedirectToAction("Index","Orcamento");
        }


        public IActionResult Excluir(int orc_codigo)
        {
            OrcamentoDAL or = new OrcamentoDAL();

            bool excluir = or.Excluir(orc_codigo);

            OrcamentoDAL model = new OrcamentoDAL();

            List<FLNControl.Models.Orcamento.Orcamento> lista = model.ListarOrcamento();
            return View("Index", lista);
        }

        public IActionResult PesquisarProdutoPorNome(string descricao)
        {
            ProdutoDAL prods = new ProdutoDAL();
            List<Produto> list = prods.findByDescription(descricao);
            return Json(list);
        }

        public IActionResult Editar(int orc_codigo, int cli_codigo)
        {
            OrcamentoDAL model = new OrcamentoDAL();
            Transporte transp = new Transporte();
            transp.orcamento = model.selectOrcamento(orc_codigo);
            transp.produto = model.selectProduto(orc_codigo);
            transp.cliente = new List<Cliente>();
            transp.cliente.Add(model.selectCliente(cli_codigo));

            return View("EditarOrcamento", transp);
        }

        public IActionResult EditarOrcamento(int statusSelected, DateTime dataVencimento, List<string> listProdutos, List<string> valorVenda, List<int> quantidadeProdutos, List<int> listIdsProdutos, int idOrcamento)
        {
            OrcamentoDAL model = new OrcamentoDAL();
            Models.Orcamento.Orcamento or = new Models.Orcamento.Orcamento();
            List<Produto> listaProdutoBanco = new List<Produto>();

            or.Id = idOrcamento;
            or.cli_Id = statusSelected;
            or.Data_Validade = dataVencimento;
            or.Valor_Total = 0;

            for (int i = 0; i < listProdutos.Count; i++)
            {
                if (quantidadeProdutos[i] > 0)
                    or.Valor_Total += quantidadeProdutos[i] * Convert.ToDecimal(valorVenda[i]);

                Produto produtoAux = new Produto();
                produtoAux.Id = listIdsProdutos[i];
                produtoAux.quantidade = quantidadeProdutos[i];
                produtoAux.ValorVenda = Convert.ToDecimal(valorVenda[i]);
                listaProdutoBanco.Add(produtoAux);
            }
            or.Valor_Desconto = 0;
            or.col_Id = 7;

            if (or.Valor_Total != 0)
            {
                int retorno = model.update(or);
                listaProdutoBanco.ForEach(t => {
                    t.orcamentoId = idOrcamento;
                    model.updateProdutoOrcamento(t);
                });
            }

            return RedirectToAction("Index", "Orcamento");
        }
    }
}
