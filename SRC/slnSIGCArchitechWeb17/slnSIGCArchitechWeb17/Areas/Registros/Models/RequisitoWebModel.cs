using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Registros.Models
{
    public class RequisitoWebModel : BERequisito
    {
        public List<BERequisito> lRegistrosRequisitos { get; set; }
        public List<BERequisitoDato> lRegistrosDatos { get; set; }
        public bool NuevoRegistro { get; set; }
        public IEnumerable<ComunModel> lTipoRecurso { get; set; }
        public IEnumerable<ComunModel> lPeriodicidad { get; set; }
        public IEnumerable<ComunModel> lTipoServicio { get; set; }        

        public IEnumerable<ComunModel> lAcciones { get; set; }

        public string IdEmpresaSel { get; set; }
        public string NombreEmpresaSel { get; set; }
        public IEnumerable<ComunModel> lEmpresas { get; set; }

        public string FecVigenciaDesde { get; set; }
        public string FecVigenciaHasta { get; set; }
    }
}