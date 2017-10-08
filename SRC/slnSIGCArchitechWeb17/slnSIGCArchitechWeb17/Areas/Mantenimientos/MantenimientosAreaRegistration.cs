using System.Web.Mvc;

namespace slnSIGCArchitechWeb17.Areas.Mantenimientos
{
    public class MantenimientosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Mantenimientos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Mantenimientos_default",
                "Mantenimientos/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "slnSIGCArchitechWeb17.Areas.Mantenimientos.Controllers" }                
            );
        }
    }
}