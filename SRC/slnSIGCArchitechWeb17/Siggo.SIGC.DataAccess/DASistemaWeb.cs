using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    public class DASistemaWeb
    {
        public List<BEMenuSistema> ObtenerOpcionesMenu(string idUsuarioWeb)
        {
            List<BEMenuSistema> lOpciones = new List<BEMenuSistema>();
            try
            {
                using (DASistemaWebDataContext dc = new DASistemaWebDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_OPCIONES_MENU_ROL(idUsuarioWeb);
                    foreach (var item in lnq_Query)
                    {
                        lOpciones.Add(new BEMenuSistema()
                        {
                            IdMenu = Convert.ToInt32(item.IdMenu),
                            PreMenu = Convert.ToInt32(item.PreMenu),
                            DescripcionMenu = Convert.ToString(item.DescripcionMenu),
                            Grupo = Convert.ToString(item.FlagTipo) == "1" ? true : false,
                            Action = Convert.ToString(item.ActionResult),
                            Controller = Convert.ToString(item.Controller)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lOpciones;
        }

        public List<BERoles> ObtenerRolesUsuario(string strIdEmpresa)
        {
            List<BERoles> lRoles = new List<BERoles>();
            try
            {
                using (DASistemaWebDataContext dc = new DASistemaWebDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_ROLES(strIdEmpresa);
                    foreach (var item in lnq_Query)
                    {
                        lRoles.Add(new BERoles()
                        {
                            IDRol = item.IdRol,
                            DescripcionRol = item.DescripcionRol
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lRoles;
        }
        
    }
}
