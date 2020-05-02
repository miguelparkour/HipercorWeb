using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HipercorWeb.Models
{
    public class Cliente
    {
        #region propiedades de clase
        [Required(ErrorMessage = "Esta propiedad es requerida")]
        [StringLength(50,ErrorMessage = "Nombre de usuario demasiado largo")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Esta propiedad es requerida")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{4,}$",ErrorMessage = "La contraseña debe contener una letra mayúscula, una minúscula, un número y un caracter especial")]
        public string Password { get; set; }
        #endregion
        #region metodos de clase
        #endregion
    }
}
