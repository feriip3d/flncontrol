using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FLNControl.Models;

namespace FLNControl.Controllers
{
    public class EnderecoController : Controller
    {
        public IActionResult GravarEnderecoCompleto([FromBody] System.Text.Json.JsonElement dados)
        {
            /*string rua, string bairro, string tipo, string cep, 
             * string descricao, int codigoCidade, int codigoCliente, 
             * int codigoColaborador, int codigoFornecedor*/
            Endereco endereco = new Endereco(
                dados.GetProperty("rua").ToString(),
                dados.GetProperty("bairro").ToString(),
                dados.GetProperty("tipo").ToString(),
                dados.GetProperty("cep").ToString(),
                dados.GetProperty("descricao").ToString(),
                Convert.ToInt32(dados.GetProperty("codigoCidade").ToString()),
                Convert.ToInt32(dados.GetProperty("codigoCliente").ToString()),
                Convert.ToInt32(dados.GetProperty("codigoColaborador").ToString()),
                Convert.ToInt32(dados.GetProperty("codigoFornecedor").ToString())
                );

            endereco.GravarEnderecoCompleto();

            return Json(endereco);
        }
        public IActionResult AlterarEnderecoCompleto([FromBody] System.Text.Json.JsonElement dados)
        {
            /*string rua, string bairro, string tipo, string cep, 
             * string descricao, int codigoCidade, int codigoCliente, 
             * int codigoColaborador, int codigoFornecedor*/
            Endereco endereco = new Endereco(
                Convert.ToInt32(dados.GetProperty("codigo").ToString()),
                dados.GetProperty("rua").ToString(),
                dados.GetProperty("bairro").ToString(),
                dados.GetProperty("tipo").ToString(),
                dados.GetProperty("cep").ToString(),
                dados.GetProperty("descricao").ToString(),
                Convert.ToInt32(dados.GetProperty("codigoCidade").ToString()),
                Convert.ToInt32(dados.GetProperty("codigoCliente").ToString()),
                Convert.ToInt32(dados.GetProperty("codigoColaborador").ToString()),
                Convert.ToInt32(dados.GetProperty("codigoFornecedor").ToString())
                );

            //endereco.GravarEnderecoCompleto();

            return Json(endereco);
        }
        public IActionResult ListarTodosEnderecosCompletos(){

            Endereco endereco = new Endereco();

            List<Endereco> listaEndereco = endereco.ListarTodosEnderecos();

            var response = new {
                header = new {
                    status = true,
                    mensagem = "Retornou"                    
                },
                dados = listaEndereco
            };

            return Json(response);
        }
    }
}
