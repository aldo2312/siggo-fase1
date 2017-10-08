using System.Web.Mvc;

namespace slnSIGCArchitechWeb17.Areas.Registros
{
    public class RegistrosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Registros";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Registros_default",
                "Registros/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}