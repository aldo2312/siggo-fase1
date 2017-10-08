using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLEmpresa
    {
        public BEEmpresa ObtenerEmpresa(string IdEmpresa)
        {
            return new DAEmpresa().ObtenerEmpresa(IdEmpresa);
        }

        public List<BEEmpresa> Listar(string Id, string Nombre, string TipoEmpresa, string TipoEmpresaSiggo, string Usuario)
        {
            return new DAEmpresa().Listar(Id, Nombre, TipoEmpresa, TipoEmpresaSiggo, Usuario);
        }

        public bool MantenerEmpresa(int Opcion, BEEmpresa oEmpresa)
        {
            try
            {
                return Convert.ToBoolean(new DAEmpresa().MantenerEmpresa(Opcion, oEmpresa));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEMaestro> ObtenerTiposEmpresa()
        {
            try
            {
                return new DAEmpresa().ObtenerTiposEmpresa();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
