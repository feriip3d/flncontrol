using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FLNControl.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Efetuar([FromBody] JsonElement data)
        {
            string cpf = data.GetProperty("cpf").ToString();
            string nomeCli = data.GetProperty("nomeCli").ToString();
            string endereco = data.GetProperty("endereco").ToString();
            string numero = data.GetProperty("numero").ToString();
            string bairro = data.GetProperty("bairro").ToString();
            string cidade = data.GetProperty("cidade").ToString();
            string uf = data.GetProperty("uf").ToString();
            string formaPagamento = data.GetProperty("formaPagamento").ToString();
            string parcelas = data.GetProperty("parcelas").ToString();
            var listaProdutos = data.GetProperty("listaProdutos").EnumerateArray().ToArray()[0];

            VendaDAL dal = new VendaDAL();
            dal.gravarVenda(cpf, nomeCli, endereco, numero, bairro, cidade, uf, formaPagamento, parcelas, listaProdutos);

            

            return Json(new
            {
            });
       }
    }
}
