using System.Web.Mvc;

namespace RecursoCurricular.Api.Areas.BasesCurriculares
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
            context.MapRoute(
                "BasesCurriculares_default",
                "BasesCurriculares/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}