using System;
using System.Threading.Tasks;
using HipercorWeb.Interfaces;
using HipercorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HipercorWeb.Controllers
{
    public class TiendaController : Controller
    {
        #region propiedades de clase
        private IDataBaseAccess _dbAccess;
        private ISendEmail _sendEmail;
        #endregion


        #region metodos de clase


        #region Constructores
        public TiendaController(IDataBaseAccess dbAccess, ISendEmail sendEmail)
        {
            this._dbAccess = dbAccess;
            this._sendEmail = sendEmail;
        }
        #endregion


        #region Login/registro
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
                string email = cliente.Email;
                cliente = await _dbAccess.login(cliente);
                if (cliente == null)
                {
                    if (await _dbAccess.CheckEmail(email))
                    {
                        // El Correo está registrado, pero el password es incorrecto
                        ViewBag.EmailError = false;
                        return View();
                    }
                    else
                    {
                        // El Correo no está registrado
                        ViewBag.EmailError = true;
                        return View();
                    }
                }
                else if (await _dbAccess.CheckConfirmEmail(email))
                {
                    ViewBag.CuentaNoActiva = true;
                    return View();
                }
                else
                {
                    cliente = await _dbAccess.CargarDirecciones(cliente);
                    cliente = await _dbAccess.CargarPedidos(cliente);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(cliente));
                    HttpContext.Session.SetString("name", cliente.DatosPersonales.Nombre);
                    return RedirectToAction("UserPanel", "Cliente");
                }
            }
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Registro(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (!await _dbAccess.CheckEmail(cliente.Email) && await _dbAccess.signup(cliente))
                {
                    string EncryptEmail = StringCipher.EncryptString(cliente.Email);
                    string body = "<h1><a href='https://localhost:44315/Tienda/EmailConfirm?cEmail=" + EncryptEmail + "'>Confirmar cuenta</a></h1>";
                    _sendEmail.Send(cliente.Email, "Bienvenido a Hipercor, " + cliente.DatosPersonales.Nombre, body);
                    return View("ConfirmarEmail");
                }
                else
                { // si hay un error en el signup también entraría x aquí, hay que solucionarlo
                    ViewData["error"] = "Ese correo electronico ya esta registrado";
                    return View();
                }
            }
            return View();
        }
        #endregion


        #region Olvidar password
        [HttpGet]
        public IActionResult RecuperarPassword()
        {
            ViewBag.Mensaje = "";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RecuperarPassword(string email)
        {
            if (await _dbAccess.RecuperarCuenta(email))
            {
                string cEmail = StringCipher.EncryptString(email);
                string body = @"Lamentamos que haya olvidado su contraseña de Hipercor Web, para crear una nueva clique <a href='https://localhost:44315/Tienda/EditarPassword?cEmail=" + cEmail + "'>aquí<a/>";
                _sendEmail.Send(email, "Recuperación de contraseña", body);
                ViewBag.Mensaje = "Ya hemos enviado el email de recuperación, si en unos minutos no ha llegado a su cuenta vuelva a intentarlo mas tarde";
            }
            else
            {
                ViewBag.Mensaje = "La cuenta " + email + " no está registrada en nuestra base de datos, por favor asegurese de escribirla bien o registrese";
            }

            return View();
        }
        [HttpGet]
        public IActionResult EditarPassword(string cEmail)
        {
            string email = StringCipher.DecryptString(cEmail);
            HttpContext.Session.SetString("email", email);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditarPassword(Cliente cliente)
        {
            if (ModelState.GetValidationState("Password") == ModelValidationState.Valid &&
                ModelState.GetValidationState("ConfirmPassword") == ModelValidationState.Valid)
            {
                ViewBag.mensaje = "";
                string email = HttpContext.Session.GetString("email");
                if (await _dbAccess.EditarPassword(email, cliente.Password))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    //Hay que cambiar esto para enviar un mensaje de error desconocido
                    return RedirectToAction("Registro");
                }
            }
            else
            {
                return View();
            }
        }
        #endregion


        #region Tienda

        public async Task<IActionResult> Productos()
        {
            List<Producto> productos = await _dbAccess.CargarProductos();
            return View(productos);
        }

        public async Task<IActionResult> Detalle(string id)
        {
            Producto producto = await _dbAccess.CargarProductos(id);
            return View(producto);
        }


        #endregion


        #region Otros
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> EmailConfirm(string cEmail)
        {
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
             * 
             * cEmail = cEmail.Replace(" ", "+");
             * 
             */

            bool result = await _dbAccess.ConfirmEmail(email);
            if (result)
            {
                return View("EmailConfirmado");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        #endregion


        #endregion
    }
}