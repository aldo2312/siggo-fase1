using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

using slnSIGCArchitechWeb17.Areas.Procesos.Models;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Areas.Procesos.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: Procesos/CargaMasiva
        [Authorize]
        public ActionResult Index(string sError, string sMensaje, string sRegistros)
        {
            var viewModel = new DataEntidadModel();

            ViewBag.Error = sError;
            ViewBag.Mensaje = sMensaje;
            ViewBag.Registros = sRegistros;

            var _Contratas = new BLEmpresa().Listar("", "", "", "CONTRATISTA", Session["IdUsuario"].ToString());
            viewModel.lContratas = _Contratas.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });
            var _Clientes = new BLEmpresa().Listar("", "", "", "CLIENTE", Utilitario.USUARIO_MASTER);
            viewModel.lClientes = _Clientes.ConvertAll(x => new ComunModel { Codigo = x.IdEmpresa, Descripcion = x.RazonSocial });

            var _Meses = new SelectList(Helper.Llenar_Meses(), "Codigo", "Descripcion", DateTime.Now.Month).ToList();
            viewModel.lMeses = _Meses.ConvertAll(x => new ComunModel { Codigo = x.Value, Descripcion = x.Text });
            var _Año = new SelectList(Helper.Llenar_Años(), DateTime.Now.Year.ToString()).ToList();
            viewModel.lAnhos = _Año.ConvertAll(x => new ComunModel { Codigo = x.Text, Descripcion = x.Text });

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(DataEntidadModel model, HttpPostedFileBase file)
        {
            //do container stuff
            var path = "";

            if (file == null)
            {
                return RedirectToAction("Index", "CargaMasiva", new { area = "Procesos", sError = "Debe seleccionar un Archivo", sRegistros = "" });
            }

            if (file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);

                //string strTipo = fileName.Substring(0, 4);
                //string strFormato = "";
                //if (Helper.Right(fileName, 4).ToUpper() != ".TXT")
                //{
                //    return RedirectToAction("Index", "CargarData", new { sError = "El tipo de Archivo debe ser TXT", sRegistros = "" });
                //}

                //if (!ArchivosPermitidos(strTipo))
                //{
                //    return RedirectToAction("Index", "CargarData", new { sError = "Seleccione un archivo correcto: LIN1 o DESP", sRegistros = "" });
                //}

                //DateTime dFechaArchivo;
                //try
                //{
                //    switch (strTipo)
                //    {
                //        case "LIN1":
                //        case "LIN2":
                //            strFormato = strTipo + "YYMMDD";
                //            dFechaArchivo = Convert.ToDateTime(fileName.Substring(8, 2) + "/" + fileName.Substring(6, 2) + "/" + fileName.Substring(4, 2));
                //            break;
                //        case "SALP":
                //        case "DESP":
                //            strFormato = strTipo + "DDMM";
                //            dFechaArchivo = Convert.ToDateTime(fileName.Substring(4, 2) + "/" + fileName.Substring(6, 2) + "/" + DateTime.Now.Year.ToString());
                //            break;
                //        default:
                //            strFormato = "";
                //            dFechaArchivo = Convert.ToDateTime("01/01/1900");
                //            break;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return RedirectToAction("Index", "CargarData", new
                //    {
                //        sError = "El formato del Archivo debe ser " + strFormato + ". Verificar",
                //        sRegistros = ""
                //    });
                //}


                try
                {
                    //if (oBL_DataBbva.Validando_Fecha(strTipo, fileName))
                    //{
                    //    return RedirectToAction("Index", "CargarData", new { sError = "Ya se ha cargado el archivo " + Path.GetFileName(file.FileName) + ". Verificar", sRegistros = "" });
                    //}

                    ////string webCurrentDirectory = HttpRuntime.AppDomainAppPath;
                    string webCurrentDirectory = @"C:\Tempo\";
                    string webCurrentDirectory_combine = Path.Combine(webCurrentDirectory, "tmp.txt");
                    FileInfo oFile = new FileInfo(webCurrentDirectory_combine);

                    //using (new ImpersonateUser("administrador", "ironmountain.com.pe", "iron09439280"))
                    //{
                    //    if (oFile.Exists)
                    //    {
                    //        oFile.Delete();
                    //    }
                    file.SaveAs(webCurrentDirectory_combine);

                    //    oBL_DataBbva.Cargar_DataTmp(strTipo, Helper.Importar(webCurrentDirectory_combine, fileName, "txt"));

                    //    oBL_DataBbva.Actualizar_Data(strTipo, fileName, User.Identity.Name);

                    //    oBL_DataBbva.Cargar_Data(strTipo, fileName);
                    //}

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "CargarData", new { sError = ex.Message, sRegistros = "" });
                    //"No se completó la operación. Comuníquese con IT de IMP."
                }
            }

            return RedirectToAction("Index", new { sMensaje = "Se ha cargado el Archivo " + Path.GetFileName(file.FileName) + " en forma Exitosa." });

        }

        private bool ArchivosPermitidos(string strFile)
        {
            bool bPermitido = false;
            switch (strFile)
            {
                case "LIN1":
                case "LIN2":
                case "DESP":
                case "SALP":
                    bPermitido = true;
                    break;
            }
            return bPermitido;
        }

    }
}