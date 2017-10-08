using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

using slnSIGCArchitechWeb17.Areas.Procesos.Models;


namespace slnSIGCArchitechWeb17.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var viewModel = new OrdenModel();
            string strIdEmpresa = "";
            string strIdUsuario = "";
            string strTipoEmpresaSiggo = "";

            if (!String.IsNullOrEmpty(Session["IdEmpresa"].ToString())) strIdEmpresa = Session["IdEmpresa"].ToString();
            if (!String.IsNullOrEmpty(Session["IdUsuario"].ToString())) strIdUsuario = Session["IdUsuario"].ToString();
            if (!String.IsNullOrEmpty(Session["TipoEmpresaSiggo"].ToString())) strTipoEmpresaSiggo = Session["TipoEmpresaSiggo"].ToString();
            if (!String.IsNullOrEmpty(Session["UsuarioInterno"].ToString())) strIdEmpresa = Session["UsuarioInterno"].ToString() == "Si" ? "" : strIdEmpresa;

            viewModel.lRegistrosOrdenes = new BLOrdenServicio().Listar(strIdEmpresa, strIdUsuario, strTipoEmpresaSiggo);

            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult ChangeCompany()
        {
            var _Empresas = new BLUsuarioWeb().ListarEmpresas(Session["IdUsuario"].ToString()) ;
            ViewBag.Empresas = new SelectList(_Empresas, "IdEmpresa", "RazonSocial");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeCompany(string Empresas)
        {
            if (!String.IsNullOrEmpty(Empresas))
            {
                BEEmpresa oEmpresa = new BLEmpresa().ObtenerEmpresa(Empresas);
                Session["IdEmpresa"] = oEmpresa.IdEmpresa;
                Session["NombreEmpresa"] = oEmpresa.RazonSocial;
                Session["TipoEmpresaSiggo"] = oEmpresa.TipoEmpresaSiggo;
                return RedirectToAction("Index", "Home");
            }
            var _Empresas = new BLUsuarioWeb().ListarEmpresas(User.Identity.Name);
            ViewBag.Empresas = new SelectList(_Empresas, "IdEmpresa", "RazonSocial");
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}