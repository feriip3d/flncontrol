using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.Models.Caixa;
using Microsoft.AspNetCore.Mvc;

namespace engenharia.Controllers.GerirCaixa
{
    public class GerirCaixaController : Controller
    {
        private Caixa caixa;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FecharCaixa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FecharCaixaAberto(DateTime data, decimal valor)
        {
            DAL.CaixaDAL.CaixaDAL cx = new DAL.CaixaDAL.CaixaDAL();

            caixa = new Caixa();
            caixa.data_fechamento = data;
            caixa.valor_inicial = valor;

            cx.Fechar(caixa);

            return View("Index", caixa);
        }

        public IActionResult Movimentacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AbrirCaixa(DateTime data, decimal valor)
        {
            DAL.CaixaDAL.CaixaDAL cx = new DAL.CaixaDAL.CaixaDAL();

            caixa = new Caixa();
            caixa.data_abertura = data;
            caixa.valor_inicial = valor;

            caixa = cx.Abrir(caixa);

            return View("Index", caixa);
        }

        [HttpPost]
        public ActionResult MovimentacaoCaixa(string operacao, DateTime data, decimal valor, string motivo, decimal dinheiro)
        {
            return View("Index");
        }
    }
}
