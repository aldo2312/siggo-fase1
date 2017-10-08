using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

using slnSIGCArchitechWeb17.Areas.Procesos.Models;
using slnSIGCArchitechWeb17.Models;


namespace slnSIGCArchitechWeb17.Areas.Procesos.Controllers
{
    public class OrdenServicioController : Controller
    {
        static string fechaDesde = "";
        static string fechaHasta = "";

        // GET: Procesos/OrdenServicio
        [Authorize]
        public ActionResult Index(string NroOrden, string Clientes, string Contratas, string fDesde, string fHasta, string sMensaje, string sError)
        {
            var viewModel = new OrdenModel();
            string strIdUsuario = "";

            if (String.IsNullOrEmpty(NroOrden)) NroOrden = "";
            if (String.IsNullOrEmpty(Clientes)) Clientes = "";
            if (String.IsNullOrEmpty(Contratas)) Contratas = "";

            if (String.IsNullOrEmpty(fDesde)) fDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            if (String.IsNullOrEmpty(fHasta)) fHasta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("dd/MM/yyyy");

            if (!String.IsNullOrEmpty(Session["IdUsuario"].ToString())) strIdUsuario = Session["IdUsuario"].ToString();

            DateTime dtDesde = Convert.ToDateTime(DateTime.ParseExact(fDesde, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString());
            DateTime dtHasta = Convert.ToDateTime(DateTime.ParseExact(fHasta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString());

            viewModel.lRegistrosOrdenes = new BLOrdenServicio().Listar(NroOrden, Contratas, Clientes, dtDesde, dtHasta, strIdUsuario);

            var _Contratas = new BLEmpresa().Listar("", "", "", "CONTRATISTA", Session["IdUsuario"].ToString());
            ViewBag.Contratas = new SelectList(_Contratas, "IdEmpresa", "RazonSocial", Contratas);
            var _Clientes = new BLEmpresa().Listar("", "", "", "CLIENTE", Session["IdUsuario"].ToString());
            ViewBag.Clientes = new SelectList(_Clientes, "IdEmpresa", "RazonSocial", Clientes);

            viewModel.fDesde = Utilitario.GetFecha_DDMMYYYY(dtDesde);
            viewModel.fHasta = Utilitario.GetFecha_DDMMYYYY(dtHasta);
            fechaDesde = Utilitario.GetFecha_DDMMYYYY(dtDesde);
            fechaHasta = Utilitario.GetFecha_DDMMYYYY(dtHasta);

            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult EditarOrden(string IdEmpresa, string NroOrden, string Accion)
        {
            var osWebEditar = new OrdenModel();
            BEOrdenServicio ordenWebEditar = new BLOrdenServicio().ObtenerOS(IdEmpresa, NroOrden);
            var _EmpresaOrigen = new BLEmpresa().Listar("", "", "", "CLIENTE", Utilitario.USUARIO_MASTER);
            osWebEditar.lEmpresas = _EmpresaOrigen.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
            osWebEditar.NuevoRegistro = true;
            osWebEditar.AccionRealizada = Accion;
            if (ordenWebEditar != null)
            {
                osWebEditar.NuevoRegistro = false;
                osWebEditar.IdEmpresa = ordenWebEditar.IdEmpresa;
                osWebEditar.NroOrden = ordenWebEditar.NroOrden;
                osWebEditar.Descripcion = ordenWebEditar.Descripcion;
                osWebEditar.FecRealInicio = Utilitario.GetFecha_DDMMYYYY((DateTime)ordenWebEditar.FechaRealInicio);
                osWebEditar.FecRealFin = Utilitario.GetFecha_DDMMYYYY((DateTime)ordenWebEditar.FechaRealFin);
                osWebEditar.FecControlInicio = Utilitario.GetFecha_DDMMYYYY((DateTime)ordenWebEditar.FechaControlInicio);
                osWebEditar.FecControlFin = Utilitario.GetFecha_DDMMYYYY((DateTime)ordenWebEditar.FechaControlFin);

                osWebEditar.lRegistrosAreas = new BLOrdenServicio().ObtenerAreas(ordenWebEditar.IdEmpresa, ordenWebEditar.NroOrden);
                osWebEditar.lRegistrosSedes = new BLOrdenServicio().ObtenerSedes(ordenWebEditar.IdEmpresa, ordenWebEditar.NroOrden);
                osWebEditar.lRegistrosContratas = new BLOrdenServicio().ObtenerContratistas(ordenWebEditar.IdEmpresa, ordenWebEditar.NroOrden);
            }

            return View(osWebEditar);
        }

        [Authorize]
        [HttpPost]
        public JsonResult EditarOrden(OrdenModel model)
        {
            bool status = false;
            //string redirectUrl = "";
            List<BEOrdenServicioAreas> lstAreas = new List<BEOrdenServicioAreas>();
            List<BEOrdenServicioLocalizaciones> lstLocalizaciones = new List<BEOrdenServicioLocalizaciones>();
            List<BEOrdenServicioContratas> lstContratas = new List<BEOrdenServicioContratas>();
            try
            {
                if (ModelState.IsValid)
                {
                    BEOrdenServicio order = new BEOrdenServicio
                    {
                        IdEmpresa = model.IdEmpresa,
                        NroOrden = model.NroOrden,
                        Descripcion = model.Descripcion,
                        FechaRealInicio = !String.IsNullOrEmpty(model.FecRealInicio) ? DateTime.ParseExact(model.FecRealInicio, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null,
                        FechaRealFin = !String.IsNullOrEmpty(model.FecRealFin) ? DateTime.ParseExact(model.FecRealFin, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null,
                        FechaControlInicio = !String.IsNullOrEmpty(model.FecControlInicio) ? DateTime.ParseExact(model.FecControlInicio, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null,
                        FechaControlFin = !String.IsNullOrEmpty(model.FecControlFin) ? DateTime.ParseExact(model.FecControlFin, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null,
                        Maker = Session["IdUsuario"].ToString()
                    };

                    string nroOrdenGenerado = "";
                    if (model.NuevoRegistro)
                        new BLOrdenServicio().MantenerOrdenServicio(3, order, out nroOrdenGenerado);
                    else
                        new BLOrdenServicio().MantenerOrdenServicio(2, order, out nroOrdenGenerado);

                    foreach (var d1 in model.lRegistrosAreas)
                        lstAreas.Add(new BEOrdenServicioAreas() { IdEmpresa = model.IdEmpresa, NroOrden = nroOrdenGenerado, CodigoDpto = d1.CodigoDpto, DescripcionDpto = d1.DescripcionDpto });

                    foreach (var d2 in model.lRegistrosSedes)
                        lstLocalizaciones.Add(new BEOrdenServicioLocalizaciones() { IdEmpresa = model.IdEmpresa, NroOrden = nroOrdenGenerado, CodigoSede = d2.CodigoSede, DescripcionSede = d2.DescripcionSede });

                    foreach (var d3 in model.lRegistrosContratas)
                        lstContratas.Add(new BEOrdenServicioContratas() { IdEmpresa = model.IdEmpresa, NroOrden = nroOrdenGenerado, IdContrata = d3.IdContrata, IdSubContrata = d3.IdSubContrata });

                    new BLOrdenServicio().MantenerOrdenServicioAreas(3, lstAreas);
                    new BLOrdenServicio().MantenerOrdenServicioLocalizaciones(3, lstLocalizaciones);
                    new BLOrdenServicio().MantenerOrdenServicioContratas(3, lstContratas);

                    status = true;
                    //redirectUrl = Url.Action("Index", "OrdenServicio", new { area = "Procesos", NroOrden = model.NroOrden, sMensaje = "Se ha " + (model.NuevoRegistro ? "Creado al " : "Modificado la ") + " Orden " + model.NroOrden + " en forma exitosa" });
                }
                else
                {
                    status = false;
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                }
                return new JsonResult { Data = new { status = status } };
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "OrdenServicioController.Editar", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                status = false;
                return new JsonResult { Data = new { status = status } };
            }
        }

        public JsonResult ObtenerSedes()
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lSedes = new BLOrdenServicio().ObtenerSedes();
            string _Sedes = "-1";
            try
            {
                _Sedes = jssObject.Serialize(lSedes);
            }
            catch (Exception)
            {
                return Json(_Sedes);
            }
            return Json(_Sedes);
        }

        public JsonResult ObtenerAreas()
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lAreas = new BLOrdenServicio().ObtenerAreas();
            string _Areas = "-1";
            try
            {
                _Areas = jssObject.Serialize(lAreas);
            }
            catch (Exception)
            {
                return Json(_Areas);
            }
            return Json(_Areas);
        }

        public JsonResult ObtenerContratas(string IdEmpresaSel)
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lContratas = new BLOrdenServicio().ObtenerContratas(IdEmpresaSel, Session["IdUsuario"].ToString());
            string _Contratas = "-1";
            try
            {
                _Contratas = jssObject.Serialize(lContratas);
            }
            catch (Exception)
            {
                return Json(_Contratas);
            }
            return Json(_Contratas);
        }

        [Authorize]
        public ActionResult DetallePeriodos(string NroOrden, string Cliente, string Contratista)
        {
            var model = new OrdenModel();
            model.ClienteSel = Cliente;
            model.NroOrdenSel = NroOrden;
            model.ContratistaSel = Contratista;
            model.fDesde = fechaDesde;
            model.fHasta = fechaHasta;
            model.lRegistrosPeriodos = new BLOrdenServicio().ListarPeriodos(NroOrden, Contratista, Cliente, "", "");

            ViewBag.Mes = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month);
            ViewBag.Año = new SelectList(Helper.Llenar_Años(), DateTime.Now.Year.ToString());

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DetallePeriodos(string NroOrden, string Cliente, string Contratista,
            string btnEliminarPeriodo, List<int> ChecksReg, List<string> Ids)
        {
            try
            {
                if (!String.IsNullOrEmpty(btnEliminarPeriodo))
                {
                    List<BEOrdenServicioPeriodos> lstReq = new List<BEOrdenServicioPeriodos>();
                    if (ChecksReg != null)
                    {
                        foreach (var nFila in ChecksReg)
                        {
                            lstReq.Add(new BEOrdenServicioPeriodos()
                            {
                                Cliente = Cliente,
                                NroOrden = NroOrden,
                                Contratista = Contratista,
                                Mes = Ids[nFila].Substring(0, 2),
                                Anho = Ids[nFila].Substring(2, 4)
                            });
                        }

                        int nResultado = new BLOrdenServicio().QuitarPeriodoDeclaracion(lstReq);
                        ViewBag.MensajePeriodo = "Se ha(n) Quitado el(os) periodo(s) en forma exitosa";
                    }
                }

                //
                var model = new OrdenModel();
                model.ClienteSel = Cliente;
                model.NroOrdenSel = NroOrden;
                model.ContratistaSel = Contratista;
                model.fDesde = fechaDesde;
                model.fHasta = fechaHasta;
                model.lRegistrosPeriodos = new BLOrdenServicio().ListarPeriodos(NroOrden, Contratista, Cliente, "", "");

                ViewBag.Mes = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month);
                ViewBag.Año = new SelectList(Helper.Llenar_Años());

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
        public ActionResult GenerarPeriodo(string IdEmpresaSel, string NroOrden, string MesControl, string AnhoControl)
        {
            try
            {
                if (new BLRequisito().Listar(IdEmpresaSel, "", "", "", Session["IdUsuario"].ToString()).Count() <= 0)
                {
                    return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", sError = "El contratista seleccionado no tiene MATRIZ DE SERVICIO Pre-Cargado. Verificar" });
                }

                if (new BLRecurso().Listar(IdEmpresaSel, "", "", "").Count() <= 0)
                {
                    return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", sError = "El contratista seleccionado no tiene RECURSOS Pre-Cargados. Verificar" });
                }

                if (new BLOrdenServicio().ExistePeriodoDeclaracion(IdEmpresaSel, NroOrden, MesControl, AnhoControl))
                {
                    return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", sError = "El periodo seleccionado ya ha sido generado anteriormente. Verificar" });
                }

                //Generar Periodo
                new BLOrdenServicio().GenerarPeriodoDeclaracion(IdEmpresaSel, NroOrden, Utilitario.Right("00" + MesControl, 2), AnhoControl, Session["IdUsuario"].ToString());
                return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", NroOrden = NroOrden, Contratas = IdEmpresaSel, fDesde = fechaDesde, fHasta = fechaHasta, sMensaje = "Se ha generado el periodo [" + string.Concat(MesControl, "-", AnhoControl) + "] para la Orden " + NroOrden + " del Contrata [" + IdEmpresaSel + "] en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "OrdenServicioController.GenerarPeriodo", "No se pudo eliminar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }

        [Authorize]
        public ActionResult Procesar(string NroOrden, string Clientes, string Contratas, string MesControl, string AnhoControl, int Publicar)
        {
            try
            {
                //validaciones

                new BLOrdenServicio().ProcesarPeriodoDeclaracion(NroOrden, Contratas, Clientes, MesControl, AnhoControl, Publicar == 1 ? true : false);
                return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", NroOrden = NroOrden, Clientes = Clientes, Contratas = Contratas, sMensaje = "Se ha Publicado al Periodo Seleccionado en forma exitosa" });
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "OrdenServicioController.Procesar", "No se pudo procesar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                return RedirectToAction("Index", "OrdenServicio", new { area = "Procesos", sError = "Lo sentimos, la transacción no ha sido completada." });
            }
        }


    }
}