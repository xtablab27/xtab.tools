using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using xTab.Tools.Attributes;

namespace xTab.Tools.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {

            string description = enumObj.ToString();

            FieldInfo fieldInfo = enumObj.GetType().GetField(description);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
                return attributes[0].Description;

            return description;
        }


        public static string GetLocalDescription<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {

            string description = enumObj.ToString();

            FieldInfo fieldInfo = enumObj.GetType().GetField(description);
            LocalDescriptionAttribute[] attributes = (LocalDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(LocalDescriptionAttribute), false);

            if (attributes.Any())
                return attributes[0].Description;
            else
                return enumObj.GetDescription();
        }

        public static List<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj, Object notSelectedValue = null, String notSelectedText = null)
           where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var list = (from TEnum e in Enum.GetValues(typeof(TEnum))
                        select new SelectListItem()
                        {
                            Value = e.ToString(),
                            Text = GetLocalDescription((TEnum)e)
                        }).ToList() ?? new List<SelectListItem>();

            if (notSelectedValue != null)
                list.Insert(0, new SelectListItem() { Value = notSelectedValue.ToString(), Text = notSelectedText });

            return list;
        }

        public static List<SelectListItem> ToIntSelectList<TEnum>(this TEnum enumObj, Object notSelectedValue = null, String notSelectedText = null)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var list = (from TEnum e in Enum.GetValues(typeof(TEnum))
                        select new SelectListItem()
                        {
                            Value = Convert.ToInt32(e).ToString(),
                            Text = GetLocalDescription((TEnum)e)
                        }).ToList();

            if (notSelectedValue != null)
                list.Insert(0, new SelectListItem() { Value = notSelectedValue.ToString(), Text = notSelectedText });

            return list;
        }
    }
}
