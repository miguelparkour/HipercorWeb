using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Models
{
    public class Direccion
    {
        [Required(ErrorMessage = "provincia requerido")]
        public Provincia Provincia { get; set; }
        [Required(ErrorMessage = "municipio requerido")]
        public Municipio Municipio { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Formato de Codigo postal incorrecto")]
        public string CodigoPostal{ get; set; }

    }
}
