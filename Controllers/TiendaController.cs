using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HipercorWeb.Controllers
{
    public class TiendaController : Controller
    {
        #region propiedades de clase
        #endregion
        #region metodos de clase
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
        #endregion
    }
}