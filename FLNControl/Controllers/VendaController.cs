using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FLNControl.DAL;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FLNControl.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Novo()
        {
            return View();
        }

        public IActionResult Efetuar([FromBody] JsonElement data)
        {
            CarrinhoCompra carrinho = JsonConvert.DeserializeObject<CarrinhoCompra>(data.ToString());
            VendaDAL dal = new VendaDAL();
            int idVenda = dal.gravarVenda(carrinho);

            return Json(new
            {
                id = idVenda
            });
        }

        public IActionResult Visualizar(int id)
        {
            VendaDAL vdal = new VendaDAL();
            List<Object> dados = vdal.find(id);
            ViewBag.Dados = dados;

            return View();
        }

        public IActionResult Consultar([FromBody] JsonElement data)
        {
            string tipo = data.GetProperty("tipo").ToString();
            VendaDAL dal = new VendaDAL();
            List<Object> vendas = new List<Object>();

            switch (tipo)
            {
                case "nome":
                    string nome = data.GetProperty("termo").ToString();
                    vendas.Add(dal.findByName(nome));
                    break;
            }
            return Json(new
            {
                vendas = vendas,
            });
        }
    }
}
