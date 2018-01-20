using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia
{
    public class EducacionParvulariaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "EducacionParvularia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                   name: "GetSelectorNucleos",
                   url: "EducacionParvularia/Home/Nucleos/{ambitoExperienciaAprendizajeCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Home", action = "Nucleos", ambitoExperienciaAprendizajeCodigo = "" }
               );

            context.MapRoute(
                   name: "EjesParvulo",
                   url: "EducacionParvularia/Home/GetEjes/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Home", action = "GetEjes" }
               );

            #region NucleoAprendizaje

            context.MapRoute(
                   name: "GetNucleos",
                   url: "EducacionParvularia/Nucleo/GetNucleos/{ambitoExperienciaAprendizajeCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Nucleo", action = "GetNucleos", ambitoExperienciaAprendizajeCodigo = "" }
               );

            context.MapRoute(
                   name: "AddNucleo",
                   url: "EducacionParvularia/Nucleo/AddNucleo/{ambitoExperienciaAprendizajeCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Nucleo", action = "AddNucleo", ambitoExperienciaAprendizajeCodigo = "" }
               );

            context.MapRoute(
                   name: "EditNucleo",
                   url: "EducacionParvularia/Nucleo/EditNucleo/{ambitoExperienciaAprendizajeCodigo}/{id}",
                   defaults: new { area = "EducacionParvularia", controller = "Nucleo", action = "EditNucleo", ambitoExperienciaAprendizajeCodigo = "", id = "" }
               );

            context.MapRoute(
                   name: "DeleteNucleo",
                   url: "EducacionParvularia/Nucleo/DeleteNucleo/{ambitoExperienciaAprendizajeCodigo}/{id}",
                   defaults: new { area = "EducacionParvularia", controller = "Nucleo", action = "DeleteNucleo", ambitoExperienciaAprendizajeCodigo = "", id = "" }
               );

            #endregion

            #region Eje

            context.MapRoute(
                   name: "GetEjesParvulo",
                   url: "EducacionParvularia/Eje/GetEjes/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "GetEjes" }
               );

            context.MapRoute(
                   name: "AddEjeParvulo",
                   url: "EducacionParvularia/Eje/AddEje/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "AddEje" }
               );

            context.MapRoute(
                   name: "EditEjeParvulo",
                   url: "EducacionParvularia/Eje/EditEje/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}/{id}",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "EditEje" }
               );

            context.MapRoute(
                   name: "DeleteEjeParvulo",
                   url: "EducacionParvularia/Eje/DeleteEje/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}/{id}",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "DeleteEje" }
               );

            #endregion

            #region AprendizajeEsperado

            context.MapRoute(
                   name: "GetAprendizajeEsperadoParvulo",
                   url: "EducacionParvularia/AprendizajeEsperado/GetAprendizajesEsperados/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "AprendizajeEsperado", action = "GetAprendizajesEsperados" }
               );

            context.MapRoute(
                   name: "AddAprendizajeEsperadoParvulo",
                   url: "EducacionParvularia/AprendizajeEsperado/AddAprendizajeEsperado/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "AprendizajeEsperado", action = "AddAprendizajeEsperado" }
               );

            context.MapRoute(
                   name: "NumeroAprendizajeEsperadoParvulo",
                   url: "EducacionParvularia/AprendizajeEsperado/NumeroAprendizajeEsperado/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}/{ejeId}",
                   defaults: new { area = "EducacionParvularia", controller = "AprendizajeEsperado", action = "NumeroAprendizajeEsperado" }
               );

            context.MapRoute(
                   name: "EditAprendizajeEsperadoParvulo",
                   url: "EducacionParvularia/AprendizajeEsperado/EditAprendizajeEsperado/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}/{id}",
                   defaults: new { area = "EducacionParvularia", controller = "AprendizajeEsperado", action = "EditAprendizajeEsperado" }
               );

            context.MapRoute(
                   name: "DeleteAprendizajeEsperadoParvulo",
                   url: "EducacionParvularia/AprendizajeEsperado/DeleteAprendizajeEsperado/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}/{id}",
                   defaults: new { area = "EducacionParvularia", controller = "AprendizajeEsperado", action = "DeleteAprendizajeEsperado" }
               );
            #endregion

            context.MapRoute(
                "EducacionParvularia_default",
                "EducacionParvularia/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}