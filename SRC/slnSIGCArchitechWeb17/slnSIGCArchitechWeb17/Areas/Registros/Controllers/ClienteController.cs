using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

using slnSIGCArchitechWeb17.Models;
using slnSIGCArchitechWeb17.Areas.Registros.Models;
using slnSIGCArchitechWeb17.Areas.Administracion.Models;

namespace slnSIGCArchitechWeb17.Areas.Registros.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Registros/Cliente
        [Authorize]
        public ActionResult Index(string IdCliente, string NombreEmpresa, string TipoEmpresa, string sMensaje, string sError)
        {
            var model = new EmpresaWebModel();
            var _TiposEmpresa = new BLEmpresa().ObtenerTiposEmpresa();
            ViewBag.TipoEmpresa = new SelectList(_TiposEmpresa, "Codigo", "Descripcion");
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (String.IsNullOrEmpty(IdCliente)) IdCliente = "";
            if (String.IsNullOrEmpty(NombreEmpresa)) NombreEmpresa = "";
            if (String.IsNullOrEmpty(TipoEmpresa)) TipoEmpresa = "";

            var _Clientes = new BLEmpresa().Listar(IdCliente, NombreEmpresa, TipoEmpresa, "CLIENTE", Session["IdUsuario"].ToString());
            model.lRegistrosEmpresas = _Clientes;

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Editar(string Id, string TipoEmpresaSiggo)
        {
            var clienteWebEditar = new EmpresaWebModel();
            BEEmpresa empresaWebEditar = new BLEmpresa().ObtenerEmpresa(Id);
            var _TipoEmpresa = new BLEmpresa().ObtenerTiposEmpresa();
            var _EmpresaOrigen = new BLEmpresa().Listar("", "", "", TipoEmpresaSiggo, Session["IdUsuario"].ToString());
            clienteWebEditar.lTipoEmpresa = _TipoEmpresa.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            clienteWebEditar.NuevoRegistro = true;
            clienteWebEditar.TipoEmpresaSiggo = TipoEmpresaSiggo;
            if (empresaWebEditar != null)
            {
                clienteWebEditar.NuevoRegistro = false;
                clienteWebEditar.IdEmpresa = empresaWebEditar.IdEmpresa;
                clienteWebEditar.RazonSocial = empresaWebEditar.RazonSocial;
                clienteWebEditar.Direccion = empresaWebEditar.Direccion;
                clienteWebEditar.Telefono = empresaWebEditar.Telefono;
                clienteWebEditar.Correo = empresaWebEditar.Correo;
                clienteWebEditar.Contacto = empresaWebEditar.Contacto;
                clienteWebEditar.RucEmpresa = empresaWebEditar.RucEmpresa;
                clienteWebEditar.NotificacionDiasVencimiento = empresaWebEditar.NotificacionDiasVencimiento;
                clienteWebEditar.ActividadNormalEspecifica = empresaWebEditar.ActividadNormalEspecifica;
                clienteWebEditar.TipoEmpresa = empresaWebEditar.TipoEmpresa;
                clienteWebEditar.IdCliente = empresaWebEditar.IdCliente;
                clienteWebEditar.TipoEmpresaSiggo = empresaWebEditar.TipoEmpresaSiggo;
                clienteWebEditar.EstadoDescripcion = empresaWebEditar.EstadoDescripcion;
                clienteWebEditar.FechaMaker = empresaWebEditar.FechaMaker;
                clienteWebEditar.HoraMaker = empresaWebEditar.HoraMaker;
                clienteWebEditar.Maker = empresaWebEditar.Maker;
            }
            return View(clienteWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(EmpresaWebModel model)
        {
            BEEmpresa oEmpresa = new BEEmpresa();
            var _TipoEmpresa = new BLEmpresa().ObtenerTiposEmpresa();
            var _EmpresaOrigen = new BLEmpresa().Listar("", "", "", model.TipoEmpresaSiggo, Session["IdUsuario"].ToString());
            try
            {
                if (ModelState.IsValid)
                {
                    oEmpresa.IdEmpresa = model.IdEmpresa;
                    oEmpresa.RazonSocial = model.RazonSocial == null ? "" : model.RazonSocial.Trim();
                    oEmpresa.RucEmpresa = model.RucEmpresa == null ? "" : model.RucEmpresa.Trim();
                    oEmpresa.Direccion = model.Direccion == null ? "" : model.Direccion.Trim();
                    oEmpresa.Telefono = model.Telefono == null ? "" : model.Telefono.Trim();
                    oEmpresa.Correo = model.Correo == null ? "" : model.Correo.Trim();
                    oEmpresa.PrefijoCorreo = model.PrefijoCorreo == null ? "" : oEmpresa.PrefijoCorreo.Trim();
                    oEmpresa.Contacto = model.Contacto == null ? "" : model.Contacto.Trim();
                    oEmpresa.NotificacionDiasVencimiento = model.NotificacionDiasVencimiento;
                    oEmpresa.ActividadNormalEspecifica = model.ActividadNormalEspecifica;
                    oEmpresa.TipoEmpresa = model.TipoEmpresa == null ? "" : model.TipoEmpresa;
                    oEmpresa.IdCliente = model.IdCliente == null ? null : model.IdCliente;
                    oEmpresa.TipoEmpresaSiggo = model.TipoEmpresaSiggo == null ? "" : model.TipoEmpresaSiggo;
                    oEmpresa.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                        new BLEmpresa().MantenerEmpresa(3, oEmpresa);
                    else
                        new BLEmpresa().MantenerEmpresa(2, oEmpresa);
                    return RedirectToAction("Index", "Cliente", new { area = "Registros", IdCliente = model.IdEmpresa, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Cliente " + model.IdEmpresa + " en forma exitosa" });
                }

                model.lTipoEmpresa = _TipoEmpresa.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                //model.lEmpresaOrigen = _EmpresaOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ClienteController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.lTipoEmpresa = _TipoEmpresa.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                //model.lEmpresaOrigen = _EmpresaOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Eliminar(string Id)
        {
            try
            {
                //Eliminando cliente
                BEEmpresa oEmpresa = new BEEmpresa();
                oEmpresa.IdEmpresa = Id;
                new BLEmpresa().MantenerEmpresa(4, oEmpresa);
                return RedirectToAction("Index", "Cliente", new { area = "Registros", sMensaje = "Se ha Eliminado al cliente " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ClienteController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Cliente", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }


        //-Usuarios por Cliente
        [Authorize]
        public ActionResult Usuarios(string IdEmpresaSel, string NombreEmpresaSel, string TipoEmpresaSiggo)
        {
            var model = new UsuarioWebModel();

            if (String.IsNullOrEmpty(IdEmpresaSel)) IdEmpresaSel = "";
            if (String.IsNullOrEmpty(NombreEmpresaSel)) NombreEmpresaSel = "";
            if (String.IsNullOrEmpty(TipoEmpresaSiggo)) TipoEmpresaSiggo = "";

            model.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            model.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            model.TipoEmpresaSiggo = TipoEmpresaSiggo;

            model.lRegistrosUsuarios = new BLUsuarioWeb().ListarUsuarios(IdEmpresaSel, "", "");

            return View(model);
        }

        [Authorize]
        public ActionResult EditarUsuario(string Id, string IdEmpresaSel, string NombreEmpresaSel, string TipoEmpresaSiggo)
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
            usuarioWebEditar.TipoEmpresaSiggo = String.IsNullOrEmpty(TipoEmpresaSiggo) ? "" : TipoEmpresaSiggo;
            return View(usuarioWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditarUsuario(UsuarioWebModel model)
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
                    oUsuario.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                    {
                        oUsuario.IdUsuario = Utilitario.GenerarUserID();
                        new BLUsuarioWeb().MantenerUsuario(3, oUsuario);
                    }
                    else
                        new BLUsuarioWeb().MantenerUsuario(2, oUsuario);

                    return RedirectToAction("Index", "Cliente", new { area = "Registros", IdCliente = model.IdEmpresa, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Usuario " + oUsuario.IdUsuario + " en forma exitosa" });
                }
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
                model.TipoEmpresaSiggo = String.IsNullOrEmpty(model.TipoEmpresaSiggo) ? "" : model.TipoEmpresaSiggo;
                model.lRoles = _Roles.ConvertAll(x => new ComunModel { Codigo = x.IDRol.ToString(), Descripcion = x.DescripcionRol });
                model.lRecibeNotificaciones = Helper.RecibeNotificaciones();
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ClienteController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
                model.TipoEmpresaSiggo = String.IsNullOrEmpty(model.TipoEmpresaSiggo) ? "" : model.TipoEmpresaSiggo;
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
                //Eliminando cliente
                BEUsuarioWeb oUsuario = new BEUsuarioWeb();
                oUsuario.IdEmpresa = IdEmpresaSel;
                oUsuario.IdUsuario = Id;
                oUsuario.RecibeNotificaciones = "N";
                new BLUsuarioWeb().MantenerUsuario(4, oUsuario);

                return RedirectToAction("Index", "Cliente", new { area = "Registros", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Eliminado al usuario " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Eliminar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Contratista", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
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

                return RedirectToAction("Index", "Cliente", new { area = "Registros", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Reseteado la Contraseña del usuario " + Id + " en forma exitosa" });

            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Resetear", "No se pudo resetear el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Usuario", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }
    }
}