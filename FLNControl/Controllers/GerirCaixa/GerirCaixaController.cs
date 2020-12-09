using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL.CaixaDAL;
using engenharia.Models.Caixa;
using engenharia.Models.CaixaMovimentacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace engenharia.Controllers.GerirCaixa
{
    public class GerirCaixaController : Controller
    {
        private Caixa caixa;
        public GerirCaixaController()
        {
            CaixaDAL cx = new CaixaDAL();
            caixa = new Caixa();

            caixa = cx.Atual();
        }
        public IActionResult Index()
        {
            if (caixa.status == null || caixa.status == "Fechado")
            {
                caixa.status = "Fechado";
                return View(caixa);
            }
            else
            {
                caixa.status = "Aberto";
                return View("ImpossivelEntrar");
            }

            return View(caixa);
        }
        public IActionResult Movimentacao()
        {
            if (caixa.status == "Aberto")
                return View(caixa);
            else
            {
                return View("ImpossivelEntrar");
            }

        }

        public IActionResult FecharCaixa()
        {
            if(caixa.status == "Aberto")
                return View(caixa);
            else
            {
                return View("ImpossivelEntrar");
            }
        }

        [HttpPost]
        public JsonResult FecharCaixaAberto(DateTime data, decimal valor)
        {
            CaixaDAL cx = new CaixaDAL();
            bool created = true;

            caixa.data_fechamento = data;
            caixa.valor_inicial = valor;



            if (caixa.status == "Fechado")
            {
                created = false;
                return Json(new
                {
                    retorno = created,
                });
            }
            else
            {
                cx.Fechar(caixa, (int)HttpContext.Session.GetInt32("usu_id"));
                return Json(new
                {
                    retorno = created,
                });
            }

        }


        [HttpPost]
        public JsonResult AbrirCaixa(DateTime data, decimal valor)
        {
            CaixaDAL cx = new CaixaDAL();
            bool created = true;
            if (caixa.status == "Aberto")
            {

                created = false;
                return Json(new
                {
                    retorno = created,
                });

            }
            else
            {
                caixa = new Caixa();
                caixa.data_abertura = data;
                caixa.valor_inicial = valor;

                caixa = cx.Abrir(caixa, (int)HttpContext.Session.GetInt32("usu_id"));
                caixa.status = "Aberto";
                return Json(new
                {
                    retorno = created,
                });
            }
        }

        [HttpPost]
        public JsonResult MovimentacaoCaixa(string operacao, DateTime data, decimal valor, string motivo, decimal dinheiro)
        {
            CaixaDAL cx = new CaixaDAL();
            bool created = true;

            if (caixa.status == "Fechado")
            {
                created = false;
                return Json(new
                {
                    retorno = created,
                });
            }
            else
            {

                Movimentacao mov = new Movimentacao();
                mov.caixa_id = caixa.id;
                mov.colaborador_id = (int) HttpContext.Session.GetInt32("usu_id");
                mov.data = data;
                mov.motivo = motivo;
                mov.tipo = operacao;
                mov.valor = dinheiro;

                caixa.valor_final += (mov.tipo == "Sangria" ? -mov.valor : mov.valor);

                cx.Atualizar(mov);

                return Json(new
                {
                    retorno = created,
                });
            }
        }
    }
}
