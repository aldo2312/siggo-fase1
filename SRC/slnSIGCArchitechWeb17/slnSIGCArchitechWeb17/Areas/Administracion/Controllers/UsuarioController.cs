using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

using slnSIGCArchitechWeb17.Models;
using slnSIGCArchitechWeb17.Areas.Administracion.Models;

namespace slnSIGCArchitechWeb17.Areas.Administracion.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Administracion/Usuario
        [Authorize]
        public ActionResult Index(string IdEmpresaSel, string NombreEmpresaSel, string IdUsuarioWeb, string NombreUsuarioWeb, string TipoEmpresa, string sMensaje, string sError)
        {
            var model = new UsuarioWebModel();

            if (String.IsNullOrEmpty(IdEmpresaSel)) IdEmpresaSel = "";
            if (String.IsNullOrEmpty(NombreEmpresaSel)) NombreEmpresaSel = "";
            if (String.IsNullOrEmpty(IdUsuarioWeb)) IdUsuarioWeb = "";
            if (String.IsNullOrEmpty(NombreUsuarioWeb)) NombreUsuarioWeb = "";
            if (String.IsNullOrEmpty(TipoEmpresa)) TipoEmpresa = "";
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            model.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            model.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            var _Empresas = new BLEmpresa().Listar("", "", "", "NN", Session["IdUsuario"].ToString());
            model.lEmpresas = _Empresas.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
            model.lRegistrosUsuarios = new BLUsuarioWeb().ListarUsuarios(IdEmpresaSel, IdUsuarioWeb, NombreUsuarioWeb);

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Editar(string Id, string IdEmpresaSel, string NombreEmpresaSel)
        {
            var usuarioWebEditar = new UsuarioWebModel();
            BEUsuarioWeb usuarioWeb = new BLUsuarioWeb().ObtenerUsuario(Id);
            var _Roles = new BLSistemaWeb().ObtenerRolesUsuario(IdEmpresaSel);
            usuarioWebEditar.lRoles = _Roles.ConvertAll(x => new ComunModel { Codigo = x.IDRol.ToString(), Descripcion = x.DescripcionRol });
            usuarioWebEditar.lRecibeNotificaciones = Helper.RecibeNotificaciones();
            usuarioWebEditar.NuevoRegistro = true;
            usuarioWebEditar.IdEmpresa = IdEmpresaSel;
            usuarioWebEditar.IdUsuario = "?????";
            if (usuarioWeb != null)
            {
                usuarioWebEditar.NuevoRegistro = false;
                usuarioWebEditar.IdEmpresa = usuarioWeb.IdEmpresa;
                usuarioWebEditar.IdUsuario = usuarioWeb.IdUsuario;
                usuarioWebEditar.NombreUsuario = usuarioWeb.NombreUsuario;
                usuarioWebEditar.ApellidoPaternoUsuario = usuarioWeb.ApellidoPaternoUsuario;
                usuarioWebEditar.ApellidoMaternoUsuario = usuarioWeb.ApellidoMaternoUsuario;
                usuarioWebEditar.CorreoElectronico = usuarioWeb.CorreoElectronico;
                usuarioWebEditar.Contrasenha = Utilitario.DecryptText(usuarioWeb.Contrasenha);
                usuarioWebEditar.IdRol = usuarioWeb.IdRol;
                usuarioWebEditar.FechaAsignacion = usuarioWeb.FechaAsignacion;
                usuarioWebEditar.EsUsuarioInterno = usuarioWeb.EsUsuarioInterno;
                usuarioWebEditar.RecibeNotificaciones = usuarioWeb.RecibeNotificaciones;
                usuarioWebEditar.EstadoDescripcion = usuarioWeb.EstadoDescripcion;
                usuarioWebEditar.FechaMaker = usuarioWeb.FechaMaker;
                usuarioWebEditar.HoraMaker = usuarioWeb.HoraMaker;
                usuarioWebEditar.Maker = usuarioWeb.Maker;
            }
            usuarioWebEditar.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            usuarioWebEditar.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            return View(usuarioWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(UsuarioWebModel model)
        {
            BEUsuarioWeb oUsuario = new BEUsuarioWeb();
            var _Roles = new BLSistemaWeb().ObtenerRolesUsuario(model.IdEmpresa);
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario.IdEmpresa = model.IdEmpresa == null ? "" : model.IdEmpresa;
                    oUsuario.IdUsuario = model.IdUsuario == null ? "" : model.IdUsuario;
                    oUsuario.NombreUsuario = model.NombreUsuario == null ? "" : model.NombreUsuario;
                    oUsuario.ApellidoPaternoUsuario = model.ApellidoPaternoUsuario == null ? "" : model.ApellidoPaternoUsuario;
                    oUsuario.ApellidoMaternoUsuario = model.ApellidoMaternoUsuario == null ? "" : model.ApellidoMaternoUsuario;
                    oUsuario.CorreoElectronico = model.CorreoElectronico == null ? "" : model.CorreoElectronico;
                    oUsuario.Contrasenha = model.Contrasenha == null ? "" : Utilitario.EncryptText(model.Contrasenha);
                    oUsuario.IdRol = model.IdRol;
                    oUsuario.RecibeNotificaciones = model.RecibeNotificaciones;
                    oUsuario.FechaAsignacion = null;
                    oUsuario.EsUsuarioInterno = model.IdEmpresa == "SIGGO" ? true : false;
                    oUsuario.Maker= Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                    {
                        oUsuario.IdUsuario= Utilitario.GenerarUserID();
                        new BLUsuarioWeb().MantenerUsuario(3, oUsuario);
                    }
                    else
                        new BLUsuarioWeb().MantenerUsuario(2, oUsuario);

                    model.IdUsuario = oUsuario.IdUsuario;
                    model.IdEmpresa = oUsuario.IdEmpresa;

                    return RedirectToAction("Index", "Usuario", new { area = "Administracion", IdUsuarioWeb = model.IdUsuario, IdEmpresaSel = model.IdEmpresa, NombreEmpresaSel = model.NombreEmpresaSel, TipoEmpresaSiggo = model.TipoEmpresaSiggo, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Usuario " + model.IdUsuario + " en forma exitosa" });
                }
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
                //model.TipoEmpresaSiggo = String.IsNullOrEmpty(model.TipoEmpresaSiggo) ? "" : model.TipoEmpresaSiggo;
                model.lRoles = _Roles.ConvertAll(x => new ComunModel { Codigo = x.IDRol.ToString(), Descripcion = x.DescripcionRol });
                model.lRecibeNotificaciones = Helper.RecibeNotificaciones();
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "UsuarioController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
                //model.TipoEmpresaSiggo = String.IsNullOrEmpty(model.TipoEmpresaSiggo) ? "" : model.TipoEmpresaSiggo;
                model.lRoles = _Roles.ConvertAll(x => new ComunModel { Codigo = x.IDRol.ToString(), Descripcion = x.DescripcionRol });
                model.lRecibeNotificaciones = Helper.RecibeNotificaciones();
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }       

        [Authorize]
        public ActionResult Eliminar(string IdEmpresaSel, string NombreEmpresaSel, string Id)
        {
            try
            {
                //Eliminando
                BEUsuarioWeb oUsuario = new BEUsuarioWeb();
                oUsuario.IdEmpresa = IdEmpresaSel;
                oUsuario.IdUsuario = Id;
                oUsuario.RecibeNotificaciones = "N";
                new BLUsuarioWeb().MantenerUsuario(4, oUsuario);                
                return RedirectToAction("Index", "Usuario", new { area = "Administracion", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Eliminado al usuario " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "UsuarioController.Eliminar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Usuario", new { area = "Administracion", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

        [Authorize]
        public ActionResult Resetear(string IdEmpresaSel, string NombreEmpresaSel, string Id)
        {
            try
            {
                //Reseteando cuenta del usuario
                BEUsuarioWeb oUsuario = new BEUsuarioWeb();
                oUsuario.IdEmpresa = IdEmpresaSel;
                oUsuario.IdUsuario = Id;
                oUsuario.Contrasenha = Utilitario.EncryptText(Id);
                oUsuario.FechaAsignacion = DateTime.Now;
                oUsuario.RecibeNotificaciones = "N";
                new BLUsuarioWeb().MantenerUsuario(5, oUsuario);

                return RedirectToAction("Index", "Usuario", new { area = "Administracion", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Reseteado la Contraseña del usuario " + Id + " en forma exitosa" });

            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "UsuarioController.Resetear", "No se pudo resetear el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Usuario", new { area = "Administracion", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }
    }
}