using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.Entity;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Administracion.Models
{
    public class UsuarioWebModel: BEUsuarioWeb
    {        
        public List<BEUsuarioWeb> lRegistrosUsuarios { get; set; }
        public bool NuevoRegistro { get; set; }

        public string IdEmpresaSel { get; set; }
        public string NombreEmpresaSel { get; set; }
        public string TipoEmpresaSiggo { get; set; }
        public IEnumerable<ComunModel> lEmpresas { get; set; }

        public IEnumerable<ComunModel> lRoles { get; set; }
        public IEnumerable<ComunModel> lRecibeNotificaciones { get; set; }
        
    }   
}