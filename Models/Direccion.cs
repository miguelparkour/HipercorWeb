using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Models
{
    public class Direccion
    {
        public Provincia Provincia { get; set; }
        public Municipio Municipio { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        public string CodigoPostal{ get; set; }
        /*public Direccion(string calle,string cp,Provincia prov, Municipio mun)
        {
            this.Provincia = prov;
            this.Municipio = mun;
            this.Calle = calle;
            this.CodigoPostal = cp;
        }*/
    }
}
