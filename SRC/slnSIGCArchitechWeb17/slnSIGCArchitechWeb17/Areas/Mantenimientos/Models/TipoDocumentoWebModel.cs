using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;
using System.ComponentModel.DataAnnotations;

namespace slnSIGCArchitechWeb17.Areas.Mantenimientos.Models
{
    public class TipoDocumentoWebModel : BETipoDocumento
    {
        public List<BETipoDocumento> lRegistrosTipoDocumento { get; set; }
        public bool NuevoRegistro { get; set; }

        public IEnumerable<ComunModel> lNivelRiesgo { get; set; }
        public IEnumerable<ComunModel> lTipoRiesgo { get; set; }
        public IEnumerable<ComunModel> lTipoServicio { get; set; }
    }
}