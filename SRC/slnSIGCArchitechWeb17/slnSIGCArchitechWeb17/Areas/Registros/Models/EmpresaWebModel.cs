using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;
using System.ComponentModel.DataAnnotations;

namespace slnSIGCArchitechWeb17.Areas.Registros.Models
{
    public class EmpresaWebModel : BEEmpresa
    {
        public List<BEEmpresa> lRegistrosEmpresas { get; set; }
        public bool NuevoRegistro { get; set; }
        public IEnumerable<ComunModel> lTipoEmpresa { get; set; }
        public IEnumerable<ComunModel> lEsActividadNormal { get; set; }
        public IEnumerable<ComunModel> lEmpresaOrigen { get; set; }
    }
}