using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.DAL.ColaboradorDAL;
using engenharia.Models.Colaborador;
using Microsoft.AspNetCore.Mvc;

namespace engenharia.Controllers.Acesso
{
    public class AcessoController : Controller
    {
        public IActionResult Index()
        {
            ColaboradorDAL model = new ColaboradorDAL();

            List<Colaborador> lista = model.Listar();
                return View(lista);


        }
    }
}
