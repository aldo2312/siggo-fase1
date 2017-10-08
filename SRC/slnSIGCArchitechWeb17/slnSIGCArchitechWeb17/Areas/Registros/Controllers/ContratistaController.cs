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
using System.Web.Script.Serialization;

namespace slnSIGCArchitechWeb17.Areas.Registros.Controllers
{
    public class ContratistaController : Controller
    {
        // GET: Registros/Contratista
        [Authorize]
        public ActionResult Index(string IdContratista, string NombreEmpresa, string TipoEmpresa, string sMensaje, string sError)
        {
            var model = new EmpresaWebModel();
            var _TiposEmpresa = new BLEmpresa().ObtenerTiposEmpresa();
            ViewBag.TipoEmpresa = new SelectList(_TiposEmpresa, "Codigo", "Descripcion");
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (String.IsNullOrEmpty(IdContratista)) IdContratista = "";
            if (String.IsNullOrEmpty(NombreEmpresa)) NombreEmpresa = "";
            if (String.IsNullOrEmpty(TipoEmpresa)) TipoEmpresa = "";

            var _Clientes = new BLEmpresa().Listar(IdContratista, NombreEmpresa, TipoEmpresa, "CONTRATISTA", Session["IdUsuario"].ToString());
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
            var contratistaWebEditar = new EmpresaWebModel();
            BEEmpresa empresaWebEditar = new BLEmpresa().ObtenerEmpresa(Id);
            var _TipoEmpresa = new BLEmpresa().ObtenerTiposEmpresa();
            var _EmpresaOrigen = new BLEmpresa().Listar("", "", "", "CLIENTE", Session["IdUsuario"].ToString());
            contratistaWebEditar.lTipoEmpresa = _TipoEmpresa.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            contratistaWebEditar.lEmpresaOrigen = _EmpresaOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
            contratistaWebEditar.NuevoRegistro = true;
            contratistaWebEditar.TipoEmpresaSiggo = TipoEmpresaSiggo;
            if (empresaWebEditar != null)
            {
                contratistaWebEditar.NuevoRegistro = false;
                contratistaWebEditar.IdEmpresa = empresaWebEditar.IdEmpresa;
                contratistaWebEditar.RazonSocial = empresaWebEditar.RazonSocial;
                contratistaWebEditar.Direccion = empresaWebEditar.Direccion;
                contratistaWebEditar.Telefono = empresaWebEditar.Telefono;
                contratistaWebEditar.Correo = empresaWebEditar.Correo;
                contratistaWebEditar.Contacto = empresaWebEditar.Contacto;
                contratistaWebEditar.RucEmpresa = empresaWebEditar.RucEmpresa;
                contratistaWebEditar.NotificacionDiasVencimiento = empresaWebEditar.NotificacionDiasVencimiento;
                contratistaWebEditar.ActividadNormalEspecifica = empresaWebEditar.ActividadNormalEspecifica;
                contratistaWebEditar.TipoEmpresa = empresaWebEditar.TipoEmpresa;
                contratistaWebEditar.IdCliente = empresaWebEditar.IdCliente;
                contratistaWebEditar.TipoEmpresaSiggo = empresaWebEditar.TipoEmpresaSiggo;
                contratistaWebEditar.EstadoDescripcion = empresaWebEditar.EstadoDescripcion;
                contratistaWebEditar.FechaMaker = empresaWebEditar.FechaMaker;
                contratistaWebEditar.HoraMaker = empresaWebEditar.HoraMaker;
                contratistaWebEditar.Maker = empresaWebEditar.Maker;
            }
            return View(contratistaWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(EmpresaWebModel model)
        {
            BEEmpresa oEmpresa = new BEEmpresa();
            var _TipoEmpresa = new BLEmpresa().ObtenerTiposEmpresa();
            var _EmpresaOrigen = new BLEmpresa().Listar("", "", "", "CLIENTE", Session["IdUsuario"].ToString());
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
                    return RedirectToAction("Index", "Contratista", new { area = "Registros", IdCliente = model.IdEmpresa, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Cliente " + model.IdEmpresa + " en forma exitosa" });
                }

                model.lTipoEmpresa = _TipoEmpresa.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lEmpresaOrigen = _EmpresaOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.lTipoEmpresa = _TipoEmpresa.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lEmpresaOrigen = _EmpresaOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
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
                return RedirectToAction("Index", "Contratista", new { area = "Registros", sMensaje = "Se ha Eliminado al contratista " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Contratista", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }            
        }

        #region Usuarios

        //-Usuarios por Contratista
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

                    return RedirectToAction("Index", "Contratista", new { area = "Registros", IdContratista = model.IdEmpresa, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Usuario " + oUsuario.IdUsuario + " en forma exitosa" });
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
                "ContratistaController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
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
        public ActionResult EliminarUsuario(string IdEmpresaSel, string NombreEmpresaSel, string Id)
        {
            try
            {
                //Eliminando cliente
                BEUsuarioWeb oUsuario = new BEUsuarioWeb();
                oUsuario.IdEmpresa = IdEmpresaSel;
                oUsuario.IdUsuario = Id;
                oUsuario.RecibeNotificaciones = "N";
                new BLUsuarioWeb().MantenerUsuario(4, oUsuario);
                
                return RedirectToAction("Index", "Contratista", new { area = "Registros", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Eliminado al usuario " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
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

                return RedirectToAction("Index", "Contratista", new { area = "Registros", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Reseteado la Contraseña del usuario " + Id + " en forma exitosa" });

            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Resetear", "No se pudo resetear el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Usuario", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

        #endregion

        #region Empleados

        //-Empleados por Contratista
        [Authorize]
        public ActionResult Empleados(string IdEmpresaSel, string NombreEmpresaSel, string IdEmpleado, string NombreEmpleado, string sMensaje, string sError)
        {
            var model = new RecursoWebModel();

            if (String.IsNullOrEmpty(IdEmpresaSel)) IdEmpresaSel = "";
            if (String.IsNullOrEmpty(NombreEmpresaSel)) NombreEmpresaSel = "";
            if (String.IsNullOrEmpty(IdEmpleado)) IdEmpleado = "";
            if (String.IsNullOrEmpty(NombreEmpleado)) NombreEmpleado = "";

            model.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            model.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;

            model.lRegistrosRecursos = new BLRecurso().Listar(IdEmpresaSel, "", "", "302");

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult EditarEmpleado(string Id, string IdEmpresaSel, string NombreEmpresaSel)
        {
            var recursoWebEditar = new RecursoWebModel();
            BERecurso recurso = new BLRecurso().ObtenerRecurso(IdEmpresaSel, Id);
            var _Roles = new BLRecurso().ObtenerTiposRecurso();
            recursoWebEditar.lTipoRecurso = _Roles.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            recursoWebEditar.NuevoRegistro = true;
            recursoWebEditar.IdEmpresa = IdEmpresaSel;
            recursoWebEditar.IdRecurso = "?????";
            if (recurso != null)
            {
                recursoWebEditar.NuevoRegistro = false;
                recursoWebEditar.IdEmpresa = recurso.IdEmpresa;
                recursoWebEditar.IdRecurso = recurso.IdRecurso;
                recursoWebEditar.NumeroReferencia = recurso.NumeroReferencia;
                recursoWebEditar.Descripcion1 = recurso.Descripcion1;
                recursoWebEditar.Descripcion2 = recurso.Descripcion2;
                recursoWebEditar.Descripcion3 = recurso.Descripcion3;
                //recursoWebEditar.Descripcion4 = recurso.Descripcion4;
                recursoWebEditar.Observacion = recurso.Observacion;
                recursoWebEditar.Cantidad = recurso.Cantidad;
                recursoWebEditar.TipoRecurso = recurso.TipoRecurso;
                recursoWebEditar.EstadoDescripcion = recurso.EstadoDescripcion;
                recursoWebEditar.FechaMaker = recurso.FechaMaker;
                recursoWebEditar.HoraMaker = recurso.HoraMaker;
                recursoWebEditar.Maker = recurso.Maker;
            }
            recursoWebEditar.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            recursoWebEditar.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            return View(recursoWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditarEmpleado(RecursoWebModel model)
        {
            BERecurso oRecurso = new BERecurso();
            var _TiposRecurso = new BLRecurso().ObtenerTiposRecurso();
            model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
            model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
            model.lTipoRecurso = _TiposRecurso.ConvertAll(x => new ComunModel { Codigo = x.Codigo.ToString(), Descripcion = x.Descripcion });
            try
            {
                if (ModelState.IsValid)
                {
                    oRecurso.IdEmpresa = model.IdEmpresaSel == null ? "" : model.IdEmpresaSel;
                    oRecurso.IdRecurso = model.IdRecurso == null ? "" : model.IdRecurso;
                    oRecurso.NumeroReferencia = model.NumeroReferencia == null ? "" : model.NumeroReferencia;
                    oRecurso.Descripcion1 = model.Descripcion1 == null ? "" : model.Descripcion1;
                    oRecurso.Descripcion2 = model.Descripcion2 == null ? "" : model.Descripcion2;
                    oRecurso.Descripcion3 = model.Descripcion3 == null ? "" : model.Descripcion3;
                    oRecurso.Descripcion4 = model.Descripcion1 == null ? "" : string.Concat(model.Descripcion1, " ", model.Descripcion2, " ", model.Descripcion3);
                    oRecurso.Observacion = model.Observacion == null ? "" : model.Observacion;
                    oRecurso.Cantidad = 0;
                    oRecurso.TipoRecurso = model.TipoRecurso == null ? "" : model.TipoRecurso;
                    oRecurso.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                    {
                        new BLRecurso().MantenerRecurso(3, oRecurso);
                    }
                    else
                        new BLRecurso().MantenerRecurso(2, oRecurso);

                    return RedirectToAction("Empleados", "Contratista", new { area = "Registros", IdEmpresaSel = model.IdEmpresa, NombreEmpresaSel = model.NombreEmpresaSel, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Recurso " + oRecurso.IdRecurso + " en forma exitosa" });
                }
                
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.EditarEmpleado", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
                model.lTipoRecurso = _TiposRecurso.ConvertAll(x => new ComunModel { Codigo = x.Codigo.ToString(), Descripcion = x.Descripcion });                
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        [Authorize]
        public ActionResult EliminarEmpleado(string IdEmpresaSel, string NombreEmpresaSel, string Id)
        {
            try
            {
                //Eliminando cliente
                BERecurso oUsuario = new BERecurso();
                oUsuario.IdEmpresa = IdEmpresaSel;
                oUsuario.IdRecurso = Id;
                new BLRecurso().MantenerRecurso(4, oUsuario);

                return RedirectToAction("Index", "Contratista", new { area = "Registros", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Eliminado al usuario " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Contratista", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

        [Authorize]
        public ActionResult RequisitosEmpleado(string IdEmpresaSel, string sIdRecurso)
        {
            var model = new RecursoDetallesWebModel();
            //var _Requisito = new BLRequisito().Listar(IdEmpresaSel, "", "", "302");
            //ViewBag.AgregarRequisito = new SelectList(_Requisito, "IdTipoDocumento", "DescripcionRequisito");
            model.IdEmpresaSel = IdEmpresaSel;
            model.IdRecursoSel = sIdRecurso;
            model.lRecursosRequisitos = new BLRecurso().ObtenerRequisitosRecurso(IdEmpresaSel, sIdRecurso);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RequisitosEmpleado(string IdEmpresaSel, string sIdRecurso,
            string btnQuitarReq, List<int> ChecksReq, List<string> IdsServicio, List<string> IdsRequisito)
        {
            try
            {
                if (!String.IsNullOrEmpty(btnQuitarReq))
                {
                    List<BERequisitoRecurso> lstReq = new List<BERequisitoRecurso>();
                    if (ChecksReq != null)
                    {
                        foreach (var nFila in ChecksReq)
                        {
                            lstReq.Add(new BERequisitoRecurso()
                            {
                                IdEmpresa = IdEmpresaSel,
                                IdRecurso = sIdRecurso,
                                IdTipoServicio = IdsServicio[nFila],
                                IdTipoDocumento = IdsRequisito[nFila],
                                DescripcionDocumento = "",
                                EsRequerido = false,
                                NecesitaAdjunto = false,
                                Orden = 0,
                                Maker = Session["IdUsuario"].ToString()
                            });
                        }
                        new BLRecurso().MantenerRequisitoRecurso(4, lstReq);
                        ViewBag.MensajeRequisito = "Se ha(n) Quitado el(os) requisito(s) en forma exitosa";
                    }
                }

                //
                var model = new RecursoDetallesWebModel();
                model.IdEmpresaSel = IdEmpresaSel;
                model.IdRecursoSel = sIdRecurso;
                model.lRecursosRequisitos = new BLRecurso().ObtenerRequisitosRecurso(IdEmpresaSel, sIdRecurso);

                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.RequisitosDetalle", "No se pudo quitar el(os) registro(s). Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult GrabarRequisitos(RecursoDetallesWebModel objRequisito)
        {
            bool status = false;
            List<BERequisitoRecurso> details = new List<BERequisitoRecurso>();
            if (ModelState.IsValid)
            {
                foreach (var i in objRequisito.lRecursosRequisitos)
                {
                    details.Add(new BERequisitoRecurso()
                    {
                        IdEmpresa = i.IdEmpresa,
                        IdRecurso = i.IdRecurso,
                        IdTipoServicio = i.IdTipoServicio,
                        IdTipoDocumento = i.IdTipoDocumento,
                        DescripcionDocumento = i.DescripcionDocumento,
                        EsRequerido = i.EsRequerido,
                        NecesitaAdjunto = i.NecesitaAdjunto,
                        Orden = i.Orden,
                        Maker = Session["IdUsuario"].ToString()
                    });
                }
                new BLRecurso().MantenerRequisitoRecurso(3, details);
                status = true;
                //ViewBag.MensajeRequisito = "Se ha(n) Agregado el(os) requisito(s) en forma exitosa";
            }
            else
            {
                status = false;
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return new JsonResult { Data = new { status = status, MensajeRequisito= "Se ha(n) Agregado el(os) requisito(s) en forma exitosa" } };
        }

        public JsonResult ObtenerTiposServicios()
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lServicios = new BLRecurso().ObtenerServicio();
            string _Servicios = "-1";
            try
            {
                _Servicios = jssObject.Serialize(lServicios);
            }
            catch (Exception)
            {
                return Json(_Servicios);
            }
            return Json(_Servicios);
        }

        public JsonResult ObtenerTiposRequisitos(string IdEmpresaSel, string IdTipoServicio, string TipoRecurso)
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lRequisitos = new BLRecurso().ObtenerRequisitoPorServicio(IdEmpresaSel, IdTipoServicio, TipoRecurso);
            string _Requisitos = "";
            try
            {
                _Requisitos = jssObject.Serialize(lRequisitos);
            }
            catch (Exception)
            {
                return Json(_Requisitos);
            }
            return Json(_Requisitos);
        }

        #endregion

        #region Vehiculos

        //-Vehiculos por Contratista
        [Authorize]
        public ActionResult Vehiculos(string IdEmpresaSel, string NombreEmpresaSel, string IdVehiculo, string NombreVehiculo, string sMensaje, string sError)
        {
            var model = new RecursoWebModel();

            if (String.IsNullOrEmpty(IdEmpresaSel)) IdEmpresaSel = "";
            if (String.IsNullOrEmpty(NombreEmpresaSel)) NombreEmpresaSel = "";
            if (String.IsNullOrEmpty(IdVehiculo)) IdVehiculo = "";
            if (String.IsNullOrEmpty(NombreVehiculo)) NombreVehiculo = "";

            model.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            model.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;

            model.lRegistrosRecursos = new BLRecurso().Listar(IdEmpresaSel, "", "", "303");

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult EditarVehiculo(string Id, string IdEmpresaSel, string NombreEmpresaSel)
        {
            var recursoWebEditar = new RecursoWebModel();
            BERecurso recurso = new BLRecurso().ObtenerRecurso(IdEmpresaSel, Id);
            var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
            recursoWebEditar.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            recursoWebEditar.NuevoRegistro = true;
            recursoWebEditar.IdEmpresa = IdEmpresaSel;
            recursoWebEditar.IdRecurso = "?????";
            if (recurso != null)
            {
                recursoWebEditar.NuevoRegistro = false;
                recursoWebEditar.IdEmpresa = recurso.IdEmpresa;
                recursoWebEditar.IdRecurso = recurso.IdRecurso;
                recursoWebEditar.NumeroReferencia = recurso.NumeroReferencia;
                recursoWebEditar.Descripcion4 = recurso.Descripcion4;
                recursoWebEditar.Observacion = recurso.Observacion;
                recursoWebEditar.TipoRecurso = recurso.TipoRecurso;
                recursoWebEditar.EstadoDescripcion = recurso.EstadoDescripcion;
                recursoWebEditar.FechaMaker = recurso.FechaMaker;
                recursoWebEditar.HoraMaker = recurso.HoraMaker;
                recursoWebEditar.Maker = recurso.Maker;
            }
            recursoWebEditar.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            recursoWebEditar.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            return View(recursoWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditarVehiculo(RecursoWebModel model)
        {
            BERecurso oRecurso = new BERecurso();
            var _TiposRecurso = new BLRecurso().ObtenerTiposRecurso();
            model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
            model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
            model.lTipoRecurso = _TiposRecurso.ConvertAll(x => new ComunModel { Codigo = x.Codigo.ToString(), Descripcion = x.Descripcion });

            try
            {
                if (ModelState.IsValid)
                {
                    oRecurso.IdEmpresa = model.IdEmpresaSel == null ? "" : model.IdEmpresaSel;
                    oRecurso.IdRecurso = model.IdRecurso == null ? "" : model.IdRecurso;
                    oRecurso.NumeroReferencia = model.NumeroReferencia == null ? "" : model.NumeroReferencia;
                    oRecurso.Descripcion1 = "";
                    oRecurso.Descripcion2 = "";
                    oRecurso.Descripcion3 = "";
                    oRecurso.Descripcion4 = model.Descripcion4 == null ? "" : model.Descripcion4;
                    oRecurso.Observacion = model.Observacion == null ? "" : model.Observacion;
                    oRecurso.TipoRecurso = model.TipoRecurso == null ? "" : model.TipoRecurso;
                    oRecurso.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                    {
                        new BLRecurso().MantenerRecurso(3, oRecurso);
                    }
                    else
                        new BLRecurso().MantenerRecurso(2, oRecurso);

                    return RedirectToAction("Vehiculos", "Contratista", new { area = "Registros", IdEmpresaSel = model.IdEmpresa, NombreEmpresaSel = model.NombreEmpresaSel, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Recurso " + oRecurso.IdRecurso + " en forma exitosa" });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.EditarVehiculo", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;
                model.lTipoRecurso = _TiposRecurso.ConvertAll(x => new ComunModel { Codigo = x.Codigo.ToString(), Descripcion = x.Descripcion });
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        [Authorize]
        public ActionResult EliminarVehiculo(string IdEmpresaSel, string NombreEmpresaSel, string Id)
        {
            try
            {
                //Eliminando cliente
                BERecurso oUsuario = new BERecurso();
                oUsuario.IdEmpresa = IdEmpresaSel;
                oUsuario.IdRecurso = Id;
                new BLRecurso().MantenerRecurso(4, oUsuario);

                return RedirectToAction("Index", "Contratista", new { area = "Registros", IdEmpresaSel = IdEmpresaSel, IdUsuarioWeb = Id, NombreEmpresaSel = NombreEmpresaSel, sMensaje = "Se ha Eliminado al vehiculo " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.EliminarVehiculo", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Contratista", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

        [Authorize]
        public ActionResult RequisitosVehiculo(string IdEmpresaSel, string sIdRecurso)
        {
            var model = new RecursoDetallesWebModel();
            model.IdEmpresaSel = IdEmpresaSel;
            model.IdRecursoSel = sIdRecurso;
            model.lRecursosRequisitos = new BLRecurso().ObtenerRequisitosRecurso(IdEmpresaSel, sIdRecurso);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RequisitosVehiculo(string IdEmpresaSel, string sIdRecurso,
            string btnQuitarReq, List<int> ChecksReq, List<string> IdsServicio, List<string> IdsRequisito)
        {
            try
            {
                if (!String.IsNullOrEmpty(btnQuitarReq))
                {
                    List<BERequisitoRecurso> lstReq = new List<BERequisitoRecurso>();
                    if (ChecksReq != null)
                    {
                        foreach (var nFila in ChecksReq)
                        {
                            lstReq.Add(new BERequisitoRecurso()
                            {
                                IdEmpresa = IdEmpresaSel,
                                IdRecurso = sIdRecurso,
                                IdTipoServicio = IdsServicio[nFila],
                                IdTipoDocumento = IdsRequisito[nFila],
                                DescripcionDocumento = "",
                                EsRequerido = false,
                                NecesitaAdjunto = false,
                                Orden = 0,
                                Maker = Session["IdUsuario"].ToString()
                            });
                        }
                        new BLRecurso().MantenerRequisitoRecurso(4, lstReq);
                        ViewBag.MensajeRequisito = "Se ha(n) Quitado el(os) requisito(s) en forma exitosa";
                    }
                }

                //
                var model = new RecursoDetallesWebModel();
                var _Requisito = new BLRequisito().Listar(IdEmpresaSel, "", "", "302", Session["IdUsuario"].ToString());
                model.IdEmpresaSel = IdEmpresaSel;
                model.IdRecursoSel = sIdRecurso;
                model.lRecursosRequisitos = new BLRecurso().ObtenerRequisitosRecurso(IdEmpresaSel, sIdRecurso);

                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "ContratistaController.RequisitosDetalle", "No se pudo quitar el(os) registro(s). Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                throw ex;
            }
        }

        #endregion

    }
}