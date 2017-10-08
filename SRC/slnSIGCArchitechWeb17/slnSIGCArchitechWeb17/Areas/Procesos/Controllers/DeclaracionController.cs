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
using System.IO;

namespace slnSIGCArchitechWeb17.Areas.Procesos.Controllers
{
    public class DeclaracionController : Controller
    {
        // GET: Procesos/Declaracion
        [Authorize]
        public ActionResult Index(string Contratas, string Clientes, string NroOrden, string TipoRecurso, string EstadoRegistro, string MesControl, string AnhoControl, 
            string IdRegistroBusq, string sMensaje, string sError)
        {
            var viewModel = new DeclaracionModel();
            string strIdUsuario = "";

            if (String.IsNullOrEmpty(NroOrden)) NroOrden = "";
            if (String.IsNullOrEmpty(Clientes)) Clientes = "";
            if (String.IsNullOrEmpty(Contratas)) Contratas = "";
            if (String.IsNullOrEmpty(TipoRecurso)) TipoRecurso = "";
            if (String.IsNullOrEmpty(EstadoRegistro)) EstadoRegistro = "";
            if (String.IsNullOrEmpty(IdRegistroBusq)) IdRegistroBusq = "";

            if (String.IsNullOrEmpty(MesControl)) MesControl = Convert.ToString(DateTime.Now.Month);
            if (String.IsNullOrEmpty(AnhoControl)) AnhoControl = Convert.ToString(DateTime.Now.Year);

            if (!String.IsNullOrEmpty(Session["IdUsuario"].ToString())) strIdUsuario = Session["IdUsuario"].ToString();

            viewModel.lDeclaraciones = new BLDeclaracion().Listar(Contratas, Clientes, NroOrden, TipoRecurso, EstadoRegistro, Utilitario.Right("0" + MesControl, 2), AnhoControl, strIdUsuario, IdRegistroBusq);

            var _Contratas = new BLEmpresa().Listar("", "", "", "CONTRATISTA", Session["IdUsuario"].ToString());
            ViewBag.Contratas = new SelectList(_Contratas, "IdEmpresa", "RazonSocial", Contratas);
            var _Clientes = new BLEmpresa().Listar("", "", "", "CLIENTE", Utilitario.USUARIO_MASTER);
            ViewBag.Clientes = new SelectList(_Clientes, "IdEmpresa", "RazonSocial", Clientes);
            var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
            ViewBag.TipoRecurso = new SelectList(_TipoRecursos, "Codigo", "Descripcion");
            var _EstadosRegistro = new BLMaestro().ObtenerEstadosRegistro();
            ViewBag.EstadoRegistro = new SelectList(_EstadosRegistro, "Valor", "Descripcion");

            ViewBag.Mes = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month);
            ViewBag.Año = new SelectList(Helper.Llenar_Años(), DateTime.Now.Year.ToString());

            //ViewBag.NroOrden = new SelectList(Helper.Llenar_Años());

            ViewBag.Mensaje = sMensaje;
            ViewBag.Error = sError;

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return View(viewModel);
        }               

