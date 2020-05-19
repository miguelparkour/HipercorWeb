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
using Newtonsoft.Json;

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
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> DatosPersonales(Cliente client)
        {
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            
            if (ModelState.GetValidationState("DatosPersonales.Nombre") == ModelValidationState.Valid &&
                ModelState.GetValidationState("DatosPersonales.Apellidos") == ModelValidationState.Valid &&
                ModelState.GetValidationState("DatosPersonales.Movil") == ModelValidationState.Valid &&
                ModelState.GetValidationState("DatosPersonales.Fijo") == ModelValidationState.Valid)
            {
                cliente.DatosPersonales = client.DatosPersonales;
                cliente = await _dbAccess.EditarPersonal(cliente);
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(cliente));
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
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente.Direcciones);
        }
        [HttpGet]
        public async Task<IActionResult> BorrarDireccion(int id)
        {
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            if (cliente.Direcciones.Count > 1) cliente = await _dbAccess.BorrarDireccion(cliente, id);
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(cliente));
            return RedirectToAction("Direcciones");
        }
        [HttpGet]
        public IActionResult EditarDireccion(int id)
        {
            HttpContext.Session.SetString("indiceABorrar", id.ToString());
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente.Direcciones[id]);
        }
        [HttpPost]
        public async Task<IActionResult> EditarDireccion(Direccion dir)
        {
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            int indice = int.Parse(HttpContext.Session.GetString("indiceABorrar"));
            cliente = await _dbAccess.EditarDireccion(cliente,dir, indice);
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(cliente));
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
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
                cliente = await _dbAccess.AddDireccion(cliente, dir);
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(cliente));
                return RedirectToAction("Direcciones");
            }
            else
            {
                return View();
            }
        }

        #endregion


        #region Productos

        public async Task<IActionResult> AddProducto(string id)
        {
            //Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            Producto producto = await _dbAccess.CargarProductos(id);
            string carrito = HttpContext.Session.GetString("carrito");
            List<Producto> ListaProductos;
            if (carrito != null)
            {
                ListaProductos = JsonConvert.DeserializeObject<List<Producto>>(carrito);
                if (!ListaProductos.Contains(producto))
                {
                    ListaProductos.Add(producto);
                }
                else
                {
                    ListaProductos[ListaProductos.IndexOf(producto)].Cantidad++;
                }
            }
            else
            {
                ListaProductos = new List<Producto>();
                ListaProductos.Add(producto);
            }
            HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(ListaProductos));

            return RedirectToAction("carrito");
        }
        public async Task<IActionResult> DisProducto(string id)
        {
            Producto producto = await _dbAccess.CargarProductos(id);
            string carrito = HttpContext.Session.GetString("carrito");
            List<Producto> ListaProductos;
            if (carrito != null)
            {
                ListaProductos = JsonConvert.DeserializeObject<List<Producto>>(carrito);
                if (!ListaProductos.Contains(producto))
                {
                    return RedirectToAction("carrito");
                }
                else
                {
                    ListaProductos[ListaProductos.IndexOf(producto)].Cantidad--;
                    int cant = ListaProductos[ListaProductos.IndexOf(producto)].Cantidad;
                    if (cant==0)
                    {
                        ListaProductos.RemoveAt(ListaProductos.IndexOf(producto));
                    }

                    HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(ListaProductos));

                    return RedirectToAction("carrito");
                }
            }
            else
            {
                return RedirectToAction("UserPanel");
            }
        }
        public async Task<IActionResult> DelProducto(string id)
        {
            Producto producto = await _dbAccess.CargarProductos(id);
            string carrito = HttpContext.Session.GetString("carrito");
            List<Producto> ListaProductos;
            if (carrito != null)
            {
                ListaProductos = JsonConvert.DeserializeObject<List<Producto>>(carrito);
                if (!ListaProductos.Contains(producto))
                {
                    return RedirectToAction("carrito");
                }
                else
                {
                    ListaProductos.RemoveAt(ListaProductos.IndexOf(producto));

                    HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(ListaProductos));

                    return RedirectToAction("carrito");
                }
            }
            else
            {
                return RedirectToAction("UserPanel");
            }
        }
        public IActionResult Carrito()
        {
            string carrito = HttpContext.Session.GetString("carrito");
            if (carrito == "[]") carrito = null;
            if (carrito !=null)
            {
                List<Producto> ListaProductos;
                ListaProductos = JsonConvert.DeserializeObject<List<Producto>>(carrito);
                Pedido miCarrito = new Pedido();
                miCarrito.ListaProductos = ListaProductos;
                return View(miCarrito);
            }
            else
            {
                HttpContext.Session.Remove("carrito");
                return RedirectToAction("Productos","Tienda");
            }
        }

        #endregion


        #region Pedidos

        public IActionResult EscogerDireccion()
        {
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            string carrito = HttpContext.Session.GetString("carrito");

            if (carrito != null)  return View(cliente.Direcciones);
            else return RedirectToAction("carrito");
        }
        public async Task<IActionResult> HacerPedido(int id)
        {
            List<Producto> ListaProductos;
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            string carrito = HttpContext.Session.GetString("carrito");
            if (carrito != null)
            {   
                ListaProductos = JsonConvert.DeserializeObject<List<Producto>>(carrito);
                Pedido pedido = new Pedido();
                pedido.Fecha = DateTime.Now;
                pedido.GenerateId(cliente.Email);
                pedido.ListaProductos = ListaProductos;
                if (await _dbAccess.HacerPedido(pedido, cliente, id))
                {
                    cliente = await _dbAccess.CargarPedidos(cliente);
                    HttpContext.Session.Remove("carrito");
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(cliente));
                    return RedirectToAction("UserPanel");
                }

            }
            return RedirectToAction("carrito");
        }
        public IActionResult Pedidos()
        {
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
            return View(cliente.Pedidos);
        }


        #endregion


        #region Otros
        public IActionResult UserPanel()
        {
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("User"));
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