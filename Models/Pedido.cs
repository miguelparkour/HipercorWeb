using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HipercorWeb.Models
{
    public class Pedido
    {
        public string Id { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public DateTime Fecha { get; set; }
        private decimal PrecioTotal;
        public decimal GetPrecioTotal()
        {
            PrecioTotal = 0;
            foreach (Producto item in ListaProductos)
            {
                PrecioTotal += item.Precio * item.Cantidad;
            }
            return PrecioTotal;
        }
        public void GenerateId(string email)
        {
            this.Id = StringCipher.EncryptOneWay(email + this.Fecha.ToFileTime());
        }
        


    }
}
