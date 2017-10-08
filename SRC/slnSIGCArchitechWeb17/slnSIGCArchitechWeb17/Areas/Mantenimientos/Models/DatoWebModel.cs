using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;
using System.ComponentModel.DataAnnotations;

namespace slnSIGCArchitechWeb17.Areas.Mantenimientos.Models
{
    public class DatoWebModel : BEDato
    {
        public List<BEDato> lRegistrosDatos { get; set; }
        public bool NuevoRegistro { get; set; }

        public IEnumerable<ComunModel> lCategoria { get; set; }
    }
}