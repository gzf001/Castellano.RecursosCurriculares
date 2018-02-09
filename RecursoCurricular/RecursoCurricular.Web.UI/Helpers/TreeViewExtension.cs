using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Helpers
{
    public static class TreeViewExtension
    {
        public static MvcHtmlString TreeViewMenu(this HtmlHelper helper, string id)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", id);

            string fisicalPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.FilePath);

            fisicalPath = string.Format("{0}\\Areas", fisicalPath.Substring(0, fisicalPath.LastIndexOf(".UI") + 4));

            foreach (string directory in System.IO.Directory.GetDirectories(fisicalPath))
            {
                string area = directory.Remove(0, directory.LastIndexOf("\\") + 1);

                t.InnerHtml += string.Format("<li class='folder expanded'>{0}", area);

                t.InnerHtml += "<ul>";

                string targetDirectory = string.Format("{0}\\Controllers", directory);

                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(targetDirectory);

                foreach (System.IO.FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    string className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

                    string controlador = className.Replace("Controller", string.Empty);

                    t.InnerHtml += string.Format("<li class='folder expanded'>{0}", controlador);

                    t.InnerHtml += "<ul>";

                    string classFullName = string.Format("RecursoCurricular.Web.UI.Areas.{0}.Controllers.{1}", area, className);

                    Type type = Type.GetType(classFullName);

                    //Obtención de métodos que sea tipo GET
                    IEnumerable<System.Reflection.MethodInfo> getMethod = type.GetMethods().Where<System.Reflection.MethodInfo>(x => x.CustomAttributes.Any<System.Reflection.CustomAttributeData>(y => y.AttributeType.Equals(typeof(System.Web.Mvc.HttpGetAttribute))) && !x.GetParameters().Any<System.Reflection.ParameterInfo>());

                    foreach (System.Reflection.MethodInfo methodInfo in getMethod)
                    {
                        t.InnerHtml += string.Format("<li>/{0}/{1}/{2}", area, controlador, methodInfo.Name);
                    }

                    t.InnerHtml += "</ul></li>";
                }

                t.InnerHtml += "</ul></li>";
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Crea un control Tree View a partir de las habilidades generales y específicas
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controlId"></param>
        /// <param name="anioNumero"></param>
        /// <param name="tipoEducacionCodigo"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public static MvcHtmlString TreeViewHabilidades(this HtmlHelper helper, string controlId, int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", controlId);

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            foreach (RecursoCurricular.BaseCurricular.Habilidad habilidad in RecursoCurricular.BaseCurricular.Habilidad.GetAll(anio, grado.TipoEducacion, sector))
            {
                if (RecursoCurricular.BaseCurricular.SubHabilidad.Exists(habilidad, grado))
                {
                    t.InnerHtml += string.Format("<li class='folder' id='padre'>", string.Format("{0}.- {1}", habilidad.Numero, habilidad.Descripcion.Length > 70 ? string.Format("{0}...", habilidad.Descripcion.Substring(0, 70)) : habilidad.Descripcion));

                    t.InnerHtml += "<ul>";

                    foreach (RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad in RecursoCurricular.BaseCurricular.SubHabilidad.GetAll(habilidad, grado))
                    {
                        t.InnerHtml += string.Format("<li class='fancytree-title' id='{0}'>{1}", subHabilidad.Id, string.Format("{0}.- {1}", subHabilidad.Numero, subHabilidad.Descripcion));
                    }

                    t.InnerHtml += "</ul></li>";
                }
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Crea un control Tree View a partir de los ejes y objetivos de aprendizaje
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString TreeViewObjetivosAprendizaje(this HtmlHelper helper, string controlId, int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", controlId);

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            foreach (RecursoCurricular.BaseCurricular.Eje eje in RecursoCurricular.BaseCurricular.Eje.GetAll(anio, sector, grado.TipoEducacion))
            {
                if (RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Exists(grado, sector, eje))
                {
                    t.InnerHtml += string.Format("<li class='folder' id='padre'>{0}", string.Format("{0}.- {1}", eje.Numero, eje.Nombre));

                    t.InnerHtml += "<ul>";

                    foreach (RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje in RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.GetAll(grado, sector, eje))
                    {
                        t.InnerHtml += string.Format("<li class='fancytree-title' id='{0}'>{1}", objetivoAprendizaje.Id, string.Format("{0}.- {1}", objetivoAprendizaje.Numero, objetivoAprendizaje.Descripcion));
                    }

                    t.InnerHtml += "</ul></li>";
                }
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Crea un control Tree View a partir de las actitudes
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controlId"></param>
        /// <param name="anioNumero"></param>
        /// <param name="tipoEducacionCodigo"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public static MvcHtmlString TreeViewActitudes(this HtmlHelper helper, string controlId, int anioNumero, int tipoEducacionCodigo, Guid sectorId)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", controlId);

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            foreach (RecursoCurricular.BaseCurricular.Actitud actitud in RecursoCurricular.BaseCurricular.Actitud.GetAll(tipoEducacion, anio, sector))
            {
                t.InnerHtml += string.Format("<li id='{0}'>{1}</li>", actitud.Id, string.Format("{0}.- {1}", actitud.Numero, actitud.Descripcion));
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Crea un control Tree View a partir de los conocimientos
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controlId"></param>
        /// <param name="anioNumero"></param>
        /// <param name="tipoEducacionCodigo"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public static MvcHtmlString TreeViewConocimientos(this HtmlHelper helper, string controlId, int anioNumero, int tipoEducacionCodigo, Guid sectorId)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", controlId);

            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            foreach (RecursoCurricular.BaseCurricular.Conocimiento conocimiento in RecursoCurricular.BaseCurricular.Conocimiento.GetAll(tipoEducacion, anio, sector))
            {
                t.InnerHtml += string.Format("<li id='{0}'>{1}</li>", conocimiento.Id, string.Format("{0}.- {1}", conocimiento.Numero, conocimiento.Descripcion));
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}