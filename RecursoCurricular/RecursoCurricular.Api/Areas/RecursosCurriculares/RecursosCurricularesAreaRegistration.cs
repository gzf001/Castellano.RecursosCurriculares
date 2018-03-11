using System.Web.Mvc;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares
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
        }
    }
}