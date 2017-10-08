using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace slnSIGCArchitechWeb17.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserNameLogOn { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordLogOn { get; set; }

        [Display(Name = "Recordar mi cuenta")]
        public bool RememberMe { get; set; }
    }    

    public class RecuperarContraseñaModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        [Display(Name = "ID. de Usuario")]
        public string UserNameLogOn { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electrónico")]
        public string EmailLogOn { get; set; }
    }

    public class TopMenuWebModel
    {
        public int IdMenu { get; set; }
        public int PreMenu { get; set; }
        public bool Grupo { get; set; }
        public string DescripcionMenu { get; set; }
        public string ActionResult { get; set; }
        public string Controlador { get; set; }
        public IEnumerable<TopMenuWebModel> lOpcionesMenu { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "La nueva contraseña y la Confirmacion no son iguales.")]
        public string ConfirmPassword { get; set; }
    }

}
