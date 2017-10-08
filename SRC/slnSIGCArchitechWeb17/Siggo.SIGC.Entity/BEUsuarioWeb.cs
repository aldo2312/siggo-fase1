using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Siggo.SIGC.Entity
{
    public class BEUsuarioWeb : BEMantenimientoBase
    {
        //[Display(Name = "Empresa"), Required(ErrorMessage = "Ingrese la empresa asociada al Usuario")]
        public string IdEmpresa { get; set; }
        [Display(Name = "Identificador"), Required(ErrorMessage = "Campo Obligatorio")]
        public string IdUsuario { get; set; }
        [Display(Name = "Nombres"), Required(ErrorMessage = "Campo Obligatorio")]
        public string NombreUsuario { get; set; }
        [Display(Name = "Apellido Paterno"), Required(ErrorMessage = "Campo Obligatorio")]
        public string ApellidoPaternoUsuario { get; set; }
        [Display(Name = "Apellido Materno"), Required(ErrorMessage = "Campo Obligatorio")]
        public string ApellidoMaternoUsuario { get; set; }
        [Display(Name = "Correo"), Required(ErrorMessage = "Campo Obligatorio")]
        public string CorreoElectronico { get; set; }
        [Display(Name = "Contraseña"), Required(ErrorMessage = "Campo Obligatorio")]
        public string Contrasenha { get; set; }
        [Display(Name = "Rol"), Required(ErrorMessage = "Campo Obligatorio")]
        public int IdRol { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public bool? EsUsuarioInterno { get; set; }
        [Display(Name = "Recibir Avisos de Vencimiento")]
        public string RecibeNotificaciones { get; set; }

        public string RolDescripcion { get; set; }
        public string EmpresaAsociada { get; set; }
        public string TipoEmpresaSiggo { get; set; }
        public string UsuarioSiggo { get; set; }
    }
}
