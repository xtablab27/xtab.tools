using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace xTab.Tools.Helpers
{
    public static class CurrentCulture
    {
        public static String GetLocalValue(this Object Obj, String invariantPropertyName, String Prefix = "")
        {
            var propertyName = invariantPropertyName + Prefix + ColumnLocalPrefix;
            var property = Obj.GetType().GetProperty(propertyName);

            if (property != null && property.GetValue(Obj, null) != null)
            {
                return property.GetValue(Obj, null).ToString();
            }
            else
                return null;
        }

        public static PropertyInfo GetLocalProperty(this Object Obj, String invariantPropertyName, String Prefix = "")
        {
            var propertyName = invariantPropertyName + Prefix + ColumnLocalPrefix;
            var property = Obj.GetType().GetProperty(propertyName);

            if (property != null)
            {
                return property;
            }
            else
                return null;
        }

        public static String PropertyName(String invariantPropertyName, String Prefix = "")
        {
            return invariantPropertyName + Prefix + ColumnLocalPrefix;
        }

        public static String ColumnLocalPrefix
        {
            get
            {
                if (Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("ru"))
                    return "Ru";
                else if (Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("uk"))
                    return "Uk";
                else
                    return "En";
            }
        }

        public static String Name
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name;
            }
        }

        public static Boolean IsRU
        {
            get
            {
                return Name.ToLower().Contains("ru");
            }
        }

        public static Boolean IsUA
        {
            get
            {
                return Name.ToLower().Contains("uk");
            }
        }

        public static Boolean IsEN
        {
            get
            {
                return Name.ToLower().Contains("en");
            }
        }

        public static void RemoveLocalKeys(this ModelStateDictionary modelState, String removeCulture = "Ru")
        {
            var keys = modelState.Keys.Where(x => x.EndsWith(removeCulture)).ToList();

            foreach (var key in keys)
                modelState.Remove(key);
        }
    }
}
