using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Models
{
    public class TokenValido : IValidate
    {
        public RecursoCurricular.Api.Models.Result ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new RecursoCurricular.Api.Models.Result { Status = "Error", Message = "Sin token" };
            }

            if (!RecursoCurricular.Helper.ValidateToken(token))
            {
                return new RecursoCurricular.Api.Models.Result { Status = "Error", Message = "Token inválido" };
            }

            return new RecursoCurricular.Api.Models.Result { Status = "OK", Message = "Token válido" };
        }
    }
}