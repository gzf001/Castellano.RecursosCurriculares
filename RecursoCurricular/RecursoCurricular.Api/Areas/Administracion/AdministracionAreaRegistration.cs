﻿using System.Web.Mvc;

namespace RecursoCurricular.Api.Areas.Administracion
{
    public class AdministracionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administracion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administracion_default",
                "Administracion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administracion_Token",
                "User/Tokens",
                new { area = "Administracion", controller = "User", action = "Tokens" }
            );
        }
    }
}