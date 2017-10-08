using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;
using slnSIGCArchitechWeb17.Areas.Registros.Models;
using slnSIGCArchitechWeb17.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace slnSIGCArchitechWeb17.Areas.Registros.Controllers
{
    public class RequisitoController : Controller
    {
        // GET: Registros/Requisito
        [Authorize]
        public ActionResult Index(string IdEmpresaSel, string NombreEmpresaSel, string IdRequisito, string DescripcionRequisito, string TipoRecurso, string sMensaje, string sError)
        {
            var model = new RequisitoWebModel();

            if (String.IsNullOrEmpty(IdEmpresaSel)) IdEmpresaSel = "";
            if (String.IsNullOrEmpty(NombreEmpresaSel)) NombreEmpresaSel = "";
            if (String.IsNullOrEmpty(IdRequisito)) IdRequisito = "";
            if (String.IsNullOrEmpty(DescripcionRequisito)) DescripcionRequisito = "";
            if (String.IsNullOrEmpty(TipoRecurso)) TipoRecurso = "";
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;
            var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
            ViewBag.TipoRecurso = new SelectList(_TipoRecursos, "Codigo", "Descripcion");

            if (Session["TipoEmpresaSiggo"].ToString() != "CONTRATISTA")
            {
                model.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
                model.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            }
            else
            {
                model.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? Session["IdEmpresa"].ToString() : IdEmpresaSel;
                model.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? Session["NombreEmpresa"].ToString() : NombreEmpresaSel;
            }

            var _Empresas = new BLEmpresa().Listar("", "", "", "NN", Session["IdUsuario"].ToString());
            model.lEmpresas = _Empresas.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
            model.lRegistrosRequisitos = new BLRequisito().Listar(model.IdEmpresaSel, IdRequisito, DescripcionRequisito, TipoRecurso, Session["IdUsuario"].ToString());

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Editar(string Id, string IdTipoServicio, string IdEmpresaSel, string NombreEmpresaSel)
        {
            var reqWebEditar = new RequisitoWebModel();
            BERequisito reqWeb = new BLRequisito().ObtenerRequisito(IdEmpresaSel, Id, IdTipoServicio);
            var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
            var _Periodicidad = new BLRequisito().ObtenerPeriodicidad();
            var _TipoServicios = new BLTipoServicio().Listar("", "");
            reqWebEditar.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            reqWebEditar.lPeriodicidad = _Periodicidad.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            reqWebEditar.lTipoServicio = _TipoServicios.ConvertAll(x => new ComunModel { Codigo = x.IdTipoServicio, Descripcion = x.Descripcion });
            reqWebEditar.lAcciones = Helper.Acciones();
            reqWebEditar.NuevoRegistro = true;
            reqWebEditar.IdEmpresa = IdEmpresaSel;
            if (reqWeb != null)
            {
                reqWebEditar.NuevoRegistro = false;
                reqWebEditar.IdEmpresa = reqWeb.IdEmpresa;
                reqWebEditar.IdTipoServicio = reqWeb.IdTipoServicio;
                reqWebEditar.IdTipoDocumento = reqWeb.IdTipoDocumento;
                reqWebEditar.DescripcionRequisito = reqWeb.DescripcionRequisito;
                reqWebEditar.TipoRecurso = reqWeb.TipoRecurso;
                reqWebEditar.DiasNotificacion = reqWeb.DiasNotificacion;
                reqWebEditar.DiasTolerancia = reqWeb.DiasTolerancia;                
                reqWebEditar.Periodicidad = reqWeb.Periodicidad;
                reqWebEditar.HabilitaPago = reqWeb.HabilitaPago;
                reqWebEditar.HabilitaAcceso = reqWeb.HabilitaAcceso;
                reqWebEditar.DardeBaja = reqWeb.DardeBaja;
                if (reqWeb.FechaVigenciaDesde != null)
                    reqWebEditar.FecVigenciaDesde = Utilitario.GetFecha_DDMMYYYY((DateTime)reqWeb.FechaVigenciaDesde);
                if (reqWeb.FechaVigenciaHasta != null)
                    reqWebEditar.FecVigenciaHasta = Utilitario.GetFecha_DDMMYYYY((DateTime)reqWeb.FechaVigenciaHasta);
                reqWebEditar.EstadoDescripcion = reqWeb.EstadoDescripcion;
                reqWebEditar.FechaMaker = reqWeb.FechaMaker;
                reqWebEditar.HoraMaker = reqWeb.HoraMaker;
                reqWebEditar.Maker = reqWeb.Maker;
            }
            reqWebEditar.IdEmpresaSel = String.IsNullOrEmpty(IdEmpresaSel) ? "" : IdEmpresaSel;
            reqWebEditar.NombreEmpresaSel = String.IsNullOrEmpty(NombreEmpresaSel) ? "" : NombreEmpresaSel;
            return View(reqWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(RequisitoWebModel model)
        {
            BERequisito oRequisito = new BERequisito();
            var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
            var _Periodicidad = new BLRequisito().ObtenerPeriodicidad();
            var _TipoServicios = new BLTipoServicio().Listar("", "");
            try
            {
                if (ModelState.IsValid)
                {
                    oRequisito.IdEmpresa = model.IdEmpresa;
                    oRequisito.IdTipoServicio = model.IdTipoServicio.Trim();
                    oRequisito.IdTipoDocumento = model.IdTipoDocumento.Trim();
                    oRequisito.DescripcionRequisito = model.DescripcionRequisito;
                    oRequisito.TipoRecurso = model.TipoRecurso;
                    oRequisito.DiasNotificacion = model.DiasNotificacion;
                    oRequisito.DiasTolerancia = model.DiasTolerancia;
                    oRequisito.Periodicidad = model.Periodicidad;
                    oRequisito.HabilitaPago = model.HabilitaPago;
                    oRequisito.HabilitaAcceso = model.HabilitaAcceso;
                    oRequisito.DardeBaja = model.DardeBaja;
                    if (!String.IsNullOrEmpty(model.FecVigenciaDesde))
                        oRequisito.FechaVigenciaDesde = DateTime.ParseExact(model.FecVigenciaDesde, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (!String.IsNullOrEmpty(model.FecVigenciaHasta))
                        oRequisito.FechaVigenciaHasta = DateTime.ParseExact(model.FecVigenciaHasta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    oRequisito.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                        new BLRequisito().MantenerRequisito(3, oRequisito);
                    else
                        new BLRequisito().MantenerRequisito(2, oRequisito);
                    return RedirectToAction("Index", "Requisito", new { area = "Registros", IdEmpresaSel = model.IdEmpresa, NombreEmpresaSel = model.NombreEmpresaSel, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Cliente " + model.IdEmpresa + " en forma exitosa" });
                }
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresa) ? "" : model.IdEmpresa;
                model.NombreEmpresaSel = String.IsNullOrEmpty(model.NombreEmpresaSel) ? "" : model.NombreEmpresaSel;

                model.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lPeriodicidad = _Periodicidad.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lTipoServicio = _TipoServicios.ConvertAll(x => new ComunModel { Codigo = x.IdTipoServicio, Descripcion = x.Descripcion });
                model.lAcciones = Helper.Acciones();
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "RequisitoController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lPeriodicidad = _Periodicidad.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lTipoServicio = _TipoServicios.ConvertAll(x => new ComunModel { Codigo = x.IdTipoServicio, Descripcion = x.Descripcion });
                model.lAcciones = Helper.Acciones();
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Eliminar(string Id, string IdTipoServicio, string IdEmpresaSel)
        {
            try
            {
                //Eliminando requisito
                BERequisito oRequisito = new BERequisito();
                oRequisito.IdTipoDocumento = Id;
                oRequisito.IdTipoServicio = IdTipoServicio;
                oRequisito.IdEmpresa = IdEmpresaSel;
                new BLRequisito().MantenerRequisito(4, oRequisito);
                return RedirectToAction("Index", "Requisito", new { area = "Registros", sMensaje = "Se ha Eliminado el requisito " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "RequisitoController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Requisito", new { area = "Registros", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

        public JsonResult ObtenerTiposDocs(string IdTipoServicio)
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            string _Docums = "-1";
            if (string.IsNullOrEmpty(IdTipoServicio)) IdTipoServicio = "0";
            var lRequisitos = new BLTipoServicio().ObtenerTipoDocsServicio(IdTipoServicio);
            
            try
            {
                _Docums = jssObject.Serialize(lRequisitos);
            }
            catch (Exception)
            {
                return Json(_Docums);
            }
            return Json(_Docums);
        }


        [Authorize]
        public ActionResult RequisitosDato(string IdEmpresaSel, string IdTipoServicio, string IdRequisito)
        {
            var model = new RequisitoWebModel();
            model.IdEmpresa = IdEmpresaSel;
            model.IdTipoServicio = IdTipoServicio;
            model.IdTipoDocumento = IdRequisito;
            model.lRegistrosDatos = new BLRequisito().ObtenerRequisitosDato(IdEmpresaSel, IdTipoServicio, IdRequisito);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RequisitosDato(string IdEmpresaSel, string IdTipoServicio, string IdRequisito,
            string btnQuitarDato, List<int> ChecksDato, List<string> IdsDato)
        {
            try
            {
                if (!String.IsNullOrEmpty(btnQuitarDato))
                {
                    List<BERequisitoDato> lstReq = new List<BERequisitoDato>();
                    if (ChecksDato != null)
                    {
                        foreach (var nFila in ChecksDato)
                        {
                            lstReq.Add(new BERequisitoDato()
                            {
                                IdEmpresa = IdEmpresaSel,
                                IdDato = IdsDato[nFila],
                                IdTipoServicio = IdTipoServicio,
                                IdTipoDocumento = IdRequisito,
                                DescripcionDato = "",
                                Orden = 0,
                                Maker = Session["IdUsuario"].ToString()
                            });
                        }
                        new BLRequisito().MantenerRequisitoDato(4, lstReq);
                        ViewBag.MensajeRequisito = "Se ha(n) Quitado el(os) dato(s) en forma exitosa";
                    }
                }

                //
                var model = new RequisitoWebModel();
                model.IdEmpresaSel = IdEmpresaSel;
                model.IdTipoServicio = IdTipoServicio;
                model.IdTipoDocumento = IdRequisito;
                model.lRegistrosDatos = new BLRequisito().ObtenerRequisitosDato(IdEmpresaSel, IdTipoServicio, IdRequisito);

                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "RequisitoController.RequisitosDato", "No se pudo quitar el(os) registro(s). Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult GrabarDatos(RequisitoWebModel objRequisito)
        {
            bool status = false;
            List<BERequisitoDato> details = new List<BERequisitoDato>();
            if (ModelState.IsValid)
            {
                foreach (var i in objRequisito.lRegistrosDatos)
                {
                    details.Add(new BERequisitoDato()
                    {
                        IdEmpresa = i.IdEmpresa,
                        IdDato = i.IdDato,
                        IdTipoServicio = i.IdTipoServicio,
                        IdTipoDocumento = i.IdTipoDocumento,
                        DescripcionDato = i.DescripcionDato,
                        Orden = i.Orden,
                        Maker = Session["IdUsuario"].ToString()
                    });
                }
                new BLRequisito().MantenerRequisitoDato(3, details);
                status = true;
                //ViewBag.MensajeRequisito = "Se ha(n) Agregado el(os) requisito(s) en forma exitosa";
            }
            else
            {
                status = false;
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return new JsonResult { Data = new { status = status, MensajeRequisito = "Se ha(n) Agregado el(os) requisito(s) en forma exitosa" } };
        }

        public JsonResult ObtenerDatos()
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lDatos = new BLDato().Listar("", "");
            string _Datos = "-1";
            try
            {
                _Datos = jssObject.Serialize(lDatos);
            }
            catch (Exception)
            {
                return Json(_Datos);
            }
            return Json(_Datos);
        }
    }
}