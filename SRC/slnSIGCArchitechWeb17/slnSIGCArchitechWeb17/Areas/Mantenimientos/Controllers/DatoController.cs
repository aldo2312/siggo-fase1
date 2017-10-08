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
    public class DatoController : Controller
    {
        // GET: Registros/Dato
        [Authorize]
        public ActionResult Index(string IdDato, string DescDato, string sMensaje, string sError)
        {
            var model = new DatoWebModel();
            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (String.IsNullOrEmpty(IdDato)) IdDato = "";
            if (String.IsNullOrEmpty(DescDato)) DescDato = "";

            var _Datos = new BLDato().Listar(IdDato, DescDato);
            model.lRegistrosDatos = _Datos;

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Editar(string Id, string TipoEmpresaSiggo)
        {
            var modelDato = new DatoWebModel();
            BEDato datosWebEditar = new BLDato().ObtenerDato(Id);
            var _datoOrigen = new BLDato().Listar("", "");
            modelDato.NuevoRegistro = true;
            modelDato.lCategoria = Helper.TipoDato();
            if (datosWebEditar != null)
            {
                modelDato.NuevoRegistro = false;
                modelDato.IdDato = datosWebEditar.IdDato;
                modelDato.Descripcion = datosWebEditar.Descripcion;
                modelDato.Alias = datosWebEditar.Alias;
                modelDato.Categoria = datosWebEditar.Categoria;
                modelDato.FechaMaker = datosWebEditar.FechaMaker;
                modelDato.HoraMaker = datosWebEditar.HoraMaker;
                modelDato.Maker = datosWebEditar.Maker;
            }
            return View(modelDato);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(DatoWebModel model)
        {
            BEDato oDato = new BEDato();
            try
            {
                if (ModelState.IsValid)
                {
                    oDato.IdDato = model.IdDato == null ? "0" : model.IdDato;
                    oDato.Descripcion = model.Descripcion == null ? "" : model.Descripcion.Trim();
                    oDato.Alias = model.Alias == null ? "" : model.Alias.Trim();
                    oDato.Categoria = model.Categoria == null ? "" : model.Categoria.Trim();
                    oDato.Maker = Session["IdUsuario"].ToString();

                    if (model.NuevoRegistro)
                        new BLDato().MantenerDato(3, oDato);
                    else
                        new BLDato().MantenerDato(2, oDato);
                    return RedirectToAction("Index", "Dato", new { area = "Mantenimientos", IdDato = model.IdDato, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado el ") + " Dato " + model.IdDato + " en forma exitosa" });
                }

                model.lCategoria = Helper.TipoDato();
                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "DatoController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                model.lCategoria = Helper.TipoDato();
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Eliminar(string Id)
        {
            try
            {
                //Eliminando
                BEDato oDato = new BEDato();
                oDato.IdDato = Id;
                new BLDato().MantenerDato(4, oDato);
                return RedirectToAction("Index", "Dato", new { area = "Mantenimientos", sMensaje = "Se ha Eliminado al dato " + Id + " en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "DatoController.Editar", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "Dato", new { area = "Mantenimientos", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

    }
}