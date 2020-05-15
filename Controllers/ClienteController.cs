using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HipercorWeb.Models;
using HipercorWeb.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using HipercorWeb.Filters;
using System.Text.Json;

namespace HipercorWeb.Controllers
{
    [ServiceFilter(typeof(CheckUserFilter))]
    public class ClienteController : Controller
    {
        #region Propiedades
        private IDataBaseAccess _dbAccess;
        #endregion

        #region Métodos

        #region contructor
        public ClienteController(IDataBaseAccess dbAccess)
        {
            this._dbAccess = dbAccess;
        }
        #endregion


        #region Datos Personales
        [HttpGet]
        public IActionResult DatosPersonales()
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> DatosPersonales(Cliente client)
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            
            if (ModelState.GetValidationState("DatosPersonales.Nombre") == ModelValidationState.Valid &&
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
        #endregion


        #region direcciones
        [HttpGet]
        public IActionResult Direcciones()
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente.Direcciones);
        }
        [HttpGet]
        public async Task<IActionResult> BorrarDireccion(int id)
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            if (cliente.Direcciones.Count > 1) cliente = await _dbAccess.BorrarDireccion(cliente,id);
            HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
            return RedirectToAction("Direcciones");
        }
        [HttpGet]
        public IActionResult EditarDireccion(int id)
        {
            HttpContext.Session.SetString("indiceABorrar", id.ToString());
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente.Direcciones[id]);
        }
        [HttpPost]
        public async Task<IActionResult> EditarDireccion(Direccion dir)
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            int indice = int.Parse(HttpContext.Session.GetString("indiceABorrar"));
            cliente = await _dbAccess.EditarDireccion(cliente,dir, indice);
            HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
            return RedirectToAction("Direcciones");
        }
        [HttpGet]
        public IActionResult AddDireccion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDireccion(Direccion dir)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
                cliente = await _dbAccess.AddDireccion(cliente, dir);
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(cliente));
                return RedirectToAction("Direcciones");
            }
            else
            {
                return View();
            }
        }

        #endregion


        #region Otros
        public IActionResult UserPanel()
        {
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente);

        }
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Tienda");
        }

        #endregion


        #region Métodos privados

        #endregion

        #endregion
    }
}