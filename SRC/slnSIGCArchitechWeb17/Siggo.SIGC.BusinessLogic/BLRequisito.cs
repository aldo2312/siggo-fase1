using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLRequisito
    {
        public List<BERequisito> Listar(string IdEmpresa, string Id, string Descripcion, string TipoRecurso, string Usuario)
        {
            return new DARequisito().Listar(IdEmpresa, Id, Descripcion, TipoRecurso, Usuario);
        }

        public BERequisito ObtenerRequisito(string IdEmpresa, string Id, string IdTipoServicio)
        {
            return new DARequisito().ObtenerRequisito(IdEmpresa, Id, IdTipoServicio);
        }

        public int MantenerRequisito(int Opcion, BERequisito oRequisito)
        {
            return new DARequisito().MantenerRequisito(Opcion, oRequisito);
        }

        public List<BEMaestro> ObtenerPeriodicidad()
        {
            return new DARequisito().ObtenerPeriodicidad();
        }

        public List<BERequisitoDato> ObtenerRequisitosDato(string IdEmpresa, string IdServicio, string IdRequisito)
        {
            return new DARequisito().ObtenerRequisitosDatos(IdEmpresa, Convert.ToInt32(IdServicio), Convert.ToInt32(IdRequisito));
        }

        public void MantenerRequisitoDato(int Opcion, List<BERequisitoDato> lReqDatos)
        {
            new DARequisito().MantenerRequisitoDato(Opcion, lReqDatos);
        }
    }
}
