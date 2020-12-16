using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Editar(string codigo)
        {
            return View("Editar",codigo);
        }
        
        public IActionResult BuscarClientes() 
        {
            Cliente clientes = new Cliente();
            List<Cliente> list = clientes.ListarTodosCliente();
            return Json(list);
        }
        public IActionResult PesquisarClientesPorNome(string nome)
        {
            Cliente clientes = new Cliente();
            List<Cliente> list = clientes.ListarClientesPorNome(nome);
            return Json(list);
        }
        public IActionResult PesquisarClientesPorCPF(string cpf)
        {
            Cliente clientes = new Cliente();
            List<Cliente> list = clientes.ListarClientesPorCPF(cpf);
            return Json(list);
        }
        public IActionResult PesquisarClientesPorTelefone(string telefone)
        {
            Cliente clientes = new Cliente();
            List<Cliente> list = clientes.ListarClientesPorTelefone(telefone);
            return Json(list);
        }
        public IActionResult BuscarClientesPorCodigo(int codigo)
        {
            Cliente cliente = new Cliente().BuscarClientesPorCodigo(codigo);
            return Json(cliente);
        }
        
        [HttpPost]
        public IActionResult CadastrarClienteCompleto([FromBody] System.Text.Json.JsonElement dados)
        {
            Cliente novo = new Cliente(
                dados.GetProperty("nome").ToString(),
                dados.GetProperty("cpf").ToString(),
                dados.GetProperty("telefone").ToString(),
                dados.GetProperty("email").ToString(),
                dados.GetProperty("dataNascimento").ToString(),
                Convert.ToInt32(dados.GetProperty("fiado").ToString()),
                Convert.ToDouble(dados.GetProperty("valorLimiteFiado").ToString()),
                Convert.ToInt32(dados.GetProperty("dataVencimento").ToString()),
                Convert.ToInt32(dados.GetProperty("parcelasLimiteFiado").ToString())
                );

            novo.GravarClienteCompleto();

            return Json(novo);
        }
        public IActionResult GravarClienteCompleto([FromBody] System.Text.Json.JsonElement dados)
        {
            Cliente novo = new Cliente(
                Convert.ToInt32(dados.GetProperty("codigo").ToString()),
                dados.GetProperty("nome").ToString(),
                dados.GetProperty("cpf").ToString(),
                dados.GetProperty("telefone").ToString(),
                dados.GetProperty("email").ToString(),
                dados.GetProperty("dataNascimento").ToString(),
                Convert.ToInt32(dados.GetProperty("fiado").ToString()),
                Convert.ToDouble(dados.GetProperty("valorLimiteFiado").ToString()),
                Convert.ToInt32(dados.GetProperty("dataVencimento").ToString()),
                Convert.ToInt32(dados.GetProperty("parcelasLimiteFiado").ToString())
                );

            bool resultado = novo.GravarClienteCompleto();
            
            return Json(new
            {
                status = resultado
            });
        }
        public IActionResult ExcluirClienteFisico([FromBody] System.Text.Json.JsonElement dados)
        {
            Cliente novo = new Cliente();

            bool resultado = novo.ExcluirCliente(Convert.ToInt32(dados.GetProperty("codigo").ToString()));

            return Json(new {
                status = resultado
            });
        }
    }
}
