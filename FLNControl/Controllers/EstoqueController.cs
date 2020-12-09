using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using engenharia.DAL.EstoqueDAL;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers
{
    public class EstoqueController : Controller
    {
        public IActionResult Decrementar([FromBody] JsonElement data)
        {
            EstoqueDAL dal = new EstoqueDAL();
            int prodId = Convert.ToInt32(data.GetProperty("idProduto").ToString());
            int estoqQtd = Convert.ToInt32(data.GetProperty("qtdeEstoque").ToString());


            dal.decrementarEstoque(prodId, estoqQtd);

            return Json(new
            {
                success = true
            });
        }

        public IActionResult Incrementar([FromBody] JsonElement data)
        {
            EstoqueDAL dal = new EstoqueDAL();

            int prodId = Convert.ToInt32(data.GetProperty("idProduto").ToString());
            int estoqQtd = Convert.ToInt32(data.GetProperty("qtdeEstoque").ToString());
            string estoqLote = data.GetProperty("estoqLote").ToString();

            Estoque estoque = new Estoque();
            estoque.IdProduto = prodId;
            estoque.Lote = estoqLote;
            estoque.QtdeEstoque = estoqQtd;
            estoque.Status = "A";

            dal.incrementarEstoque(Convert.ToInt32(estoqLote), estoqQtd);

            return Json(new
            {
                success = true
            });
        }
    }
}
