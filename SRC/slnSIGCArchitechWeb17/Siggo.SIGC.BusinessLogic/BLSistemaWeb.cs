using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLSistemaWeb
    {
        public List<BEMenuSistema> ObtenerOpcionesMenu(string idUsuarioWeb)
        {
            return new DASistemaWeb().ObtenerOpcionesMenu(idUsuarioWeb);
        }

        public List<BERoles> ObtenerRolesUsuario(string strIdEmpresa)
        {
            return new DASistemaWeb().ObtenerRolesUsuario(strIdEmpresa);
        }
    }
}
