using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLTipoServicio
    {
        public BETipoServicio ObtenerTipoServicio(string IdTipoServicio)
        {
            return new DATipoServicio().ObtenerTipoServicio(IdTipoServicio);
        }

        public List<BETipoServicio> Listar(string Id, string Nombre)
        {
            return new DATipoServicio().Listar(Id, Nombre);
        }

        public bool MantenerTipoServicio(int Opcion, BETipoServicio oTipoServicio)
        {
            try
            {
                return Convert.ToBoolean(new DATipoServicio().MantenerTipoServicio(Opcion, oTipoServicio));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BETipoDocumento> ObtenerTipoDocsServicio(string IdTipoServicio)
        {
            return new DATipoServicio().ObtenerTipoDocsServicio(IdTipoServicio);
        }

    }
}
