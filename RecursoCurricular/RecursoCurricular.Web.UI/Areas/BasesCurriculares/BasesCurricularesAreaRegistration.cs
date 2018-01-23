using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares
{
    public class BasesCurricularesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BasesCurriculares";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            #region Eje

            context.MapRoute(
                   name: "GetEjesBasesCurriculares",
                   url: "BasesCurriculares/Eje/GetEjes/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Eje", action = "GetEjes" }
               );

            context.MapRoute(
                   name: "GetEjesComboBasesCurriculares",
                   url: "BasesCurriculares/Home/Ejes/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Home", action = "Ejes" }
               );

            context.MapRoute(
                   name: "AddEjeBaseCurricular",
                   url: "BasesCurriculares/Eje/AddEje/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Eje", action = "AddEje" }
               );

            context.MapRoute(
                   name: "EditEjeBaseCurricular",
                   url: "BasesCurriculares/Eje/EditEje/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Eje", action = "EditEje" }
               );

            context.MapRoute(
                   name: "DeleteEjeBaseCurricular",
                   url: "BasesCurriculares/Eje/DeleteEje/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Eje", action = "DeleteEje" }
               );

            #endregion

            #region ObjetivoAprendizaje
            
            context.MapRoute(
                   name: "GetObjetivosAprendizaje",
                   url: "BasesCurriculares/ObjetivoAprendizaje/GetObjetivosAprendizaje/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizaje", action = "GetObjetivosAprendizaje" }
               );

            context.MapRoute(
                   name: "AddObjetivosAprendizaje",
                   url: "BasesCurriculares/ObjetivoAprendizaje/AddObjetivoAprendizaje/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizaje", action = "AddObjetivoAprendizaje" }
               );

            context.MapRoute(
                   name: "EditObjetivosAprendizaje",
                   url: "BasesCurriculares/ObjetivoAprendizaje/EditObjetivoAprendizaje/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{Id}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizaje", action = "EditObjetivoAprendizaje" }
               );

            #endregion

            context.MapRoute(
                "BasesCurriculares_default",
                "BasesCurriculares/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}