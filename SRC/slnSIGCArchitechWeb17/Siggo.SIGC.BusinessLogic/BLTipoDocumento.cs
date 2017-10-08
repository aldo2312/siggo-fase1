using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLTipoDocumento
    {
        public BETipoDocumento ObtenerTipoDocumento(string IdTipoDocumento, string IdTipoServicio)
        {
            return new DATipoDocumento().ObtenerTipoDocumento(IdTipoDocumento, IdTipoServicio);
        }

        public List<BETipoDocumento> Listar(string Id, string Descripcion, string Servicio)
        {
            return new DATipoDocumento().Listar(Id, Descripcion, Servicio);
        }

        public bool MantenerTipoDocumento(int Opcion, BETipoDocumento oTipoDocumento)
        {
            try
            {
                return Convert.ToBoolean(new DATipoDocumento().MantenerTipoDocumento(Opcion, oTipoDocumento));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BEMaestro> ObtenerTiposRiesgo()
        {
            return new DATipoDocumento().ObtenerTiposRiesgo();
        }

        public List<BEMaestro> ObtenerNivelesRiesgo()
        {
            return new DATipoDocumento().ObtenerNivelesRiesgo();
        }

    }
}
