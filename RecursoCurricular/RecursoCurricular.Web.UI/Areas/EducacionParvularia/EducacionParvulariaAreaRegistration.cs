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
                "EducacionParvularia_default",
                "EducacionParvularia/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}