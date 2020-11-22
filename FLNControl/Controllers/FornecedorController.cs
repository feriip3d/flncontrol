using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers
{
    public class FornecedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult BuscarFornecedores() 
        {
            Fornecedor fornecedores = new Fornecedor();
            List<Fornecedor> list = fornecedores.ListarTodosFornecedores();
            return Json(list);
        }
        public IActionResult PesquisarFornecedoresPorNome(string nome)
        {
            Fornecedor fornecedores = new Fornecedor();
            List<Fornecedor> list = fornecedores.ListarFornecedoresPorNome(nome);
            return Json(list);
        }
        public IActionResult PesquisarFornecedoresPorEmail(string email)
        {
            Fornecedor fornecedores = new Fornecedor();
            List<Fornecedor> list = fornecedores.ListarFornecedoresPorEmail(email);
            return Json(list);
        }
        public IActionResult PesquisarFornecedoresPorNomeVendedor(string nome)
        {
            Fornecedor fornecedores = new Fornecedor();
            List<Fornecedor> list = fornecedores.ListarFornecedoresPorNomeVendedor(nome);
            return Json(list);
        }
        public IActionResult PesquisarFornecedoresPorTelefoneVendedor(string telefone)
        {
            Fornecedor fornecedores = new Fornecedor();
            List<Fornecedor> list = fornecedores.ListarFornecedoresPorTelefoneVendedor(telefone);
            return Json(list);
        }

        [HttpPost]
        public IActionResult CadastrarFornecedorCompleto([FromBody] System.Text.Json.JsonElement dados)
        {
            /*
             int codigo;
        string nome;
        string cnpj;
        string telefone;
        string dados_bancarios;
        string email;
        string site;
        string nomeVendedor;
        string telefoneVendedor;
            int codigo, string nome, string cnpj, 
            string telefone, string dados_bancarios, 
            string email, string site, string nomeVendedor, 
            string telefoneVendedor
             
             
             */
            Fornecedor novo = new Fornecedor(
                dados.GetProperty("nome").ToString(),
                dados.GetProperty("cnpj").ToString(),
                dados.GetProperty("telefone").ToString(),
                dados.GetProperty("dados_bancarios").ToString(),
                dados.GetProperty("email").ToString(),
                dados.GetProperty("site").ToString(),
                dados.GetProperty("nomeVendedor").ToString(),
                dados.GetProperty("telefoneVendedor").ToString()
                );

            novo.GravarFornecedorCompleto();

            return Json(novo);
        }
        public IActionResult GravarFornecedorCompleto([FromBody] System.Text.Json.JsonElement dados)
        {
            Fornecedor novo = new Fornecedor(
               dados.GetProperty("nome").ToString(),
               dados.GetProperty("cnpj").ToString(),
               dados.GetProperty("telefone").ToString(),
               dados.GetProperty("dados_bancarios").ToString(),
               dados.GetProperty("email").ToString(),
               dados.GetProperty("site").ToString(),
               dados.GetProperty("nomeVendedor").ToString(),
               dados.GetProperty("telefoneVendedor").ToString()
               );

            bool resultado = novo.GravarFornecedorCompleto();

            return Json(new
            {
                status = resultado
            });
        }
        public IActionResult ExcluirClienteFisico([FromBody] System.Text.Json.JsonElement dados)
        {
            Fornecedor novo = new Fornecedor();

            bool resultado = novo.ExcluirFornecedorFisico(Convert.ToInt32(dados.GetProperty("codigo").ToString()));

            return Json(new
            {
                status = resultado
            });
        }
    }
}
