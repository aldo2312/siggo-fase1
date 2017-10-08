using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BEDato : BEMantenimientoBase
    {
        [Display(Name = "Identificador")]
        public string IdDato { get; set; }
        [Display(Name = "Descripción"), Required(ErrorMessage = "Ingrese la Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Descripcion Corta"), Required(ErrorMessage = "Ingrese la Descripción Corta")]
        public string Alias { get; set; }
        [Display(Name = "Categoria"), Required(ErrorMessage = "Ingrese la Categoria")]
        public string Categoria { get; set; }
    }
}