        [Authorize]
        public ActionResult DetalleDocumentos(string IdEmpresaSel, string IdRegistroSel)
        {
            var model = new DeclaracionDetalleModel();
            model.IdEmpresaSel = IdEmpresaSel;
            model.IdRegistroSel = IdRegistroSel;
            model.lDocumentos = new BLDeclaracion().Listar_Documentos(IdEmpresaSel, IdRegistroSel);

            foreach (vReg_Documento _Docum in model.lDocumentos)
            {
                _Docum.lDetalleDocumentos = new BLDeclaracion().Listar_DocumentosDatos(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
                _Docum.lImagenes = new BLDeclaracion().Listar_Imagenes(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
            }

            return View(model);
        }

        #region MantenerRegistro

        [Authorize]
        public ActionResult EditarRegistro(string IdRegistro, string IdEmpresa)
        {
            var registroWebEditar = new DeclaracionModel();
            vReg_Declaracion registro = new BLDeclaracion().Get_Registro(IdEmpresa, IdRegistro);

            #region Listas

            var _Contratas = new BLEmpresa().Listar("", "", "", "CONTRATISTA", Session["IdUsuario"].ToString());
            registroWebEditar.lContratas = _Contratas.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
            var _Clientes = new BLEmpresa().Listar("", "", "", "CLIENTE", Utilitario.USUARIO_MASTER);
            registroWebEditar.lClientes = _Clientes.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });

            registroWebEditar.lTipoDoi = Helper.TipoDocs();
            var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
            registroWebEditar.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
            var _Meses = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month).ToList();
            registroWebEditar.lMeses = _Meses.ConvertAll(x => new ComunModel { Codigo = x.Value, Descripcion = x.Text });
            var _Año = new SelectList(Helper.Llenar_Años(), DateTime.Now.Year.ToString()).ToList();
            registroWebEditar.lAnhos = _Año.ConvertAll(x => new ComunModel { Codigo = x.Text, Descripcion = x.Text });

            #endregion

            registroWebEditar.NuevoRegistro = true;
            if (registro != null)
            {
                registroWebEditar.NuevoRegistro = false;
                registroWebEditar.IdEmpresa = registro.IdEmpresa;
                registroWebEditar.IdRegistro = registro.IdRegistro;
                registroWebEditar.IdRecurso = registro.IdRecurso;
                registroWebEditar.IdCliente = registro.IdCliente;
                registroWebEditar.NroOrden = registro.NroOrden;
                registroWebEditar.TipoDocumentoReferencia = registro.TipoDocumentoReferencia;
                registroWebEditar.NroReferenciaRecurso = registro.NroReferenciaRecurso;
                registroWebEditar.DescripcionRecurso = registro.DescripcionRecurso;
                registroWebEditar.Descripcion001 = registro.Descripcion001;
                registroWebEditar.Descripcion002 = registro.Descripcion002;
                registroWebEditar.Descripcion003 = registro.Descripcion003;
                registroWebEditar.Observacion = registro.Observacion;
                registroWebEditar.Localidad = registro.Localidad;
                registroWebEditar.TipoRecurso = registro.TipoRecurso;
                registroWebEditar.FechaIngreso = registro.FechaIngreso;
                registroWebEditar.MesInformado = registro.MesInformado;
                registroWebEditar.AnhoInformado = registro.AnhoInformado;
                registroWebEditar.EstadoDescripcion = registro.EstadoDescripcion;
                registroWebEditar.FechaMaker = registro.FechaMaker;
                registroWebEditar.HoraMaker = registro.HoraMaker;
                registroWebEditar.Maker = registro.Maker;
            }
            return View(registroWebEditar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditarRegistro(DeclaracionModel model)
        {
            vReg_Declaracion oRecurso = new vReg_Declaracion();            

            try
            {
                if (ModelState.IsValid)
                {
                    oRecurso.IdEmpresa = model.IdEmpresa;
                    oRecurso.IdRegistro = model.IdRegistro;
                    oRecurso.IdRecurso = model.IdRecurso;
                    oRecurso.IdCliente = model.IdCliente;
                    oRecurso.NroOrden = model.NroOrden;
                    oRecurso.TipoDocumentoReferencia = model.TipoDocumentoReferencia;
                    oRecurso.NroReferenciaRecurso = model.NroReferenciaRecurso;
                    oRecurso.DescripcionRecurso = model.DescripcionRecurso;
                    oRecurso.Descripcion001 = model.Descripcion001;
                    oRecurso.Descripcion002 = model.Descripcion002;
                    oRecurso.Descripcion003 = model.Descripcion003;
                    oRecurso.Observacion = model.Observacion;
                    oRecurso.Localidad = model.Localidad;
                    oRecurso.TipoRecurso = model.TipoRecurso;
                    oRecurso.FechaIngreso = model.FechaIngreso;
                    oRecurso.MesInformado = model.MesInformado;
                    oRecurso.AnhoInformado = model.AnhoInformado;
                    oRecurso.EstadoDescripcion = model.EstadoDescripcion;
                    oRecurso.FechaMaker = model.FechaMaker;
                    oRecurso.HoraMaker = model.HoraMaker;
                    oRecurso.Maker = Session["IdUsuario"].ToString();

                    //if (model.NuevoRegistro)
                    //{
                    //    new BLRecurso().MantenerRecurso(3, oRecurso);
                    //}
                    //else
                    //    new BLRecurso().MantenerRecurso(2, oRecurso);

                    return RedirectToAction("Index", "Declaracion", new
                    {
                        area = "Procesos",
                        Contratas = model.IdEmpresa,
                        IdRegistro = model.IdRegistro,
                        sMensaje = "Se ha actualizado la información del Registro " + model.IdRegistro + " en forma exitosa"
                    });
                }

                #region Listas

                var _Contratas = new BLEmpresa().Listar("", "", "", "CONTRATISTA", Session["IdUsuario"].ToString());
                model.lContratas = _Contratas.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
                var _Clientes = new BLEmpresa().Listar("", "", "", "CLIENTE", Utilitario.USUARIO_MASTER);
                model.lClientes = _Clientes.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });

                model.lTipoDoi = Helper.TipoDocs();
                var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
                model.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                var _Meses = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month).ToList();
                model.lMeses = _Meses.ConvertAll(x => new ComunModel { Codigo = x.Value, Descripcion = x.Text });
                var _Año = new SelectList(Helper.Llenar_Años(), DateTime.Now.Year.ToString()).ToList();
                model.lAnhos = _Año.ConvertAll(x => new ComunModel { Codigo = x.Text, Descripcion = x.Text });

