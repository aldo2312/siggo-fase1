using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Siggo.SIGC.Entity
{
    public class BEEmpresa : BEMantenimientoBase
    {
        [Display(Name = "Identificador")]
        public string IdEmpresa { get; set; }
        [Display(Name = "Razón Social"), Required(ErrorMessage = "Ingrese el Nombre de la Empresa")]
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string PrefijoCorreo { get; set; }
        [Display(Name = "Telefono"), DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        public string Contacto { get; set; }
        [Display(Name = "Tipo Empresa"), Required(ErrorMessage = "Seleccione el Tipo de Empresa")]
        public string TipoEmpresa { get; set; }
        [Display(Name = "RUC"), Required(ErrorMessage = "Ingrese el RUC de la Empresa")]
        public string RucEmpresa { get; set; }
        [Display(Name = "Cant. de Días para Notificar Vencimiento")]
        public int NotificacionDiasVencimiento { get; set; }
        [Display(Name = "Empresa con Actividad Normal Específica")]
        public bool ActividadNormalEspecifica { get; set; }
        public string TipoEmpresaSiggo { get; set; }
        [Display(Name = "Seleccione Empresa con la que trabajará")]
        public string IdCliente { get; set; }

        public string EmpresaAsociada { get; set; }
    }
}
