using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Procesos.Models
{
    public class OrdenModel : BEOrdenServicio
    {
        public List<BEOrdenServicio> lRegistrosOrdenes { get; set; }
        
        public IEnumerable<ComunModel> lEmpresas { get; set; }

        public IEnumerable<ComunModel> lAreas { get; set; }
        public IEnumerable<ComunModel> lLocalizaciones { get; set; }
        public IEnumerable<ComunModel> lContratistas { get; set; }
        public IEnumerable<ComunModel> lSubContratistas { get; set; }

        public List<BEOrdenServicioAreas> lRegistrosAreas { get; set; }
        public List<BEOrdenServicioLocalizaciones> lRegistrosSedes { get; set; }
        public List<BEOrdenServicioContratas> lRegistrosContratas { get; set; }

        public bool NuevoRegistro { get; set; }
        public string FecRealInicio { get; set; }
        public string FecRealFin { get; set; }
        public string FecControlInicio { get; set; }
        public string FecControlFin { get; set; }

        public string fDesde { get; set; }
        public string fHasta { get; set; }

        public string ClienteSel { get; set; }
        public string NroOrdenSel { get; set; }
        public string ContratistaSel { get; set; }
        public List<BEOrdenServicioPeriodos> lRegistrosPeriodos { get; set; }

        public string AccionRealizada { get; set; }
    }
}