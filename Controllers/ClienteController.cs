using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HipercorWeb.Models;
using System.Text.Json;
using HipercorWeb.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
                return View(cliente);
            }

        }
        private bool nullUser()
        {

            //Hay que sustituir todos estos if/else con un filtro que compruebe el usuario en las cookies o con un jwt
            string sUser = HttpContext.Session.GetString("User");

            if (sUser == null) return true;
            else return false;
        }

        [HttpGet]
        public IActionResult DatosPersonales()
        {
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
                return View(cliente);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DatosPersonales(Cliente client)
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else if (ModelState.GetValidationState("DatosPersonales.Nombre") == ModelValidationState.Valid &&
                ModelState.GetValidationState("DatosPersonales.Apellidos") == ModelValidationState.Valid &&
                ModelState.GetValidationState("DatosPersonales.Movil") == ModelValidationState.Valid &&
                ModelState.GetValidationState("DatosPersonales.Fijo") == ModelValidationState.Valid)
            {
                cliente.DatosPersonales = client.DatosPersonales;
                cliente = await _dbAccess.EditarPersonal(cliente);
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
                return RedirectToAction("UserPanel", "Cliente");
            }
            else
            {
                return View(cliente);
            }
        }
        [HttpGet]
        public IActionResult Direcciones()
        {
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
                return View(cliente.Direcciones);
            }
        }

        [HttpGet]
        public async Task<IActionResult> BorrarDireccion(string municipio, string calle)
        {
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
                if (cliente.Direcciones.Count > 1) cliente = await _dbAccess.BorrarDireccion(municipio, calle, cliente);
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
                return RedirectToAction("Direcciones");
            }
        }

        [HttpGet]
        public IActionResult AddDireccion()
        {
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddDireccion(Direccion dir)
        {
            if (nullUser())
            {
                return RedirectToAction("Login", "Tienda");
            }
            else if (ModelState.IsValid)
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
                cliente = await _dbAccess.AddDireccion(cliente,dir);
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
                return RedirectToAction("UserPanel");
            }
            else
            {
                return View();
            }
        }

        public async Task<string> EmailConfirm(string cEmail)
        {
            cEmail=cEmail.Replace(" ", "+");
            string email = StringCipher.DecryptString(cEmail);
            /*
             * Creo que el protocolo http eventualmente cambia el cEmail porque a veces 
             * salta la siguiente execpión al desencriptar el email
             * FormatException: 
             * The input is not a valid Base-64 string as it contains a non-base 64 character, 
             * more than two padding characters, or an illegal character among the padding characters.
             * 
             * He reparado en que remplaza el '+' por un espacio
             * puede que haya más casos, hasta que sepa cómo solucionarlo solo queda hacer más 
             * pruebas e ir corrigiendo los casos
             */

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