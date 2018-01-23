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
                   name: "Ciclos",
                   url: "Educacion/Home/Ciclos",
                   defaults: new { area = "Educacion", controller = "Home", action = "Ciclos" }
               );

            context.MapRoute(
                   name: "Grados",
                   url: "Educacion/Home/Grados/{tipoEducacionCodigo}",
                   defaults: new { area = "Educacion", controller = "Home", action = "Grados" }
               );

            context.MapRoute(
                   name: "Sectores",
                   url: "Educacion/Home/Sectores",
                   defaults: new { area = "Educacion", controller = "Home", action = "Sectores" }
               );

            context.MapRoute(
                   name: "TiposEducacion",
                   url: "Educacion/Home/TiposEducacion",
                   defaults: new { area = "Educacion", controller = "Home", action = "TiposEducacion" }
               );

            context.MapRoute(
                "Educacion_default",
                "Educacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}