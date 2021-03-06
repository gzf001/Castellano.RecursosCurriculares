using System;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class PerfilUsuario
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PerfilUsuario perfilUsuario = context.PerfilUsuarios.SingleOrDefault<PerfilUsuario>(x => x == this);

			if (perfilUsuario == null)
			{
				perfilUsuario = new PerfilUsuario
				{
					PerfilCodigo = this.PerfilCodigo,
					UsuarioId = this.UsuarioId
				};

				context.PerfilUsuarios.InsertOnSubmit(perfilUsuario);
			}

			perfilUsuario.Url = this.Url;
			perfilUsuario.Valor = this.Valor;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PerfilUsuario perfilUsuario = context.PerfilUsuarios.SingleOrDefault<PerfilUsuario>(x => x == this);

			if (perfilUsuario != null)
			{
				context.PerfilUsuarios.DeleteOnSubmit(perfilUsuario);
			}
			PostDelete(context);
		}
	}
}