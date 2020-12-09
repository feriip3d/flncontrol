using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FLNControl.Controllers.Orcamento
{
    public class OrcamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GravarOrcamento()
        {
            return View("GravarOrcamento");
        }
    }
}
