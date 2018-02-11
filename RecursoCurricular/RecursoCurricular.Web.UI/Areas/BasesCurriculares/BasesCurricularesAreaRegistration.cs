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

            context.MapRoute(
                   name: "DeleteObjetivoAprendizaje",
                   url: "BasesCurriculares/ObjetivoAprendizaje/DeleteObjetivoAprendizaje/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{Id}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizaje", action = "DeleteObjetivoAprendizaje" }
               );

            #endregion

            #region Dimension

            context.MapRoute(
                   name: "GetDimensionesOAT",
                   url: "BasesCurriculares/DimensionOAT/GetDimensionesOAT",
                   defaults: new { area = "BasesCurriculares", controller = "DimensionOAT", action = "GetDimensionesOAT" }
               );

            context.MapRoute(
                   name: "AddDimensionOAT",
                   url: "BasesCurriculares/DimensionOAT/AddDimensionOAT",
                   defaults: new { area = "BasesCurriculares", controller = "DimensionOAT", action = "AddDimensionOAT" }
               );

            context.MapRoute(
                   name: "EditDimensionOAT",
                   url: "BasesCurriculares/DimensionOAT/EditDimensionOAT/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "DimensionOAT", action = "EditDimensionOAT" }
               );

            context.MapRoute(
                   name: "DeleteDimensionOAT",
                   url: "BasesCurriculares/DimensionOAT/DeleteDimensionOAT/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "DimensionOAT", action = "DeleteDimensionOAT" }
               );

            #endregion

            #region ObjetivoAprendizajeTransversal

            context.MapRoute(
                   name: "GetObjetivosAprendizajeOAT",
                   url: "BasesCurriculares/ObjetivoAprendizajeOAT/GetObjetivosAprendizajeOAT/{dimensionId}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizajeOAT", action = "GetObjetivosAprendizajeOAT" }
               );

            context.MapRoute(
                   name: "AddObjetivoOAT",
                   url: "BasesCurriculares/ObjetivoAprendizajeOAT/AddObjetivoAprendizajeOAT/{dimensionId}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizajeOAT", action = "AddObjetivoAprendizajeOAT" }
               );

            context.MapRoute(
                   name: "EditObjetivoOAT",
                   url: "BasesCurriculares/ObjetivoAprendizajeOAT/EditObjetivoAprendizajeOAT/{dimensionId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizajeOAT", action = "EditObjetivoAprendizajeOAT" }
               );

            context.MapRoute(
                   name: "DeleteObjetivoOAT",
                   url: "BasesCurriculares/ObjetivoAprendizajeOAT/DeleteObjetivoAprendizajeOAT/{dimensionId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "ObjetivoAprendizajeOAT", action = "DeleteObjetivoAprendizajeOAT" }
               );

            #endregion

            #region Indicador

            context.MapRoute(
                   name: "GetObjetivoIndicadorBaseCurricular",
                   url: "BasesCurriculares/Indicador/GetIndicadores/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}",
                   defaults: new { area = "BasesCurriculares", controller = "Indicador", action = "GetObjetivos" }
               );

            context.MapRoute(
                   name: "GetIndicadoresBaseCurricular",
                   url: "BasesCurriculares/Indicador/GetIndicadores/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{objetivoAprendizajeId}",
                   defaults: new { area = "BasesCurriculares", controller = "Indicador", action = "GetIndicadores" }
               );

            context.MapRoute(
                   name: "SelectObjetivoAprendizaje",
                   url: "BasesCurriculares/Indicador/SelectObjetivo/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{objetivoAprendizajeId}",
                   defaults: new { area = "BasesCurriculares", controller = "Indicador", action = "SelectObjetivo" }
               );

            context.MapRoute(
                   name: "AddIndicadorBaseCurricular",
                   url: "BasesCurriculares/Indicador/AddIndicador/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{objetivoAprendizajeId}",
                   defaults: new { area = "BasesCurriculares", controller = "Indicador", action = "AddIndicador" }
               );

            context.MapRoute(
                   name: "EditIndicadorBaseCurricular",
                   url: "BasesCurriculares/Indicador/EditIndicador/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{objetivoAprendizajeId}/{Id}",
                   defaults: new { area = "BasesCurriculares", controller = "Indicador", action = "EditIndicador" }
               );

            context.MapRoute(
                   name: "DeleteIndicadorBaseCurricular",
                   url: "BasesCurriculares/Indicador/DeleteIndicador/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{ejeId}/{objetivoAprendizajeId}/{Id}",
                   defaults: new { area = "BasesCurriculares", controller = "Indicador", action = "DeleteIndicador" }
               );

            #endregion

            #region Habilidad

            context.MapRoute(
                   name: "GetHabilidadesBaseCurricular",
                   url: "BasesCurriculares/Habilidad/GetHabilidades/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Habilidad", action = "GetHabilidades" }
               );

            context.MapRoute(
                   name: "GetHabilidadesComboBasesCurriculares",
                   url: "BasesCurriculares/Home/Habilidades/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Home", action = "Habilidades" }
               );

            context.MapRoute(
                   name: "AddHabilidadBaseCurricular",
                   url: "BasesCurriculares/Habilidad/AddHabilidad/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Habilidad", action = "AddHabilidad" }
               );

            context.MapRoute(
                   name: "EditHabilidadBaseCurricular",
                   url: "BasesCurriculares/Habilidad/EditHabilidad/{tipoEducacionCodigo}/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Habilidad", action = "EditHabilidad" }
               );

            context.MapRoute(
                   name: "DeleteHabilidadBaseCurricular",
                   url: "BasesCurriculares/Habilidad/DeleteHabilidad/{tipoEducacionCodigo}/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Habilidad", action = "DeleteHabilidad" }
               );

            #endregion

            #region SubHabilidad

            context.MapRoute(
                   name: "GetSubHabilidad",
                   url: "BasesCurriculares/SubHabilidad/GetSubHabilidades/{tipoEducacionCodigo}/{gradoCodigo}/{habilidadId}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "SubHabilidad", action = "GetSubHabilidades" }
               );

            context.MapRoute(
                   name: "AddSubHabilidad",
                   url: "BasesCurriculares/SubHabilidad/AddSubHabilidad/{tipoEducacionCodigo}/{gradoCodigo}/{habilidadId}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "SubHabilidad", action = "AddSubHabilidad" }
               );

            context.MapRoute(
                   name: "EditSubHabilidad",
                   url: "BasesCurriculares/SubHabilidad/EditSubHabilidad/{tipoEducacionCodigo}/{gradoCodigo}/{habilidadId}/{sectorId}/{Id}",
                   defaults: new { area = "BasesCurriculares", controller = "SubHabilidad", action = "EditSubHabilidad" }
               );

            context.MapRoute(
                   name: "DeleteSubHabilidad",
                   url: "BasesCurriculares/SubHabilidad/DeleteSubHabilidad/{tipoEducacionCodigo}/{gradoCodigo}/{habilidadId}/{sectorId}/{Id}",
                   defaults: new { area = "BasesCurriculares", controller = "SubHabilidad", action = "DeleteSubHabilidad" }
               );

            #endregion

            #region Actitud

            context.MapRoute(
                   name: "GetActitudesBaseCurricular",
                   url: "BasesCurriculares/Actitud/GetActitudes/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Actitud", action = "GetActitudes" }
               );

            context.MapRoute(
                   name: "AddActitudBaseCurricular",
                   url: "BasesCurriculares/Actitud/AddActitud/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Actitud", action = "AddActitud" }
               );

            context.MapRoute(
                   name: "EditActitudBaseCurricular",
                   url: "BasesCurriculares/Actitud/EditActitud/{tipoEducacionCodigo}/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Actitud", action = "EditActitud" }
               );

            context.MapRoute(
                   name: "DeleteActitudBaseCurricular",
                   url: "BasesCurriculares/Actitud/DeleteActitud/{tipoEducacionCodigo}/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Actitud", action = "DeleteActitud" }
               );

            #endregion

            #region Conocimiento

            context.MapRoute(
                   name: "GetConocimientosBaseCurricular",
                   url: "BasesCurriculares/Conocimiento/GetConocimientos/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Conocimiento", action = "GetConocimientos" }
               );

            context.MapRoute(
                   name: "AddConocimientoBaseCurricular",
                   url: "BasesCurriculares/Conocimiento/AddConocimiento/{tipoEducacionCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Conocimiento", action = "AddConocimiento" }
               );

            context.MapRoute(
                   name: "EditConocimientoBaseCurricular",
                   url: "BasesCurriculares/Conocimiento/EditConocimiento/{tipoEducacionCodigo}/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Conocimiento", action = "EditConocimiento" }
               );

            context.MapRoute(
                   name: "DeleteConocimientoBaseCurricular",
                   url: "BasesCurriculares/Conocimiento/DeleteConocimiento/{tipoEducacionCodigo}/{sectorId}/{id}",
                   defaults: new { area = "BasesCurriculares", controller = "Conocimiento", action = "DeleteConocimiento" }
               );

            #endregion

            #region Unidad

            context.MapRoute(
                   name: "GetUnidadesBaseCurricular",
                   url: "BasesCurriculares/Unidad/GetUnidades/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "GetUnidades" }
               );

            context.MapRoute(
                   name: "GetAddUnidadBaseCurricular",
                   url: "BasesCurriculares/Unidad/AddUnidad/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "AddUnidad" }
               );

            context.MapRoute(
                   name: "GetEditUnidadBaseCurricular",
                   url: "BasesCurriculares/Unidad/EditUnidad/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}",
                   defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "EditUnidad" }
               );

            context.MapRoute(
                   name: "GetDeleteUnidadBaseCurricular",
                   url: "BasesCurriculares/Unidad/DeleteUnidad/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}",
                   defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "DeleteUnidad" }
               );

            context.MapRoute(
                   name: "GetUnidadHabilidadesBaseCurricular",
                   url: "BasesCurriculares/Unidad/GetHabilidades/{unidadId}/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "GetHabilidades" }
               );

            context.MapRoute(
                   name: "GetUnidadIndicadoresBaseCurricular",
                   url: "BasesCurriculares/Unidad/GetIndicadores/{unidadId}/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                   defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "GetIndicadores" }
               );

            context.MapRoute(
                  name: "GetUnidadActitudesBaseCurricular",
                  url: "BasesCurriculares/Unidad/GetActitudes/{unidadId}/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                  defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "GetActitudes" }
              );

            context.MapRoute(
                  name: "GetUnidadConocimientosBaseCurricular",
                  url: "BasesCurriculares/Unidad/GetConocimientos/{unidadId}/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                  defaults: new { area = "BasesCurriculares", controller = "Unidad", action = "GetConocimientos" }
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