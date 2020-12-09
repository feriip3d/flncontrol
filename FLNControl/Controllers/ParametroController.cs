using engenharia.DAL.ParametroDAL;
using FLNControl.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FLNControl.Controllers
{
    public class ParametroController : Controller
    {
        public IActionResult Index()
        {
            ParametroDAL dal = new ParametroDAL();
            Parametro param = dal.find();
            if (param == null)
            {
                param = new Parametro();
                param.Id = 0;
            }

            ViewBag.Parametros = param;
            return View();
        }

        public IActionResult Atualizar([FromBody] JsonElement data)
        {
            ParametroDAL dal = new ParametroDAL();
            Parametro param = dal.find();
            if (param == null)
            {
                param = new Parametro();
                param.Id = 0;
            }

            param.InscricaoEstadual = data.GetProperty("ie").ToString();
            param.RazaoSocial = data.GetProperty("razaosc").ToString();
            param.NomeFantasia = data.GetProperty("nomef").ToString();
            param.Cnpj = data.GetProperty("cnpj").ToString();
            param.Telefone = data.GetProperty("telefone").ToString();
            param.Email = data.GetProperty("email").ToString();
            param.Site = data.GetProperty("site").ToString();
            param.UrlLogoGrande = data.GetProperty("logoGrande").ToString();
            param.UrlLogoPequeno = data.GetProperty("logoPequeno").ToString();
            param.Endereco = data.GetProperty("endereco").ToString();
            param.Cidade = data.GetProperty("cidade").ToString();
            param.Uf = data.GetProperty("uf").ToString();

            dal.save(param);

            return Json(new
            {
                success = true
            });
        }
    }
}
