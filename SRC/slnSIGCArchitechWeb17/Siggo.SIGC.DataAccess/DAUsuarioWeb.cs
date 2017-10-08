using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace Siggo.SIGC.DataAccess
{
    public class DAUsuarioWeb
    {
        public List<BEEmpresa> ListarEmpresas(string idUsuarioWeb)
        {
            List<BEEmpresa> lEmpresas = new List<BEEmpresa>();
            try
            {
                using (DAUsuarioWebDataContext dc = new DAUsuarioWebDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_EMPRESA_USUARIO(idUsuarioWeb);
                    foreach (var item in lnq_Query)
                    {
                        lEmpresas.Add(new BEEmpresa()
                        {
                            IdEmpresa = item.CODIGO,
                            RazonSocial = item.DESCRIPCION
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lEmpresas;
        }
        public BEUsuarioWeb ObtenerUsuario(string idUsuarioWeb)
        {
            BEUsuarioWeb retorno = null;
            try
            {
                using (DAUsuarioWebDataContext dc = new DAUsuarioWebDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_OBTENER_USUARIO_WEB(idUsuarioWeb).ToArray();
                    if (lnq_Query.Count() > 0)
                    {
                        var item = lnq_Query.First();
                        retorno = new BEUsuarioWeb();
                        retorno.IdEmpresa = item.IDEMPRESA == null ? "" : item.IDEMPRESA;
                        retorno.IdUsuario = item.IDUSUARIO == null ? "" : item.IDUSUARIO;
                        retorno.NombreUsuario = item.NOMBREUSUARIO == null ? "" : item.NOMBREUSUARIO;
                        retorno.ApellidoPaternoUsuario = item.ApellidoPaternoUsuario == null ? "" : item.ApellidoPaternoUsuario;
                        retorno.ApellidoMaternoUsuario = item.ApellidoMaternoUsuario == null ? "" : item.ApellidoMaternoUsuario;
                        retorno.CorreoElectronico = item.CORREO == null ? "" : item.CORREO;
                        retorno.Contrasenha = item.CONTRASENHA == null ? "" : item.CONTRASENHA;
                        retorno.IdRol = item.IDROL;
                        retorno.FechaAsignacion = item.FECHAASIGNACION;
                        retorno.EsUsuarioInterno = item.ESUSUARIOSIGGO;
                        retorno.RecibeNotificaciones = Convert.ToString(item.RECIBENOTIFICACIONES);
                        retorno.Estado = Convert.ToString(item.ESTADO);
                        retorno.EstadoDescripcion = MC.get_desc_mk(Convert.ToString(item.ESTADO), Convert.ToString(item.ACCION));
                        retorno.FechaMaker = item.FECMAKER;
                        retorno.HoraMaker = item.HORAMAKER;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;

        }
        public bool Existe(string sIdUsuarioWeb)
        {
            return (ObtenerUsuario(sIdUsuarioWeb) == null ? false : true);
        }
        public int MantenerUsuario(int nOpcion, BEUsuarioWeb oUsuarioWeb)
        {
            try
            {
                using (DAUsuarioWebDataContext dc= new DAUsuarioWebDataContext(Globales.ConfigServidor()))
                {
                    var Lqn_Resultado = dc.SP_MANT_REG_USUARIOWEB(nOpcion,
                        oUsuarioWeb.IdEmpresa,
                        oUsuarioWeb.IdUsuario,
                        oUsuarioWeb.NombreUsuario,
                        oUsuarioWeb.ApellidoPaternoUsuario,
                        oUsuarioWeb.ApellidoMaternoUsuario,
                        oUsuarioWeb.CorreoElectronico,
                        oUsuarioWeb.Contrasenha,
                        oUsuarioWeb.IdRol,
                        oUsuarioWeb.FechaAsignacion,
                        oUsuarioWeb.EsUsuarioInterno,
                        Convert.ToChar(oUsuarioWeb.RecibeNotificaciones),
                        oUsuarioWeb.Maker);
                    return Lqn_Resultado == 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEUsuarioWeb> ListarUsuarios(string idEmpresa, string idUsuarioWeb, string nombreUsuarioWeb)
        {
            List<BEUsuarioWeb> lUsuarios = new List<BEUsuarioWeb>();
            try
            {
                using (DAUsuarioWebDataContext dc = new DAUsuarioWebDataContext(Globales.ConfigServidor()))
                {
                    var lnq_Query = dc.SP_LISTAR_USUARIOSWEB(idEmpresa, idUsuarioWeb, nombreUsuarioWeb);
                    foreach (var item in lnq_Query)
                    {
                        lUsuarios.Add(new BEUsuarioWeb()
                        {
                            IdEmpresa = item.IdEmpresa,
                            EmpresaAsociada = item.RazonSocial,
                            TipoEmpresaSiggo = item.TipoEmpresaSiggo,
                            IdUsuario = item.IdUsuario,
                            NombreUsuario = item.NombreUsuario,
                            CorreoElectronico = item.Correo,
                            IdRol = item.IdRol,
                            RolDescripcion = item.DescripcionRol,
                            FechaAsignacion = item.FechaAsignacion,
                            EsUsuarioInterno = item.EsUsuarioSiggo,
                            UsuarioSiggo = (bool)item.EsUsuarioSiggo ? "Si" : "No",
                            RecibeNotificaciones = Convert.ToString(item.RecibeNotificaciones),
                            Estado = MC.get_desc_mk(Convert.ToString(item.ESTADO), Convert.ToString(item.ACCION)),
                            FechaMaker = item.FECHAREGISTRO.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lUsuarios;
        }
    }
}
