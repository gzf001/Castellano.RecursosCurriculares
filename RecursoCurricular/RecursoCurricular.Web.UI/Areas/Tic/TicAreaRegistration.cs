using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Tic
{
    public class TicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                   name: "GetDimensiones",
                   url: "Tic/HabilidadTic/GetDimensiones",
                   defaults: new { area = "Tic", controller = "HabilidadTic", action = "GetDimensiones" }
               );

            context.MapRoute(
                   name: "AddDimension",
                   url: "Tic/HabilidadTic/AddDimension",
                   defaults: new { area = "Tic", controller = "HabilidadTic", action = "AddDimension" }
               );

            context.MapRoute(
                   name: "EditDimension",
                   url: "Tic/HabilidadTic/EditDimension/{id}",
                   defaults: new { area = "Tic", controller = "HabilidadTic", action = "EditDimension", id = "" }
               );

            context.MapRoute(
                   name: "DeleteDimension",
                   url: "Tic/HabilidadTic/DeleteDimension/{id}",
                   defaults: new { area = "Tic", controller = "HabilidadTic", action = "DeleteDimension", id = "" }
               );

            context.MapRoute(
                "Tic_default",
                "Tic/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}