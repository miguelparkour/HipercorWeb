using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HipercorWeb.Interfaces;
using HipercorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                return RedirectToAction("Login", "Tienda");
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
            if (ModelState.GetValidationState("Email") == ModelValidationState.Valid &&
                ModelState.GetValidationState("Password") == ModelValidationState.Valid)
            {
                cliente = await _dbAccess.login(cliente);
                if (cliente == null)
                {
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
                    return RedirectToAction("UserPanel", "Cliente");
                }
            }
            return View();
        }


        #endregion
    }
}