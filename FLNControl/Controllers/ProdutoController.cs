using System;
using System.Collections.Generic;
using System.Text.Json;
using engenharia.DAL.ProdutoDAL;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers
{
    public class ProdutoController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [Route("Produto/Visualizar/{id}")]
        public IActionResult Visualizar(int id)
        {
            ProdutoDAL dal = new ProdutoDAL();
            Produto prod = dal.find(id);

            ViewBag.Produto = prod;
            return View();
        }

        [Route("Produto/Alterar/{id}")]
        public IActionResult Alterar(int id)
        {
            ProdutoDAL dal = new ProdutoDAL();
            Produto prod = dal.find(id);

            ViewBag.Produto = prod;
            return View();
        }

        [Route("Produto/Listar")]
        public IActionResult Listar(int id)
        {
            ProdutoDAL dal = new ProdutoDAL();
            List<Produto> produtos = dal.findAll();

            return Json(new
            {
                produtos = produtos
            });
        }

        [Route("Produto/Alterar/Gravar")]
        public IActionResult Update([FromBody] JsonElement data)
        {
            ProdutoDAL dal = new ProdutoDAL();
            Produto prod = dal.find(Convert.ToInt32(data.GetProperty("id").ToString()));
            prod.Descricao = data.GetProperty("desc").ToString();
            prod.Categoria = data.GetProperty("categ").ToString();
            prod.Marca = data.GetProperty("marca").ToString();
            prod.ValorCompra = Convert.ToDecimal(data.GetProperty("precoCompra").ToString());
            prod.ValorVenda = Convert.ToDecimal(data.GetProperty("precoVenda").ToString());
            dal.save(prod);

            return Json(new
            {
                success = true
            });
        }

        [Route("Produto/Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            ProdutoDAL dal = new ProdutoDAL();
            dal.delete(id);

            return Json(new
            {
                success = true
            });
        }

        // GET
        public IActionResult Cadastrar()
        {
            return View();
        }


        // GET
        public IActionResult Novo([FromBody] JsonElement data)
        {

            Produto prod = new Produto();
            prod.Descricao = data.GetProperty("desc").ToString();
            prod.Categoria = data.GetProperty("categ").ToString();
            prod.Marca = data.GetProperty("marca").ToString();
            prod.ValorCompra = Convert.ToDecimal(data.GetProperty("precoCompra").ToString());
            prod.ValorVenda = Convert.ToDecimal(data.GetProperty("precoVenda").ToString());

            ProdutoDAL dal = new ProdutoDAL();
            int id = dal.save(prod);
            bool success = id > 0;

            return Json(new
            {
                success = success,
                id = id,
            });
        }

        //POST
        public IActionResult Consultar([FromBody] JsonElement data)
        {
            string tipo = data.GetProperty("tipo").ToString();
            ProdutoDAL dal = new ProdutoDAL();
            List<Produto> listaProdutos = new List<Produto>();

            switch (tipo)
            {
                case "codigo":
                    int id = Convert.ToInt32(data.GetProperty("termo").ToString());
                    listaProdutos.Add(dal.find(id));
                    break;

                case "categ":
                    string categ = data.GetProperty("termo").ToString();
                    listaProdutos = dal.findByCategory(categ);
                    break;

                case "brand":
                    string brand = data.GetProperty("termo").ToString();
                    listaProdutos = dal.findByBrand(brand);
                    break;

                case "desc":
                default:
                    string desc = data.GetProperty("termo").ToString();
                    listaProdutos = dal.findByDescription(desc);
                    break;
            }
            return Json(new
            {
                produtos = listaProdutos,
                total = listaProdutos.Count
            });
        }

        public IActionResult ListarTodos()
        {
            ProdutoDAL dal = new ProdutoDAL();
            List<Produto> listaProdutos = new List<Produto>();

            listaProdutos = dal.findAll();
            return Json(new
            {
                produtos = listaProdutos,
                total = listaProdutos.Count
            });
        }
    }
}
