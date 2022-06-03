using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Controllers
{
    public class InformacoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
