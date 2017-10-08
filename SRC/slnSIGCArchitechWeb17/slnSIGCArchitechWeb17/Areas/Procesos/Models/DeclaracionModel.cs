using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Procesos.Models
{
    public class DeclaracionModel : vReg_Declaracion
    {
        public List<vReg_Declaracion> lDeclaraciones { get; set; }
        public string IdEmpresaSel { get; set; }
        public string NombreEmpresaSel { get; set; }
        public IEnumerable<ComunModel> lEmpresas { get; set; }

        //Para la edicion de un registro
        public bool NuevoRegistro { get; set; }
        public IEnumerable<ComunModel> lTipoRecurso { get; set; }
        public IEnumerable<ComunModel> lTipoDoi { get; set; }
        public IEnumerable<ComunModel> lContratas { get; set; }
        public IEnumerable<ComunModel> lClientes { get; set; }
        public IEnumerable<ComunModel> lOrdenesPorClientes { get; set; }

        public IEnumerable<ComunModel> lMeses { get; set; }
        public IEnumerable<ComunModel> lAnhos { get; set; }
    }

    public class DeclaracionDetalleModel
    {
        public string IdEmpresaSel { get; set; }
        public string IdRegistroSel { get; set; }
        public string NroSecuenciaSel { get; set; }
        public string NivelRiesgo { get; set; }
        public List<vReg_Documento> lDocumentos { get; set; }

        //Detalle Registro
        public string FechaPresentacion { get; set; }
        public string Observacion { get; set; }
        public string ImagenSel { get; set; }
        public List<vDatosDetalle> lDatosAsociados { get; set; }
    }

    public class vDatosDetalle
    {
        public string Codigo { get; set; }
        public string DescripcionDato { get; set; }
        public string Categoria { get; set; }
        public string ValorActual { get; set; }
        public string Situacion { get; set; }
        public string AccionRealizada { get; set; }
    }
}