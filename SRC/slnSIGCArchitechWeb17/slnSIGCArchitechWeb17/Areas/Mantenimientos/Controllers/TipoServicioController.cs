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
    public class TipoServicioController : Controller
    {
        // GET: Registros/TipoServicio
        [Authorize]
        public ActionResult Index(string IdTipoServicio, string DescripcionServicio, string sMensaje, string sError)
        {
            var model = new TipoServicioWebModel();
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (String.IsNullOrEmpty(IdTipoServicio)) IdTipoServicio = "";
            if (String.IsNullOrEmpty(DescripcionServicio)) DescripcionServicio = "";

            var _TipoServicio = new BLTipoServicio().Listar(IdTipoServicio, DescripcionServicio);
            model.lRegistrosTipoServicio = _TipoServicio;

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Editar(string Id)
        {
            var modelTipoServWebEditar = new TipoServicioWebModel();
            BETipoServicio TipoServiciosWebEditar = new BLTipoServicio().ObtenerTipoServicio(Id);
            modelTipoServWebEditar.NuevoRegistro = true;
            if (TipoServiciosWebEditar != null)
            {
                modelTipoServWebEditar.NuevoRegistro = false;
                modelTipoServWebEditar.IdTipoServicio = TipoServiciosWebEditar.IdTipoServicio;
                modelTipoServWebEditar.Descripcion = TipoServiciosWebEditar.Descripcion;
                modelTipoServWebEditar.FechaMaker = TipoServiciosWebEditar.FechaMaker;
                modelTipoServWebEditar.HoraMaker = TipoServiciosWebEditar.HoraMaker;
                modelTipoServWebEditar.Maker = TipoServiciosWebEditar.Maker;
            }
            return View(modelTipoServWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(TipoServicioWebModel model)
        {
            BETipoServicio oTipoServicio = new BETipoServicio();
            try
            {
                if (ModelState.IsValid)
                {
                    oTipoServicio.IdTipoServicio = model.IdTipoServicio == null ? "0" : model.IdTipoServicio;
                    oTipoServicio.Descripcion = model.Descripcion == null ? "" : model.Descripcion.Trim();
                    oTipoServicio.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                        new BLTipoServicio().MantenerTipoServicio(3, oTipoServicio);
                    else
                        new BLTipoServicio().MantenerTipoServicio(2, oTipoServicio);
                    return RedirectToAction("Index", "TipoServicio", new { area = "Mantenimientos", IdDato = model.IdTipoServicio, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Tipo de Servicio " + model.IdTipoServicio + " en forma exitosa" });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "TipoServicioController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Eliminar(string Id)
        {
            try
            {
                //Eliminando
                BETipoServicio oTipoServicio = new BETipoServicio();
                oTipoServicio.IdTipoServicio = Id;
                new BLTipoServicio().MantenerTipoServicio(4, oTipoServicio);
                return RedirectToAction("Index", "TipoServicio", new { area = "Mantenimientos", sMensaje = "Se ha Eliminado al Tipo de Servicio " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "TipoServicioController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "TipoServicio", new { area = "Mantenimientos", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

    }
}