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
                   name: "EjeNucleos",
                   url: "EducacionParvularia/Eje/Nucleos/{ambitoExperienciaAprendizajeCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "Nucleos", ambitoExperienciaAprendizajeCodigo = "" }
               );

            context.MapRoute(
                   name: "EjeCiclos",
                   url: "EducacionParvularia/Eje/Ciclos",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "Ciclos" }
               );

            context.MapRoute(
                   name: "GetEjes",
                   url: "EducacionParvularia/Eje/GetEjes/{ambitoExperienciaAprendizajeCodigo}/{nucleoId}/{cicloCodigo}",
                   defaults: new { area = "EducacionParvularia", controller = "Eje", action = "GetEjes" }
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