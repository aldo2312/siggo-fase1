using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BETipoDocumento : BEMantenimientoBase
    {
        [Display(Name = "Identificador")]
        public string IdTipoDocumento { get; set; }
        [Display(Name = "Desc. Corta"), Required(ErrorMessage = "Ingrese la descripción del documento")]
        public string Descripcion { get; set; }
        [Display(Name = "Desc. Larga")]
        public string DescripcionLarga { get; set; }
        [Display(Name = "Servicio"), Required(ErrorMessage = "Seleccione un Tipo de Servicio")]
        public string IdTipoServicio { get; set; }
        [Display(Name = "Tipo Riesgo"), Required(ErrorMessage = "Seleccione un Tipo de Riesgo")]
        public int IdTipoRiesgo { get; set; }
        [Display(Name = "Nivel Riesgo"), Required(ErrorMessage = "Seleccione un Nivel de Riesgo")]
        public string IdNivelRiesgo { get; set; }

        public string NivelRiesgo { get; set; }
        public string TipoRiesgo { get; set; }
        public string TipoServicio { get; set; }
    }
}
