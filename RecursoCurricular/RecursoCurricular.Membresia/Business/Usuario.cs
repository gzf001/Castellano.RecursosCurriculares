using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Usuario
	{
		public static Usuario Get(Guid id)
		{
			return Query.GetUsuarios().SingleOrDefault<Usuario>(x => x.Id == id);
		}

		public static Usuario Get(int cuerpo, char digito)
		{
			return Query.GetUsuarios().SingleOrDefault<Usuario>(x => x.Persona.RunCuerpo == cuerpo && x.Persona.RunDigito.ToString().ToLower() == digito.ToString().ToLower());
		}

		public static List<Usuario> GetAll()
		{
			return
				(
				from query in Query.GetUsuarios()
				select query
				).ToList<Usuario>();
        }

        public static List<Usuario> GetAll(FindType findType, string filter)
        {
            return
                (
                from query in Query.GetUsuarios(findType, filter)
                orderby query.Persona.Nombre
                select query
                ).ToList<Usuario>();
        }

        public static List<Usuario> FindAll(FindType findType, string filter)
		{
			if (string.IsNullOrEmpty(filter)) return new List<Usuario>();
			
			return
				(
				from query in Query.GetUsuarios(findType, filter)
				orderby query.Persona.Nombre
				select query
				).ToList<Usuario>();
		}

		public void EncryptPassword()
		{
			using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
			{
				this.Password = Account.EncryptPassword(this.Password);

				this.UltimoCambioPassword = DateTime.Now;

				this.Save(context);

				context.SubmitChanges();
			}
		}

		public void UnLock()
		{
			Account.UnLock(this);
		}

		public void Lock()
		{
			Account.Lock(this);
		}

		public void DoPasswordRecovery()
		{
			PasswordRecoveryStatus passwordRecoveryStatus = Account.DoPasswordRecovery(this.Persona.RunCuerpo, this.Persona.RunDigito);

			if (passwordRecoveryStatus == PasswordRecoveryStatus.EmailNotRegistered)
			{
				throw new Exception("El usuario no registra email");
			}
		}

		public void DoChangePassword(string currentPassword, string password, string passwordConfirm)
		{
			ChangePasswordStatus changePassWordStatus = Account.DoChangePassword(this, currentPassword, password, passwordConfirm);

			if (changePassWordStatus == ChangePasswordStatus.SecurityPolitiesDeny)
			{
				throw new Exception("El cambio de contraseña no ha podido ser realizado");
			}
			else if (changePassWordStatus == ChangePasswordStatus.TooShortNewPassword)
			{
				throw new Exception("La nueva contraseña no cumple con el mínimo de 8 caracteres");
			}
			else if (changePassWordStatus == ChangePasswordStatus.WrongConfirmPassword)
			{
				throw new Exception("La nueva contraseña y su confirmación son diferentes");
			}
			else if (changePassWordStatus == ChangePasswordStatus.WrongCurrentPassword)
			{
				throw new Exception("La actual contraseña es erronea");
			}
		}
	}
}