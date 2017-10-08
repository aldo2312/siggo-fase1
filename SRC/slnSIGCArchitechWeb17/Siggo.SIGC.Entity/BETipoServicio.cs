using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BETipoServicio : BEMantenimientoBase
    {
        [Display(Name = "Identificador")]
        public string IdTipoServicio { get; set; }
        [Display(Name = "Descripción"), Required(ErrorMessage = "Ingrese la Descripción")]
        public string Descripcion { get; set; }    
    }
}
