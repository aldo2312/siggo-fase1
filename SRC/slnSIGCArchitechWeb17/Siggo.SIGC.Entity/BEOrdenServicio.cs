using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.Entity
{
    public class BEOrdenServicio : BEMantenimientoBase
    {
        public string IdEmpresa { get; set; }
        public string NroOrden { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaRealInicio { get; set; }
        public DateTime? FechaRealFin { get; set; }
        public DateTime? FechaControlInicio { get; set; }
        public DateTime? FechaControlFin { get; set; }

        public DateTime? FechaProceso { get; set; }
        public string HoraProceso { get; set; }
        public string ProcesadoPor { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string HoraCierre { get; set; }
        public string CerradoPor { get; set; }

        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoContratista { get; set; }
        public string NombreContratista { get; set; }
        public string NombreSubContratista { get; set; }
    }

    public class BEOrdenServicioPeriodos
    {
        public string Cliente { get; set; }
        public string NroOrden { get; set; }
        public string Contratista { get; set; }
        public string Mes { get; set; }
        public string NombreMes { get; set; }
        public string Anho { get; set; }
        public string Estado { get; set; }
        public string EsContratista { get; set; }
        public string ValorEmpleado { get; set; }
        public string ValorVehiculo { get; set; }
        public string Publicado { get; set; }
        public string Situacion { get; set; }
    }

    public class BEOrdenServicioAreas
    {
        public string IdEmpresa { get; set; }
        public string NroOrden { get; set; }
        public string CodigoDpto { get; set; }
        public string DescripcionDpto { get; set; }
    }

    public class BEOrdenServicioLocalizaciones
    {
        public string IdEmpresa { get; set; }
        public string NroOrden { get; set; }
        public string CodigoSede { get; set; }
        public string DescripcionSede { get; set; }
    }

    public class BEOrdenServicioContratas
    {
        public string IdEmpresa { get; set; }
        public string NroOrden { get; set; }
        public string IdContrata { get; set; }
        public string IdSubContrata { get; set; }

        public string DescContrata { get; set; }
        public string DescSubContrata { get; set; }
    }

}
