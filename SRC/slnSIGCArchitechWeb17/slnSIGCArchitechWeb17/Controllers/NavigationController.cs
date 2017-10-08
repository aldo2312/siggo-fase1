using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;
using slnSIGCArchitechWeb17.Models;

namespace slnSIGCArchitechWeb17.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        [Authorize]
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new TopMenuWebModel();
            var lOpciones = CargarOpcionesMenu(Session["IdUsuario"].ToString());
            model.lOpcionesMenu = lOpciones.ConvertAll(x => new TopMenuWebModel
            {
                IdMenu = x.IdMenu,
                PreMenu = x.PreMenu,
                DescripcionMenu = x.DescripcionMenu,
                Grupo = x.Grupo,
                ActionResult = x.Action,
                Controlador = x.Controller
            });
            if (Session["PwdCaducado"].ToString() == "SI")
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return PartialView(model);
        }
        [Authorize]
        private List<BEMenuSistema> CargarOpcionesMenu(string IdUsuario)
        {
            List<BEMenuSistema> lMenuPrincipal = new List<BEMenuSistema>();
            lMenuPrincipal = new BLSistemaWeb().ObtenerOpcionesMenu(IdUsuario);
            return lMenuPrincipal;
        }
    }
}