using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HipercorWeb.Models;
using System.Text.Json;

namespace HipercorWeb.Controllers
{
    public class ClienteController : Controller
    {
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
    }
}