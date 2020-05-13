using System;
using System.Threading.Tasks;
using HipercorWeb.Interfaces;
using HipercorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Text.Json;

namespace HipercorWeb.Controllers
{
    public class TiendaController : Controller
    {
        #region propiedades de clase
        private IDataBaseAccess _dbAccess;
        private ISendEmail _sendEmail;
        #endregion
        #region metodos de clase
        public TiendaController(IDataBaseAccess dbAccess,ISendEmail sendEmail)
        {
            this._dbAccess = dbAccess;
            this._sendEmail = sendEmail;
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
                string EncryptEmail = StringCipher.EncryptString(cliente.Email);
                string body = "<h1><a href='https://localhost:44315/Cliente/EmailConfirm?cEmail="+EncryptEmail+"'>Confirmar cuenta</a></h1>";
                _sendEmail.Send(cliente.Email, "Bienvenido a Hipercor, " + cliente.DatosPersonales.Nombre, body);
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
                    cliente = await _dbAccess.CargarDirecciones(cliente);
                    HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
                    HttpContext.Session.SetString("name", cliente.DatosPersonales.Nombre);
                    return RedirectToAction("UserPanel", "Cliente");
                }
            }
            return View();
        }

        public string Test( string a)
        {

            string EncryptEmail = StringCipher.EncryptString("lzr85044@eoopy.com");
            string email = StringCipher.DecryptString(EncryptEmail);
            return EncryptEmail;
        }

        #endregion

        //RDJJxX2ZOzOpD2rGT8LG3+09JdgCevughxBghqcFOh8=
        //RDJJxX2ZOzOpD2rGT8LG3+09JdgCevughxBghqcFOh8=
        //RDJJxX2ZOzOpD2rGT8LG3 09JdgCevughxBghqcFOh8=
    }
}