using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
   public class BLMaestro
    {
        public List<BEMaestro> ObtenerEstadosRegistro()
        {
            return new DAMaestro().ObtenerEstadosRegistro();
        }
    }
}
