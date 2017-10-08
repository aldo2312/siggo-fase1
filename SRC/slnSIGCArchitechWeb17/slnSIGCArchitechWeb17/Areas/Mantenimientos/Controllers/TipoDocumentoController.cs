using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

using slnSIGCArchitechWeb17.Models;
using slnSIGCArchitechWeb17.Areas.Mantenimientos.Models;

namespace slnSIGCArchitechWeb17.Areas.Mantenimientos.Controllers
{
    public class TipoDocumentoController : Controller
    {
        // GET: Mantenimientos/TipoDocumento
        [Authorize]
        public ActionResult Index(string IdDoc, string DescripcionTipoDoc, string Servicio, string sMensaje, string sError)
        {
            var model = new TipoDocumentoWebModel();
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (String.IsNullOrEmpty(IdDoc)) IdDoc = "";
            if (String.IsNullOrEmpty(DescripcionTipoDoc)) DescripcionTipoDoc = "";
            if (String.IsNullOrEmpty(Servicio)) Servicio = "";

            var _TipoServicios = new BLTipoServicio().Listar("", "");
            ViewBag.Servicio = new SelectList(_TipoServicios, "IdTipoServicio", "Descripcion");

            var _TipoDocumento = new BLTipoDocumento().Listar(IdDoc, DescripcionTipoDoc, Servicio);
            model.lRegistrosTipoDocumento = _TipoDocumento;

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Editar(string Id, string IdTipoServicio)
        {
            var TipoDocWebEditar = new TipoDocumentoWebModel();
            BETipoDocumento TipoDocumWebEditar = new BLTipoDocumento().ObtenerTipoDocumento(Id, IdTipoServicio);
            var _TipoServicioOrigen = new BLTipoServicio().Listar("", "");
            var _TipoRiesgo = new BLTipoDocumento().ObtenerTiposRiesgo();
            var _NivelRiesgo = new BLTipoDocumento().ObtenerNivelesRiesgo();

            TipoDocWebEditar.lTipoServicio = _TipoServicioOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdTipoServicio, Descripcion = x.Descripcion });
            TipoDocWebEditar.lTipoRiesgo = _TipoRiesgo.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            TipoDocWebEditar.lNivelRiesgo = _NivelRiesgo.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });

            TipoDocWebEditar.NuevoRegistro = true;
            if (TipoDocumWebEditar != null)
            {
                TipoDocWebEditar.NuevoRegistro = false;
                TipoDocWebEditar.IdTipoDocumento = TipoDocumWebEditar.IdTipoDocumento;
                TipoDocWebEditar.IdTipoServicio = TipoDocumWebEditar.IdTipoServicio;
                TipoDocWebEditar.Descripcion = TipoDocumWebEditar.Descripcion;
                TipoDocWebEditar.DescripcionLarga = TipoDocumWebEditar.DescripcionLarga;
                TipoDocWebEditar.IdNivelRiesgo = TipoDocumWebEditar.NivelRiesgo;
                TipoDocWebEditar.IdTipoRiesgo = TipoDocumWebEditar.IdTipoRiesgo;
                TipoDocWebEditar.FechaMaker = TipoDocumWebEditar.FechaMaker;
                TipoDocWebEditar.HoraMaker = TipoDocumWebEditar.HoraMaker;
                TipoDocWebEditar.Maker = TipoDocumWebEditar.Maker;
            }
            return View(TipoDocWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(TipoDocumentoWebModel model)
        {
            BETipoDocumento oTipoDocumento = new BETipoDocumento();
            var _TipoServicioOrigen = new BLTipoServicio().Listar("", "");
            var _TipoRiesgo = new BLTipoDocumento().ObtenerTiposRiesgo();
            var _NivelRiesgo = new BLTipoDocumento().ObtenerNivelesRiesgo();
            try
            {
                if (ModelState.IsValid)
                {
                    oTipoDocumento.IdTipoDocumento = model.IdTipoDocumento == null ? "0" : model.IdTipoDocumento;
                    oTipoDocumento.IdTipoServicio = model.IdTipoServicio;
                    oTipoDocumento.Descripcion = model.Descripcion == null ? "" : model.Descripcion.Trim();
                    oTipoDocumento.DescripcionLarga = model.DescripcionLarga == null ? "" : model.DescripcionLarga.Trim();
                    oTipoDocumento.IdNivelRiesgo = model.IdNivelRiesgo;
                    oTipoDocumento.IdTipoRiesgo = model.IdTipoRiesgo;
                    oTipoDocumento.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                        new BLTipoDocumento().MantenerTipoDocumento(3, oTipoDocumento);
                    else
                        new BLTipoDocumento().MantenerTipoDocumento(2, oTipoDocumento);
                    return RedirectToAction("Index", "TipoDocumento", new { area = "Mantenimientos", IdDoc = model.IdTipoDocumento, Servicio = model.IdTipoServicio, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Tipo de Documento " + model.IdTipoDocumento + " en forma exitosa" });
                }

                model.lTipoRiesgo = _TipoRiesgo.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lNivelRiesgo = _NivelRiesgo.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lTipoServicio = _TipoServicioOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdTipoServicio, Descripcion = x.Descripcion });
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "TipoDocumentoController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                model.lTipoRiesgo = _TipoRiesgo.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lNivelRiesgo = _NivelRiesgo.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                model.lTipoServicio = _TipoServicioOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdTipoServicio, Descripcion = x.Descripcion });

                return View(model);
            }
        }

        [Authorize]
        public ActionResult Eliminar(string Id, string IdTipoServicio)
        {
            try
            {
                //Eliminando
                BETipoDocumento oTipoDoc = new BETipoDocumento();
                oTipoDoc.IdTipoServicio = IdTipoServicio;
                oTipoDoc.IdTipoDocumento = Id;
                new BLTipoDocumento().MantenerTipoDocumento(4, oTipoDoc);
                return RedirectToAction("Index", "TipoDocumento", new { area = "Mantenimientos", sMensaje = "Se ha Eliminado al Tipo de Documento " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "TipoDocumentoController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "TipoDocumento", new { area = "Mantenimientos", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

    }
}