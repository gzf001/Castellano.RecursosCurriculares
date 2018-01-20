using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Educacion
{
    public class EducacionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Educacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Educacion_default",
                "Educacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                   name: "Ciclos",
                   url: "Educacion/Home/Ciclos",
                   defaults: new { area = "Educacion", controller = "Home", action = "Ciclos" }
               );
        }
    }
}