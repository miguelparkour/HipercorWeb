using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Models
{
    public class Municipio
    {
        [Required]
        public string id { get; set; }
        public string nm { get; set; }
    }
}
