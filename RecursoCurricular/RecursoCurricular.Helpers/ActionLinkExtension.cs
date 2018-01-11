using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RecursoCurricular.Helpers
{
    public static class ActionLinkExtension
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string text, string action, string controllerName, string area, TypeButton typeButton, object dataValue, string css = null, string faIcon = null, string toolTip = null, string onClick = null, Controller controller = null)
        {
            if (controller != null)
            {
                if (!RecursoCurricular.Helpers.ActionLinkExtension.ValidatePermission((controller as RecursoCurricular.Web.Controller), typeButton))
                {
                    return new MvcHtmlString("");
                }
            }

            TagBuilder t = new TagBuilder("a");

            if (!string.IsNullOrEmpty(css))
            {
                t.AddCssClass(css);
            }

            if (string.IsNullOrEmpty(action))
            {
                t.MergeAttribute("href", "#");
            }
            else
            {
                if (string.IsNullOrEmpty(area))
                {
                    t.MergeAttribute("href", string.Format("/{0}/{1}", "", action));
                }
                else
                {
                    t.MergeAttribute("href", string.Format("/{0}/{1}/{2}", area, controllerName, action));
                }
            }

            if (!object.ReferenceEquals(dataValue, null))
            {
                t.MergeAttribute("data-value", dataValue.ToString());
            }

            if (!(typeButton.Equals(RecursoCurricular.Helpers.TypeButton.Edit) || typeButton.Equals(RecursoCurricular.Helpers.TypeButton.Edit)) && !string.IsNullOrEmpty(faIcon))
            {
                t.InnerHtml = string.Format("<i class='fa {0}'>{1}</i>", faIcon, text);
            }

            if (!(typeButton.Equals(RecursoCurricular.Helpers.TypeButton.Edit) || typeButton.Equals(RecursoCurricular.Helpers.TypeButton.Edit)) && !string.IsNullOrEmpty(toolTip))
            {
                t.MergeAttribute("title", toolTip);
            }

            switch (typeButton)
            {
                case RecursoCurricular.Helpers.TypeButton.Add:
                    {
                        t.AddCssClass("btn btn-success btn-xs btn-flat");

                        t.MergeAttribute("title", "Agregar");

                        t.InnerHtml = string.Format("<i class='fa fa-plus'>{0}</i>", text);

                        break;
                    }

                case RecursoCurricular.Helpers.TypeButton.Edit:
                    {
                        t.AddCssClass("btn btn-primary btn-xs btn-flat");

                        t.MergeAttribute("title", "Editar");

                        t.InnerHtml = string.Format("<i class='fa fa-pencil'>{0}</i>", text);

                        break;
                    }
                case RecursoCurricular.Helpers.TypeButton.Delete:
                    {
                        t.AddCssClass("btn btn-danger btn-xs btn-flat");

                        t.MergeAttribute("title", "Eliminar");

                        t.InnerHtml = string.Format("<i class='fa fa-times'>{0}</i>", text);

                        break;
                    }
            }

            if (!string.IsNullOrEmpty(onClick))
            {
                t.MergeAttribute("onclick", onClick.ToString());
            }

            t.MergeAttribute("typeButton", typeButton.ToString());

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkCrudEmbedded(Guid id, Guid? parentId, RecursoCurricular.Helpers.TypeButton typeButton, RecursoCurricular.Web.Controller controller, string faIcon = null, string tooltip = null, string nameId = null)
        {
            if (!RecursoCurricular.Helpers.ActionLinkExtension.ValidatePermission(controller, typeButton))
            {
                return new MvcHtmlString("");
            }

            TagBuilder t = new TagBuilder("a");

            t.MergeAttribute("data-value", id.ToString());

            if (parentId.HasValue)
            {
                t.MergeAttribute("data-parent", parentId.Value.ToString());
            }

            if (!string.IsNullOrEmpty(faIcon))
            {
                t.InnerHtml = string.Format("<i class='fa {0}'></i>", faIcon);
            }

            if (!string.IsNullOrEmpty(tooltip))
            {
                t.MergeAttribute("title", tooltip);
            }

            if (!string.IsNullOrEmpty(nameId))
            {
                t.MergeAttribute("id", nameId);
            }

            switch (typeButton)
            {
                case RecursoCurricular.Helpers.TypeButton.Add:
                    {
                        t.AddCssClass("btn btn-success btn-xs btn-flat actionLinkCrudEmbedded");

                        t.MergeAttribute("data-modal", "form-primary");

                        t.MergeAttribute("href", "#");

                        if (string.IsNullOrEmpty(tooltip))
                        {
                            t.MergeAttribute("title", "Agregar");
                        }

                        if (string.IsNullOrEmpty(faIcon))
                        {
                            t.InnerHtml = "<i class='fa fa-plus'></i>";
                        }

                        break;
                    }
                case RecursoCurricular.Helpers.TypeButton.Edit:
                    {
                        t.AddCssClass("btn btn-primary btn-xs btn-flat actionLinkCrudEmbedded");

                        t.MergeAttribute("data-modal", "form-primary");

                        t.MergeAttribute("href", "#");

                        if (string.IsNullOrEmpty(tooltip))
                        {
                            t.MergeAttribute("title", "Editar");
                        }

                        if (string.IsNullOrEmpty(faIcon))
                        {
                            t.InnerHtml = "<i class='fa fa-pencil'></i>";
                        }

                        break;
                    }
                case RecursoCurricular.Helpers.TypeButton.Delete:
                    {
                        t.AddCssClass("btn btn-danger btn-xs actionLinkCrudEmbedded");

                        if (string.IsNullOrEmpty(tooltip))
                        {
                            t.MergeAttribute("title", "Eliminar");
                        }

                        if (string.IsNullOrEmpty(faIcon))
                        {
                            t.InnerHtml = "<i class='fa fa-times'></i>";
                        }

                        break;
                    }
                case RecursoCurricular.Helpers.TypeButton.OtherAction:
                    {
                        t.AddCssClass("btn btn-primary btn-xs btn-flat actionLinkCrudEmbedded");

                        break;
                    }
            }

            t.MergeAttribute("typeButton", typeButton.ToString());

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        private static bool ValidatePermission(RecursoCurricular.Web.Controller controller, RecursoCurricular.Helpers.TypeButton typeButton)
        {
            switch (typeButton)
            {
                case RecursoCurricular.Helpers.TypeButton.Accept:
                    {
                        #region Acceder

                        return RecursoCurricular.Membresia.RolAccion.Exists(controller.CurrentPersona, controller.CurrentMenuItem, RecursoCurricular.Membresia.Accion.Aceptar);

                        #endregion
                    }
                case RecursoCurricular.Helpers.TypeButton.Add:
                    {
                        #region Agregar

                        return RecursoCurricular.Membresia.RolAccion.Exists(controller.CurrentPersona, controller.CurrentMenuItem, RecursoCurricular.Membresia.Accion.Agregar);

                        #endregion
                    }
                case RecursoCurricular.Helpers.TypeButton.Edit:
                    {
                        #region Editar

                        return RecursoCurricular.Membresia.RolAccion.Exists(controller.CurrentPersona, controller.CurrentMenuItem, RecursoCurricular.Membresia.Accion.Editar);

                        #endregion
                    }
                case RecursoCurricular.Helpers.TypeButton.Delete:
                    {
                        #region Eliminar

                        return RecursoCurricular.Membresia.RolAccion.Exists(controller.CurrentPersona, controller.CurrentMenuItem, RecursoCurricular.Membresia.Accion.Eliminar);

                        #endregion
                    }
                case RecursoCurricular.Helpers.TypeButton.OtherAction:
                    {
                        #region Editar

                        return RecursoCurricular.Membresia.RolAccion.Exists(controller.CurrentPersona, controller.CurrentMenuItem, RecursoCurricular.Membresia.Accion.Editar);

                        #endregion
                    }
            }

            return true;
        }
    }
}