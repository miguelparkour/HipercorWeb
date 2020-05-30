using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Models
{
    public class Provincia
    {
        [Required(ErrorMessage = "id requerido")]
        public string id { get; set; }
        public string nm { get; set; }
    }
}
