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
        [Required(ErrorMessage = "Email requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z]+.[a-zA-Z]{2,4}$", ErrorMessage = "Formato de email erroneo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña  es requerida")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?!(.*[\W]){1,})(?!.*\s).{4,}$", ErrorMessage = "La contraseña debe contener una letra mayúscula, una minúscula, un número")]
        public string Password { get; set; }


        [Compare("Password",ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
        public DatosPersonales DatosPersonales { get; set; }

        public List<Direccion> Direcciones { get; set; }

        public Pedido[] Pedidos { get; set; }


        #endregion
        #region metodos de clase
        public Cliente() { }
        public Cliente(string mail, string pass)
        {
            this.Email = mail;
            this.Password = pass;
        }
        #endregion
    }
}
