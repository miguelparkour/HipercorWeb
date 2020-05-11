using System;
using System.Threading.Tasks;
using HipercorWeb.Interfaces;
using HipercorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                    HttpContext.Session.SetString("User", System.Text.Json.JsonSerializer.Serialize(cliente));
                    return RedirectToAction("UserPanel", "Cliente");
                }
            }
            return View();
        }

        public string Test( string a)
        {
            Cliente cliente = new Cliente();
            Direccion dir = new Direccion();
            dir.Calle = "Hola";
            cliente.Direcciones = new List<Direccion>();
            cliente.Direcciones.Add(dir);
            return "retorno: " + cliente.Direcciones[0].Calle;
        }

        #endregion
    }
}