using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BERecurso : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        [Display(Name = "Identificador")]
        public string IdRecurso { get; set; }
        [Required(ErrorMessage = "Dato Obligatorio")]
        public string NumeroReferencia { get; set; }
        public string Descripcion1 { get; set; }
        public string Descripcion2 { get; set; }
        public string Descripcion3 { get; set; }
        public string Descripcion4 { get; set; }
        public string Observacion { get; set; }
        public int Cantidad { get; set; }
        public string TipoRecurso { get; set; }
        public string DescripcionTipoRecurso { get; set; }

        public string NombreEmpresa { get; set; }
        
    }

    public class BERequisitoRecurso : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public string IdRecurso { get; set; }
        public string IdTipoServicio { get; set; }
        public string IdTipoDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public bool EsRequerido { get; set; }
        public bool NecesitaAdjunto { get; set; }
        public int Orden { get; set; }
    }
}
