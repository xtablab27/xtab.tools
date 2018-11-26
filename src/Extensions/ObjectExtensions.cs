using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Caching;
using xTab.Tools.Helpers;

namespace xTab.Tools.Extensions
{
    public static class ObjectExtensions
    {
        public static String GetLocalValue(this Object obj, String invariantPropertyName)
        {
            var propertyName = CurrentCulture.PropertyName(invariantPropertyName);
            var property = obj.GetType().GetProperty(propertyName);

            if (property != null)
            {
                var value = property.GetValue(obj, null);

                return value != null ? value.ToString() : null;
            }
            else
                return null;
        }

        public static Object GetPropertyValue(this Object obj, String propertyName)
        {
            if (obj != null)
            {
                var property = obj.GetType().GetProperty(propertyName);

                if (property != null)
                    return property.GetValue(obj, null);     
            }

            return null;
        }

        public static Object SetPropertyValue(this Object obj, String propertyName, object value)
        {
            if (obj != null)
            {               
                PropertyInfo prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(obj, value, null);
                }
            }

            return null;
        }

        public static bool HasProperty(this Object obj, String propertyName)
        {
            if (obj != null)
            {
                var property = obj.GetType().GetProperty(propertyName);

                if (property != null)
                    return true;
            }

            return false;
        }

        public static string Capitalize(this string obj)
        {
            try
            {
                var result = new List<String>();
                obj = obj.ToLower();

                foreach (var word in obj.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var newWord = word.Trim();
                    newWord = newWord.Substring(0, 1).ToUpper() + newWord.Substring(1);

                    result.Add(newWord);
                }

                return String.Join(" ", result.ToArray());
            }
            catch
            {
                return obj;
            }
        }

        public static String RegexReplace(this string obj, string pattern, string newValue)
        {
            return Regex.Replace(obj, pattern, newValue);
        }

        public static String RegexReplace(this string obj, RegExprs pattern, string newValue)
        {
            return Regex.Replace(obj, pattern.GetDescription(), newValue);
        }

        public static String Translit(this string obj, bool clearDoubleSpaces = true)
        {
            List<String> cyrAlbhabet = new List<String> { "а", "б", "в", "г", "ґ", "д", "є", "е", "ё", "ж",  "з", "і", "ї", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч",  "ш",  "щ",  "ы", "ь", "ъ", "э", "ю", "я",   ".", ",", " ", "-", "_", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            List<String> latAlbhabet = new List<String>   { "a", "b", "v", "g", "g", "d", "e", "e", "e", "zh", "z", "i", "i", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "c", "ch", "sh", "sch", "y", "",  "", "e", "yu", "ya", "-", "-", "-", "-", "_", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            String Result = String.Empty;

            if (!string.IsNullOrEmpty(obj))
            {
                foreach (Char letter in obj.ToLower().ToCharArray())
                    if (latAlbhabet.Contains(letter.ToString()))
                        Result = Result + letter;
                    else
                        if (cyrAlbhabet.Contains(letter.ToString()))
                        Result = Result + latAlbhabet[cyrAlbhabet.IndexOf(letter.ToString())];

                if (clearDoubleSpaces)
                    do
                        Result = Result.Replace("--", "-");
                    while (Result.Contains("--"));
            }

            return Result;
        }

        public static Int32? ToNullableInt(this string obj, bool moreThanZero = true)
        {
            var intValue = 0;

            if (int.TryParse(obj, out intValue))
            {
                if (moreThanZero)
                    return intValue > 0 ? (int?)intValue : null;
                else
                    return intValue;
            }
            else
                return null;
        }

        public static Int32[] ToIntArray(this string obj)
        {
            if (!String.IsNullOrEmpty(obj))
            {
                var result = new List<Int32>();
                var arr = obj.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var val in arr)
                {
                    var item = 0;

                    if (Int32.TryParse(val, out item)) {
                        result.Add(item);
                    };
                }

                if (result.Count > 0)
                    return result.ToArray();
                else
                    return null;
            }
            else
                return null;
        }

        public static String[] ClearSplit(this string obj, char separator, string[] defaultArray = null)
        {
            var arr = defaultArray;

            if (!String.IsNullOrEmpty(obj))
            {
                arr = obj.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                if (arr != null && arr.Length > 0)
                {
                    for (var i = 0; i < arr.Length; i++)
                    {
                        arr[i].Trim();
                    }
                }
            }

            return arr;
        }

        public static T GetOrStore<T>(this Cache cache, string key, int duration, Func<T> generator)
        {
            var result = cache.Get(key);

            if (result == null)
            {
                result = generator();
                cache.Insert(key, result, null, DateTime.Now.AddMinutes(duration), Cache.NoSlidingExpiration);
            }

            return (T)result;
        }

        public static string TrimNullable(this string obj)
        {
            if (!String.IsNullOrWhiteSpace(obj))
                obj.Trim();
            else
                obj = null;

            return obj;
        }

        public static string ReplaceNullable(this string obj, string oldValue, string newValue)
        {
            if (!String.IsNullOrEmpty(obj))
                obj.Replace(oldValue, newValue);
            else
                obj = null;

            return obj;
        }

        public static string ToRssText(this string obj)
        {
            if (!String.IsNullOrEmpty(obj))
            {
                obj = obj.Replace("&nbsp;", " ");
                obj = obj.Replace("&nbsp", " ");
                obj = obj.Replace("&raquo;", "&quot;");
                obj = obj.Replace("&laquo;", "&quot;");
                obj = obj.Replace("\"", "&quot;");
                obj = obj.Replace("«", "&quot;");
                obj = obj.Replace("»", "&quot;");
                obj = obj.Replace("'", "&quot;");
                obj = obj.Replace("`", "&apos;");
                obj = obj.Replace("<", "&lt;");
                obj = obj.Replace(">", "&gt;");                

                obj = Regex.Replace(obj, "\\s+", " ");
                obj = Regex.Replace(obj, "&(?!quot|apos|lt|gt|amp)", "&amp;");
                obj = Regex.Replace(obj, "(?=^)\\s+|\\s+(?=$)", String.Empty);

                return obj.Trim();
            }                
            else
                obj = null;

            return obj;
        }

        public static string MimeType(this string obj)
        {
            if (!String.IsNullOrEmpty(obj))
            {
                if (obj.ToLower().EndsWith(".jpg") || obj.ToLower().EndsWith(".jpeg"))
                    return "image/jpeg";
                if (obj.ToLower().EndsWith(".gif"))
                    return "image/gif";
                if (obj.ToLower().EndsWith(".png"))
                    return "image/png";
                if (obj.ToLower().EndsWith(".bmp"))
                    return "image/bmp";
            }
            else
                obj = null;

            return obj;
        }

        public static Color TransformColor(this Color obj, float correctionFactor)
        {
            float red = (float)obj.R;
            float green = (float)obj.G;
            float blue = (float)obj.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(obj.A, (int)red, (int)green, (int)blue);
        }

        public enum RegExprs
        {
            [Description(@"[\s]{2,}")]
            DoubleSpaces,
            [Description(@"[\D]")]
            NotDigits,
        }

    }
}
