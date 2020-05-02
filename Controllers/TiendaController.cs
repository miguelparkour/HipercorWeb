using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HipercorWeb.Interfaces;
using HipercorWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HipercorWeb.Controllers
{
    public class TiendaController : Controller
    {
        #region propiedades de clase
        private IDataBaseAccess _dbAccess;
        #endregion
        #region metodos de clase
        public TiendaController(IDataBaseAccess dbAccess)
        {
            this._dbAccess = dbAccess;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Cliente cliente)
        {
            if (ModelState.IsValid && await _dbAccess.signup(cliente))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Cliente cliente)
        {
            if (ModelState.IsValid && await _dbAccess.login(cliente))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion
    }
}