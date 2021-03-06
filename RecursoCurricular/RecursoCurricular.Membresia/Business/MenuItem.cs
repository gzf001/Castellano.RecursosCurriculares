using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecursoCurricular.Membresia
{
    public partial class MenuItem
    {
        private const int NIVELES = 6;

        #region Propiedades

        public bool RootNode
        {
            get
            {
                return RecursoCurricular.Membresia.MenuItem.GetAll(this).Any<RecursoCurricular.Membresia.MenuItem>();
            }
        }

        public string Numeracion
        {
            get
            {
                MenuItem aux = this;
                string nombre = this.Nombre;

                while (aux.Padre != null)
                {
                    int numero = 0;

                    foreach (MenuItem menuItem in MenuItem.GetAll(aux.Padre))
                    {
                        numero++;

                        if (aux.Id.Equals(menuItem.Id))
                        {
                            nombre = numero.ToString() + "." + nombre;

                            break;
                        }
                    }

                    aux = aux.Padre;
                }

                return nombre;
            }
        }

        public static MenuItem RolPermiso
        {
            get
            {
                return MenuItem.Get(new Guid("7858015F-A8E3-4E8A-BD1A-4D184AAEC4E7"), new Guid("192DDE33-DBCD-49BE-9D19-138C00F14BCF"));
            }
        }

        #endregion

        private List<MenuItem> hijos = new List<MenuItem>();

        public List<MenuItem> Hijos
        {
            get { return this.hijos; }
            set { this.hijos = value; }
        }

        public static bool Exists(MenuItem menuItem, Aplicacion aplicacion)
        {
            return Query.GetMenuItemes().Any<MenuItem>(x => x == menuItem && x.Aplicacion == aplicacion);
        }

        public static int Last(MenuItem menuItem)
        {
            IQueryable<RecursoCurricular.Membresia.MenuItem> menuItems = (from query in Query.GetMenuItemes()
                                                                     where query.Padre.Equals(menuItem)
                                                                     select query);

            if (menuItems.Any<RecursoCurricular.Membresia.MenuItem>())
            {
                return menuItems.Max<RecursoCurricular.Membresia.MenuItem>(x => x.Orden) + 1;
            }
            else
            {
                return 1;
            }
        }

        public static MenuItem Get(Guid aplicacionId, Guid id)
        {
            return Query.GetMenuItemes().SingleOrDefault<MenuItem>(x => x.AplicacionId == aplicacionId && x.Id == id);
        }

        public static MenuItem Get(string url)
        {
            return Query.GetMenuItemes().SingleOrDefault<MenuItem>(x => x.Url == url);
        }

        public static List<MenuItem> GetAll()
        {
            return
                (
                from query in Query.GetMenuItemes()
                select query
                ).ToList<MenuItem>();
        }

        public static List<MenuItem> GetAll(Aplicacion aplicacion)
        {
            return aplicacion == null ?
                    (
                    from menuItem in Query.GetMenuItemes()
                    orderby menuItem.Orden
                    select menuItem
                    ).ToList<MenuItem>()
                    :
                    (
                    from menuItem in Query.GetMenuItemes(aplicacion)
                    orderby menuItem.Orden
                    select menuItem
                    ).ToList<MenuItem>();
        }

        public static List<MenuItem> GetAll(Aplicacion aplicacion, Persona persona)
        {
            List<MenuItem> padres = new List<MenuItem>();

            List<MenuItem> result =
                (
                from query in Query.GetMenuItemes(aplicacion, persona)
                orderby query.Orden
                select query
                ).ToList<MenuItem>();

            foreach (MenuItem menuItem in result)
            {
                MenuItem padre = menuItem.Padre;

                while (padre != null && !padre.Equals(aplicacion.Inicio))
                {
                    if (!result.Contains<MenuItem>(padre) && !padres.Contains<MenuItem>(padre)) padres.Add(padre);

                    padre = padre.Padre;
                }
            }

            result.AddRange(padres);

            return result.OrderBy(x => x.Orden).ToList<MenuItem>();
        }

        public static List<MenuItem> GetAll(Aplicacion aplicacion, MenuItem padre)
        {
            if (aplicacion == null)
            {
                return padre == null ?
                        (
                        from menuItem in Query.GetMenuItemes()
                        where (menuItem.MenuItemId == null)
                        orderby menuItem.Orden
                        select menuItem
                        ).ToList<MenuItem>()
                        :
                        (
                        from menuItem in Query.GetMenuItemes()
                        where menuItem.MenuItemId == padre.Id
                        orderby menuItem.Orden
                        select menuItem
                        ).ToList<MenuItem>();
            }
            else
            {
                return padre == null ?
                        (
                        from menuItem in Query.GetMenuItemes(aplicacion)
                        where (menuItem.MenuItemId == null)
                        orderby menuItem.Orden
                        select menuItem
                        ).ToList<MenuItem>()
                        :
                        (
                        from menuItem in Query.GetMenuItemes(aplicacion)
                        where menuItem.MenuItemId == padre.Id
                        orderby menuItem.Orden
                        select menuItem
                        ).ToList<MenuItem>();
            }
        }

        public static List<MenuItem> GetAll(MenuItem parent)
        {
            return parent == null ?
                    (
                    from menuItem in Query.GetMenuItemes()
                    where (menuItem.MenuItemId == null)
                    orderby menuItem.Orden
                    select menuItem
                    ).ToList<MenuItem>()
                    :
                    (
                    from menuItem in Query.GetMenuItemes()
                    where menuItem.Padre == parent
                    orderby menuItem.Orden
                    select menuItem
                    ).ToList<MenuItem>();
        }

        public static List<MenuItem> GetTree(Aplicacion aplicacion, Persona persona)
        {
            return BuildTree(GetAll(aplicacion, persona));
        }

        private static List<MenuItem> BuildTree(List<MenuItem> list)
        {
            List<MenuItem> result = new List<MenuItem>();

            List<MenuItem> l = list.FindAll(x => x.MenuItemId.HasValue && list.FindAll(y => y.Id == x.MenuItemId.Value).Count == 0);

            foreach (MenuItem menuItem in l)
            {
                menuItem.Padre = null;
                menuItem.Hijos = new List<MenuItem>();

                BuildTree(list, menuItem);

                result.Add(menuItem);
            }

            return result;
        }

        private static void BuildTree(List<MenuItem> list, MenuItem parent)
        {
            foreach (MenuItem menuItem in list.FindAll(x => x.Padre == parent))
            {
                menuItem.Padre = parent;
                parent.Hijos.Add(menuItem);

                menuItem.Hijos = new List<MenuItem>();

                BuildTree(list, menuItem);
            }
        }

        public static void OrderMenu(string data)
        {
            List<MenuAuxiliar> menusauxiliares = new List<MenuAuxiliar>();

            JArray jsonArray = JsonConvert.DeserializeObject(data) as JArray;

            Guid menuItemId = (Guid)(jsonArray.First["id"]);

            RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.GetAplicacion(menuItemId);

            //Se rompe la relación  recursiva del padre con el hijo
            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                foreach (RecursoCurricular.Membresia.MenuItem menuItem in RecursoCurricular.Membresia.MenuItem.GetAll(aplicacion).Where<RecursoCurricular.Membresia.MenuItem>(x => !x.Equals(aplicacion.Inicio)))
                {
                    new RecursoCurricular.Membresia.MenuItem
                    {
                        AplicacionId = menuItem.AplicacionId,
                        Id = menuItem.Id,
                        MenuItemId = default(Guid),
                        Nombre = menuItem.Nombre,
                        Informacion = menuItem.Informacion,
                        Icono = menuItem.Icono,
                        Url = menuItem.Url,
                        Visible = menuItem.Visible,
                        Orden = menuItem.Orden
                    }.Save(context);
                }

                context.SubmitChanges();
            }

            //Se obtiene el orden y los nuevos padres e hijos desde el Json generado por el control
            foreach (JObject j in jsonArray)
            {
                menusauxiliares.Add(new MenuAuxiliar
                {
                    Padre = aplicacion.Inicio.Id,
                    Hijo = (Guid)j["id"]
                });

                JArray array = j["children"] as JArray;

                if (array != null)
                {
                    MenuItem.OrderMenu(array, (Guid)j["id"], menusauxiliares);
                }
            }

            int order = (aplicacion.Orden * 1000) + 1;

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                foreach (MenuAuxiliar menuAuxiliar in menusauxiliares)
                {
                    RecursoCurricular.Membresia.MenuItem menuItem = RecursoCurricular.Membresia.Query.GetMenuItemes().SingleOrDefault<RecursoCurricular.Membresia.MenuItem>(x => x.Id.Equals(menuAuxiliar.Hijo));

                    new RecursoCurricular.Membresia.MenuItem
                    {
                        AplicacionId = menuItem.AplicacionId,
                        Id = menuItem.Id,
                        MenuItemId = menuAuxiliar.Padre,
                        Nombre = menuItem.Nombre,
                        Informacion = menuItem.Informacion,
                        Icono = menuItem.Icono,
                        Url = menuItem.Url,
                        Visible = menuItem.Visible,
                        Orden = order
                    }.Save(context);

                    order++;
                }

                context.SubmitChanges();
            }
        }

        private static List<MenuAuxiliar> OrderMenu(JArray jsonArray, Guid padre, List<MenuAuxiliar> pruebas)
        {
            foreach (JObject j in jsonArray)
            {
                pruebas.Add(new MenuAuxiliar
                {
                    Padre = padre,
                    Hijo = (Guid)j["id"]
                });

                if (j["children"] != null)
                {
                    MenuItem.OrderMenu(j["children"] as JArray, (Guid)j["id"], pruebas);
                }
            }

            return pruebas;
        }

        private class MenuAuxiliar
        {
            public Guid Padre
            {
                get;
                set;
            }

            public Guid Hijo
            {
                get;
                set;
            }
        }
    }
}