                #endregion

                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "DeclaracionController.EditarRegistro", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                #region Listas

                var _Contratas = new BLEmpresa().Listar("", "", "", "CONTRATISTA", Session["IdUsuario"].ToString());
                model.lContratas = _Contratas.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
                var _Clientes = new BLEmpresa().Listar("", "", "", "CLIENTE", Utilitario.USUARIO_MASTER);
                model.lClientes = _Clientes.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });

                model.lTipoDoi = Helper.TipoDocs();
                var _TipoRecursos = new BLRecurso().ObtenerTiposRecurso();
                model.lTipoRecurso = _TipoRecursos.ConvertAll(x => new ComunModel { Codigo = x.Codigo, Descripcion = x.Descripcion });
                var _Meses = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month).ToList();
                model.lMeses = _Meses.ConvertAll(x => new ComunModel { Codigo = x.Value, Descripcion = x.Text });
                var _Año = new SelectList(Helper.Llenar_Años(), DateTime.Now.Year.ToString()).ToList();
                model.lAnhos = _Año.ConvertAll(x => new ComunModel { Codigo = x.Text, Descripcion = x.Text });

                #endregion
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        #endregion


        #region Declarar

        [Authorize]
        public ActionResult RegistrarDeclaracion(string IdEmpresaSel, string IdRegistroSel, string NroSecuenciaSel)
        {
            var model = new DeclaracionDetalleModel();
            model.IdEmpresaSel = IdEmpresaSel;
            model.IdRegistroSel = IdRegistroSel;
            model.NroSecuenciaSel = NroSecuenciaSel;

            model.lDocumentos = new BLDeclaracion().Listar_Documentos(IdEmpresaSel, IdRegistroSel).Where(b => b.NroSecuencia.Equals(Convert.ToInt32(NroSecuenciaSel))).ToList();
            foreach (vReg_Documento _Docum in model.lDocumentos)
            {
                _Docum.lDetalleDocumentos = new BLDeclaracion().Listar_DocumentosDatos(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
                _Docum.lImagenes = new BLDeclaracion().Listar_Imagenes(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
            }
            if (model.lDocumentos != null)
            {
                model.FechaPresentacion = model.lDocumentos[0].FechaDocumento;
                model.Observacion = model.lDocumentos[0].Detalle1;
                
                model.lDatosAsociados = model.lDocumentos[0].lDetalleDocumentos.ConvertAll(x => new vDatosDetalle
                {
                    Codigo = Convert.ToString( x.IdDato),
                    DescripcionDato = x.DescripcionDato,
                    Categoria = x.CategoriaDato,
                    ValorActual =
                        (x.CategoriaDato.Equals(Helper.TiposDatos.Texto) ? x.ValorDatoTexto :
                            (x.CategoriaDato.Equals(Helper.TiposDatos.Numerico) ? x.ValorDatoEntero.ToString() :
                                (x.CategoriaDato.Equals(Helper.TiposDatos.Decimal) ? x.ValorDatoDecimal.ToString() :
                                    (x.CategoriaDato.Equals(Helper.TiposDatos.Fecha) ? x.ValorDatoFechaCadena : (x.ValorDatoBooleano ? "1" : "0"))))),
                    Situacion = x.EstadoDescripcion,
                    AccionRealizada = x.Accion
                });
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RegistrarDeclaracion(DeclaracionDetalleModel model)
        {
            vReg_Documento oDocumento = new vReg_Documento();           
            try
            {
                if (ModelState.IsValid)
                {
                    oDocumento.IdEmpresa = model.IdEmpresaSel == null ? "" : model.IdEmpresaSel;
                    oDocumento.IdRegistro = Convert.ToInt32(model.IdRegistroSel == null ? "0" : model.IdRegistroSel);
                    oDocumento.NroSecuencia = Convert.ToInt32(model.NroSecuenciaSel == null ? "0" : model.NroSecuenciaSel);
                    oDocumento.FechaDesde = !String.IsNullOrEmpty(model.FechaPresentacion) ? DateTime.ParseExact(model.FechaPresentacion, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
                    oDocumento.Detalle1 = model.Observacion == null ? "" : model.Observacion;    
                    
                    oDocumento.Maker = Session["IdUsuario"].ToString();
                    oDocumento.Accion = "U";
                    oDocumento.Estado = "P";

                    oDocumento.lDetalleDocumentos = new List<vReg_DocumentoDato>();

                    var DatoEmpresa = new BLEmpresa().ObtenerEmpresa(oDocumento.IdEmpresa);
                    Utilitario.EMPRESA_RUC = DatoEmpresa.RucEmpresa;

                    if (model.lDatosAsociados != null)
                    {
                        if (model.lDatosAsociados.Count() > 0)
                        {                            
                            foreach (var item in model.lDatosAsociados)
                            {
                                oDocumento.lDetalleDocumentos.Add(new vReg_DocumentoDato
                                {
                                    IdEmpresa = oDocumento.IdEmpresa,
                                    IdRegistro = oDocumento.IdRegistro,
                                    NroSecuencia = oDocumento.NroSecuencia,
                                    IdDato = Convert.ToInt32(item.Codigo),
                                    ValorDatoTexto = item.Categoria.Equals(Helper.TiposDatos.Texto) ? item.ValorActual : "",
                                    ValorDatoEntero = item.Categoria.Equals(Helper.TiposDatos.Numerico) ? Convert.ToInt32(item.ValorActual) : 0,
                                    ValorDatoDecimal = item.Categoria.Equals(Helper.TiposDatos.Decimal) ? Convert.ToDecimal(item.ValorActual) : 0,
                                    ValorDatoFecha = item.Categoria.Equals(Helper.TiposDatos.Fecha) ?
                                    DateTime.ParseExact(item.ValorActual, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now,
                                    ValorDatoBooleano = item.Categoria.Equals(Helper.TiposDatos.Booleano) ? Convert.ToBoolean(item.ValorActual) : false,
                                    Maker = oDocumento.Maker
                                });
                            }
                        }
                    }
                    //BLDeclaracion
                    //Se actualiza datos y asigna imagen
                    string sFileName = model.ImagenSel;
                    //string folder = sFileName.Substring(0, sFileName.LastIndexOf(("/")));
                    var fileName = sFileName.Substring(sFileName.LastIndexOf(("/")) + 1);
                    var rutaOrigenImagen = Path.Combine(Server.MapPath("~/Images"), fileName);
                    var rutaDestinoImagen = Path.Combine(Utilitario.fRutaImagenes(true), fileName);
                    
                    //Ejecutamos la grabacion
                    new BLDeclaracion().ProcesarDocumento(4, oDocumento, rutaOrigenImagen, rutaDestinoImagen);

                    return RedirectToAction("Index", "Declaracion", new { area = "Procesos", Contratas = model.IdEmpresaSel, IdRegistro = model.IdRegistroSel,
                        sMensaje = "Se ha actualizado la información del Recurso " + model.IdRegistroSel + " en forma exitosa" });
                }
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresaSel) ? "" : model.IdEmpresaSel;
                model.IdRegistroSel = String.IsNullOrEmpty(model.IdRegistroSel) ? "0" : model.IdRegistroSel;
                model.NroSecuenciaSel = String.IsNullOrEmpty(model.NroSecuenciaSel) ? "0" : model.NroSecuenciaSel;

                foreach (vReg_Documento _Docum in model.lDocumentos)
                {
                    _Docum.lDetalleDocumentos = new BLDeclaracion().Listar_DocumentosDatos(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
                    _Docum.lImagenes = new BLDeclaracion().Listar_Imagenes(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
                }

                if (model.lDocumentos != null)
                {
                    model.lDatosAsociados = model.lDocumentos[0].lDetalleDocumentos.ConvertAll(x => new vDatosDetalle
                    {
                        Codigo = Convert.ToString(x.IdDato),
                        DescripcionDato = x.DescripcionDato,
                        Categoria = x.CategoriaDato,
                        ValorActual =
                            (x.CategoriaDato.Equals(Helper.TiposDatos.Texto) ? x.ValorDatoTexto :
                                (x.CategoriaDato.Equals(Helper.TiposDatos.Numerico) ? x.ValorDatoEntero.ToString() :
                                    (x.CategoriaDato.Equals(Helper.TiposDatos.Decimal) ? x.ValorDatoDecimal.ToString() :
                                        (x.CategoriaDato.Equals(Helper.TiposDatos.Fecha) ? x.ValorDatoFechaCadena : (x.ValorDatoBooleano ? "1" : "0"))))),
                        Situacion = x.EstadoDescripcion,
                        AccionRealizada = x.Accion
                    });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                Log.EscribirLog(TipoLog.Resumido, ThreadSistema.APLICACIONSIGC, "",
                "DeclaracionController.RegistrarDeclaracion", "No se pudo grabar el registro. Error: " + ex.Message + ". " + ex.StackTrace, NivelMensajeLog.NINGUNO);
                model.IdEmpresaSel = String.IsNullOrEmpty(model.IdEmpresaSel) ? "" : model.IdEmpresaSel;
                model.IdRegistroSel = String.IsNullOrEmpty(model.IdRegistroSel) ? "0" : model.IdRegistroSel;
                model.NroSecuenciaSel = String.IsNullOrEmpty(model.NroSecuenciaSel) ? "0" : model.NroSecuenciaSel;
                foreach (vReg_Documento _Docum in model.lDocumentos)
                {
                    _Docum.lDetalleDocumentos = new BLDeclaracion().Listar_DocumentosDatos(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
                    _Docum.lImagenes = new BLDeclaracion().Listar_Imagenes(_Docum.IdEmpresa, _Docum.IdRegistro.ToString(), _Docum.NroSecuencia.ToString());
                }

                if (model.lDocumentos != null)
                {
                    model.lDatosAsociados = model.lDocumentos[0].lDetalleDocumentos.ConvertAll(x => new vDatosDetalle
                    {
                        Codigo = Convert.ToString(x.IdDato),
                        DescripcionDato = x.DescripcionDato,
                        Categoria = x.CategoriaDato,
                        ValorActual =
                            (x.CategoriaDato.Equals(Helper.TiposDatos.Texto) ? x.ValorDatoTexto :
                                (x.CategoriaDato.Equals(Helper.TiposDatos.Numerico) ? x.ValorDatoEntero.ToString() :
                                    (x.CategoriaDato.Equals(Helper.TiposDatos.Decimal) ? x.ValorDatoDecimal.ToString() :
                                        (x.CategoriaDato.Equals(Helper.TiposDatos.Fecha) ? x.ValorDatoFechaCadena : (x.ValorDatoBooleano ? "1" : "0"))))),
                        Situacion = x.EstadoDescripcion,
                        AccionRealizada = x.Accion
                    });
                }
                ViewBag.Error = "Lo sentimos, la transacción no ha sido completada.";
                return View(model);
            }
        }

        #endregion

        public JsonResult ObtenerOrdenes(string IdEmpresaSel)
        {
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            var lOrdenes = new BLDeclaracion().Listar_OrdenesPorCliente(IdEmpresaSel);
            string _Contratas = "-1";
            try
            {
                _Contratas = jssObject.Serialize(lOrdenes);
            }
            catch (Exception)
            {
                return Json(_Contratas);
            }
            return Json(_Contratas);
        }

        [Authorize]
        public ActionResult AbrirImagen(string IdEmpresaSel, string sIdImagen)
        {
            
            vReg_Imagen oImagen = new BLDeclaracion().Get_Imagen(IdEmpresaSel, sIdImagen);

            string sMesImagen = oImagen.MesImagen;
            string sAñoImagen = oImagen.AñoImagen;
            string sFileName = oImagen.NombreArchivo;
            var DatoEmpresa = new BLEmpresa().ObtenerEmpresa(IdEmpresaSel);
            Utilitario.EMPRESA_RUC = DatoEmpresa.RucEmpresa;

            //"\\\\10.50.92.141\\Imagenes\\" + Session["IdCliente"].ToString() + "\\" + sAñoImagen + "\\" + sMesImagen + "\\" + sIdImagen + ".pdf";
            string sRutaOrigen = Path.Combine(Utilitario.fRutaImagenes(false), string.Concat(sAñoImagen, "\\", sMesImagen, "\\", sFileName));

            if (System.IO.File.Exists(sRutaOrigen))
            {
                //return File(sRutaOrigen, "application/pdf", oImagen.NombreRazon.Replace("Ñ", "N") + "_Parte" + oImagen.NroArchivo.ToString() + ".pdf");
                byte[] fileBytes = System.IO.File.ReadAllBytes(sRutaOrigen);
                string extension = System.IO.Path.GetExtension(sRutaOrigen);
                string fileName = oImagen.NombreRazon.Replace("Ñ", "N") + "_Parte" + oImagen.NroArchivo.ToString() + extension;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            //using (new ImpersonateUser("imagenes", "ironmountain.com.pe", "ImPeru7114ooo"))
            //{
            //    if (System.IO.File.Exists(sRutaOrigen))
            //    {
            //        return File(sRutaOrigen, "application/pdf", oImagen.NombreRazon.Replace("Ñ", "N") + "_Parte" + oImagen.NroArchivo.ToString() + ".pdf");
            //    }
            //    else
            //    {
            //        return RedirectToAction("Error", "Home");
            //    }
            //}
        }

        //public FileResult Download()
        //{
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\folder\myfile.ext");
        //    string fileName = "myfile.ext";
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}
    }
}