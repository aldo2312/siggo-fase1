using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Registros.Models
{
    public class RecursoWebModel : BERecurso
    {
        public List<BERecurso> lRegistrosRecursos { get; set; }
        public bool NuevoRegistro { get; set; }

        public string IdEmpresaSel { get; set; }
        public string NombreEmpresaSel { get; set; }
        public string TipoRecursoSel { get; set; }

        public IEnumerable<ComunModel> lTipoRecurso { get; set; }
    }

    public class RecursoDetallesWebModel
    {
        public string IdEmpresaSel { get; set; }
        public string IdRecursoSel { get; set; }
        public List<BERequisitoRecurso> lRecursosRequisitos { get; set; }
    }
}