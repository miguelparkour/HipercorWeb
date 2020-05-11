using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HipercorWeb.Models;
using System.Text.Json;
using HipercorWeb.Interfaces;

namespace HipercorWeb.Controllers
{
    public class ClienteController : Controller
    {
        private IDataBaseAccess _dbAccess;
        public ClienteController(IDataBaseAccess dbAccess)
        {
            this._dbAccess = dbAccess;
        }
        public IActionResult UserPanel()
        {
            string sUser = HttpContext.Session.GetString("User");

            if (sUser == null)
            {
                return RedirectToAction("Login","Tienda");
            }
            else
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(sUser);
                return View(cliente);
            }

        }

        public async Task<string> EmailConfirm(string cEmail)
        {
            string email = StringCipher.DecryptString(cEmail);
            bool result = await _dbAccess.ConfirmEmail(email);
            if (result)
            {
                return "Email Confimado correctamente";
            }
            else
            {
                return "Error al confirmar el Email, porfavor vuelva a intentarlo mas tarde";
            }
        }
    }
}