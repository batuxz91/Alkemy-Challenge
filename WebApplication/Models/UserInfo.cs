using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class UserInfo
    {
        [Display (Name = "Correo Electronico")]
        [Required(ErrorMessage = "El campo es requerido.")]
        [EmailAddress(ErrorMessage = "El email no es valido.")]
        public string Email { get; set; }

        [Display(Name ="Contraseña")]
        [Required(ErrorMessage = "La Contraseña es requerida.")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password,ErrorMessage = "La contraseña no es valida.")]
        public string Password { get; set; }
    }
}
