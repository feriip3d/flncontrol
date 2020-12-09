using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            List<Produto> lista = model.ListarProduto();
            return View("GravarOrcamento", lista);
        }

        public IActionResult Excluir(int orc_codigo)
        {
            OrcamentoDAL or = new OrcamentoDAL();

            bool excluir = or.Excluir(orc_codigo);

            OrcamentoDAL model = new OrcamentoDAL();

            List<FLNControl.Models.Orcamento.Orcamento> lista = model.ListarOrcamento();
            return View("Index", lista);
        }
    }
}
