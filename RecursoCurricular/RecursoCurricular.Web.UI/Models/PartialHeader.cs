using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Models
{
    public class PartialHeader
    {
        public PartialHeader(Guid id)
        {
            RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(id);

            RecursoCurricular.Membresia.Perfil perfil = RecursoCurricular.Membresia.Perfil.PerfilAnio;

            RecursoCurricular.Membresia.PerfilUsuario perfilUsuario = RecursoCurricular.Membresia.PerfilUsuario.Get(perfil, usuario);

            this.UserName = string.Format("{0} {1} {2}", usuario.Persona.Nombres, usuario.Persona.ApellidoPaterno, string.IsNullOrEmpty(usuario.Persona.ApellidoMaterno) ? usuario.Persona.ApellidoMaterno : string.Empty);

            this.Anio = perfilUsuario == null ? string.Empty : perfilUsuario.Valor;
        }

        private string userName;
        private string anio;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        [Display(Name = "Año:")]
        public string Anio
        {
            get
            {
                return anio;
            }
            set
            {
                anio = value;
            }
        }
    }
}