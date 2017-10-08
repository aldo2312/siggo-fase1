
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLRecurso
    {
        public List<BERecurso> Listar(string IdEmpresa, string Id, string Descripcion, string TipoRecurso)
        {
            return new DARecurso().Listar(IdEmpresa, Id, Descripcion, TipoRecurso);
        }

        public BERecurso ObtenerRecurso(string IdEmpresa, string Id)
        {
            return new DARecurso().ObtenerRecurso(IdEmpresa, Id);
        }

        public bool MantenerRecurso(int Opcion, BERecurso oRecurso)
        {
            try
            {
                return Convert.ToBoolean(new DARecurso().MantenerRecurso(Opcion, oRecurso));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MantenerRequisitoRecurso(int Opcion, List<BERequisitoRecurso> lRequisitoRecurso)
        {
            new DARecurso().MantenerRequisitoRecurso(Opcion, lRequisitoRecurso);
        }

        public List<BEMaestro> ObtenerTiposRecurso()
        {
            return new DARecurso().ObtenerTiposRecurso();
        }

        public List<BERequisitoRecurso> ObtenerRequisitosRecurso(string IdEmpresa, string IdRecurso)
        {
            return new DARecurso().ObtenerRequisitosRecurso(IdEmpresa, IdRecurso);
        }

        public List<BETipoServicio> ObtenerServicio()
        {
            return new DARecurso().ObtenerServicio();
        }

        public List<BERequisito> ObtenerRequisitoPorServicio(string IdEmpresa, string IdTipoServicio, string TipoRecurso)
        {
            return new DARecurso().ObtenerRequisitoPorServicio(IdEmpresa, IdTipoServicio, TipoRecurso);
        }
    }
}
