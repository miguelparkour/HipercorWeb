using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HipercorWeb.Models
{
    public class DatosPersonales
    {
        [Required(ErrorMessage = "Hay que introducir un nombre valido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Hay que introducir un apellido valido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Hay que introducir un movil valido")]
        public string Movil { get; set; }
        public string Fijo { get; set; }
    }
}
