using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BERequisito : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public string IdTipoServicio { get; set; }
        public string IdTipoDocumento { get; set; }
        public string TipoRecurso { get; set; }
        public string DescripcionRequisito { get; set; }
        public int DiasTolerancia { get; set; }
        public int DiasNotificacion { get; set; }
        public string Periodicidad { get; set; }
        public bool HabilitaPago { get; set; }
        public bool HabilitaAcceso { get; set; }
        public bool DardeBaja { get; set; }
        public DateTime? FechaVigenciaDesde { get; set; }
        public DateTime? FechaVigenciaHasta { get; set; }

        public string DescripcionTipoRecurso { get; set; }
        public string TipoRiesgo { get; set; }
        public string NombreEmpresa { get; set; }
    }

    public class BERequisitoDato : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public string IdTipoServicio { get; set; }
        public string IdTipoDocumento { get; set; }
        public string IdDato { get; set; }
        public string DescripcionDato { get; set; }
        public int Orden { get; set; }
    }
}
