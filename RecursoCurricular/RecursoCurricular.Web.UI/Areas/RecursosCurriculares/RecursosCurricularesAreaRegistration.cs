using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares
{
    public class RecursosCurricularesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RecursosCurriculares";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RecursosCurriculares_default",
                "RecursosCurriculares/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            #region Eje

            context.MapRoute(
                   name: "GetEjesRecursosCurriculares",
                   url: "RecursosCurriculares/Eje/GetEjes/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "GetEjes" }
               );

            context.MapRoute(
                   name: "GetEjesComboRecursosCurriculares",
                   url: "RecursosCurriculares/Home/Ejes/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Home", action = "Ejes" }
               );

            context.MapRoute(
                   name: "AddEjeRecursoCurricular",
                   url: "RecursosCurriculares/Eje/AddEje/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "AddEje" }
               );

            context.MapRoute(
                   name: "EditEjeRecursoCurricular",
                   url: "RecursosCurriculares/Eje/EditEje/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "EditEje" }
               );

            context.MapRoute(
                   name: "DeleteEjeRecursoCurricular",
                   url: "RecursosCurriculares/Eje/DeleteEje/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Eje", action = "DeleteEje" }
               );

            #endregion

            #region Contenido

            context.MapRoute(
                   name: "GetContenidos",
                   url: "RecursosCurriculares/Contenido/GetContenidos/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "GetContenidos" }
               );

            context.MapRoute(
                   name: "AddContenido",
                   url: "RecursosCurriculares/Contenido/AddContenido/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "AddContenido" }
               );

            context.MapRoute(
                   name: "EditContenido",
                   url: "RecursosCurriculares/Contenido/EditContenido/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{Id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "EditContenido" }
               );

            context.MapRoute(
                   name: "DeleteContenido",
                   url: "RecursosCurriculares/Contenido/DeleteContenido/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{Id}",
                   defaults: new { area = "RecursosCurriculares", controller = "Contenido", action = "DeleteContenido" }
               );

            #endregion

            #region ObjetivoVertical

            context.MapRoute(
                   name: "GetObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/GetObjetivosVerticales/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "GetObjetivosVerticales" }
               );

            context.MapRoute(
                   name: "AddObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/AddObjetivoVertical/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "AddObjetivoVertical" }
               );

            context.MapRoute(
                   name: "EditObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/EditObjetivoVertical/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "EditObjetivoVertical" }
               );

            context.MapRoute(
                   name: "DeleteObjetivoVertical",
                   url: "RecursosCurriculares/ObjetivoVertical/DeleteObjetivoVertical/{tipoEducacionCodigo}/{GradoCodigo}/{sectorId}/{id}",
                   defaults: new { area = "RecursosCurriculares", controller = "ObjetivoVertical", action = "DeleteObjetivoVertical" }
               );

            #endregion
        }
    }
}