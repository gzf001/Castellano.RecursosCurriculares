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
            #region Dimension

            context.MapRoute(
                   name: "GetDimensiones",
                   url: "Tic/Dimension/GetDimensiones",
                   defaults: new { area = "Tic", controller = "Dimension", action = "GetDimensiones" }
               );

            context.MapRoute(
                   name: "AddDimension",
                   url: "Tic/Dimension/AddDimension",
                   defaults: new { area = "Tic", controller = "Dimension", action = "AddDimension" }
               );

            context.MapRoute(
                   name: "EditDimension",
                   url: "Tic/Dimension/EditDimension/{id}",
                   defaults: new { area = "Tic", controller = "Dimension", action = "EditDimension", id = "" }
               );

            context.MapRoute(
                   name: "DeleteDimension",
                   url: "Tic/Dimension/DeleteDimension/{id}",
                   defaults: new { area = "Tic", controller = "Dimension", action = "DeleteDimension", id = "" }
               );

            #endregion

            #region Habilidad

            context.MapRoute(
                   name: "GetHabilidades",
                   url: "Tic/Habilidad/GetHabilidades/{dimensionId}",
                   defaults: new { area = "Tic", controller = "Habilidad", action = "GetHabilidades", dimensionId = "" }
               );

            context.MapRoute(
                   name: "AddHabilidad",
                   url: "Tic/Habilidad/AddHabilidad/{dimensionId}",
                   defaults: new { area = "Tic", controller = "Habilidad", action = "AddHabilidad", dimensionId = "" }
               );

            context.MapRoute(
                   name: "EditHabilidad",
                   url: "Tic/Habilidad/EditHabilidad/{dimensionId}/{id}",
                   defaults: new { area = "Tic", controller = "Habilidad", action = "EditHabilidad", dimensionId = "", id = "" }
               );

            context.MapRoute(
                   name: "DeleteHabilidad",
                   url: "Tic/Habilidad/DeleteHabilidad/{dimensionId}/{id}",
                   defaults: new { area = "Tic", controller = "Habilidad", action = "DeleteHabilidad", dimensionId = "", id = "" }
               );

            #endregion

            context.MapRoute(
                "Tic_default",
                "Tic/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}