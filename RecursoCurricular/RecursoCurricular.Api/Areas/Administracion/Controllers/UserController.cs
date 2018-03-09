using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecursoCurricular.Api.Areas.Administracion.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public string Token([FromBody] RecursoCurricular.Api.Areas.Administracion.Models.Usuario usuario)
        {
            return RecursoCurricular.Helper.GenerateToken(usuario.Run, usuario.Id);
        }
    }
}
