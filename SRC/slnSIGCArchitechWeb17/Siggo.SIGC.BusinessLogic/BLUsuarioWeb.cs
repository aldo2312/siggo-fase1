using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Siggo.SIGC.DataAccess;
using Siggo.SIGC.Entity;

namespace Siggo.SIGC.BusinessLogic
{
    public class BLUsuarioWeb
    {
        public List<BEEmpresa> ListarEmpresas(string idUsuarioWeb)
        {
            try
            {
                return new DAUsuarioWeb().ListarEmpresas(idUsuarioWeb);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public BEUsuarioWeb ObtenerUsuario(string idUsuarioWeb)
        {
            try
            {
                return new DAUsuarioWeb().ObtenerUsuario(idUsuarioWeb);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public bool MantenerUsuario(int nOpcion, BEUsuarioWeb oUsuarioWeb)
        {
            try
            {
                return Convert.ToBoolean(new DAUsuarioWeb().MantenerUsuario(nOpcion, oUsuarioWeb));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BEUsuarioWeb> ListarUsuarios(string idEmpresa, string idUsuarioWeb, string nombreUsuarioWeb)
        {
            try
            {
                return new DAUsuarioWeb().ListarUsuarios(idEmpresa, idUsuarioWeb, nombreUsuarioWeb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
