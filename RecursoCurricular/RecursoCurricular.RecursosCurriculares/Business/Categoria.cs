using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.RecursosCurriculares
{
    public partial class Categoria : RecursoCurricular.Default
    {
        public static Categoria Recordar
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(1);
            }
        }

        public static Categoria Comprender
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(2);
            }
        }

        public static Categoria Aplicar
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(3);
            }
        }

        public static Categoria Conocer
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(4);
            }
        }

        public static Categoria Sintetizar
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(5);
            }
        }

        public static Categoria Analizar
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(6);
            }
        }

        public static Categoria Evaluar
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(7);
            }
        }

        public static Categoria Crear
        {
            get
            {
                return RecursoCurricular.RecursosCurriculares.Categoria.Get(8);
            }
        }

        public static IEnumerable<SelectListItem> Categorias
        {
            get
            {
                SelectList lista = new SelectList(RecursoCurricular.RecursosCurriculares.Categoria.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Categoria Get(int codigo)
        {
            return Query.GetCategorias().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Categoria>(x => x.Codigo.Equals(codigo));
        }

        public static List<Categoria> GetAll()
        {
            return
                (
                from query in Query.GetCategorias()
                select query
                ).ToList<Categoria>();
        }
    }
}