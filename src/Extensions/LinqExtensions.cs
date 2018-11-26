using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using xTab.Tools.Helpers;

namespace xTab.Tools.Extensions
{
    public static class LinqExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(
            this IList<T> source, 
            Expression<Func<T, string>> valueSelector, 
            Expression<Func<T, string>> textProperty, 
            string notSelectedValue = null,
            string notSelectedText = null,
            string selectedValue = "")
        {
            if (valueSelector == null) throw new ArgumentNullException("valueSelector");
            if (textProperty == null) throw new ArgumentNullException("textProperty");
            if (!(textProperty.Body is MemberExpression)) throw new ArgumentException("Must be a field or property.", "textProperty");

            var item = valueSelector.Parameters[0];
            var itemValue = valueSelector.Body;
            var itemText = Expression.MakeMemberAccess(item, ((MemberExpression)textProperty.Body).Member);
            var targetType = typeof(SelectListItem);
            var bindings = new List<MemberBinding>
            {
                Expression.Bind(targetType.GetProperty("Value"), itemValue),
                Expression.Bind(targetType.GetProperty("Text"), itemText)
            };

            if (!string.IsNullOrEmpty(selectedValue))
                bindings.Add(Expression.Bind(targetType.GetProperty("Selected"), Expression.Equal(itemValue, Expression.Constant(selectedValue))));

            var selector = Expression.Lambda<Func<T, SelectListItem>>(Expression.MemberInit(Expression.New(targetType), bindings), item);
            var result = source.AsQueryable().Select(selector).ToList();

            if (notSelectedValue != null)
                result.Insert(0, new SelectListItem()
                {
                    Text = notSelectedText,
                    Value = notSelectedValue
                });

            return result;
        }

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source,
            Expression<Func<T, string>> valueSelector,
            Expression<Func<T, string>> textProperty,
            string notSelectedValue = null,
            string notSelectedText = null,
            string selectedValue = "")
        {
            if (valueSelector == null) throw new ArgumentNullException("valueSelector");
            if (textProperty == null) throw new ArgumentNullException("textProperty");
            if (!(textProperty.Body is MemberExpression)) throw new ArgumentException("Must be a field or property.", "textProperty");

            var item = valueSelector.Parameters[0];
            var itemValue = valueSelector.Body;
            var itemText = Expression.MakeMemberAccess(item, ((MemberExpression)textProperty.Body).Member);
            var targetType = typeof(SelectListItem);
            var bindings = new List<MemberBinding>
            {
                Expression.Bind(targetType.GetProperty("Value"), itemValue),
                Expression.Bind(targetType.GetProperty("Text"), itemText)
            };

            if (!string.IsNullOrEmpty(selectedValue))
                bindings.Add(Expression.Bind(targetType.GetProperty("Selected"), Expression.Equal(itemValue, Expression.Constant(selectedValue))));

            var selector = Expression.Lambda<Func<T, SelectListItem>>(Expression.MemberInit(Expression.New(targetType), bindings), item);
            var result = source.AsQueryable().Select(selector).ToList();

            if (notSelectedValue != null)
                result.Insert(0, new SelectListItem()
                {
                    Text = notSelectedText,
                    Value = notSelectedValue
                });

            return result;
        }

        public static List<SelectListItem> ToSelectList<T>(
           this IList<T> source,
           Expression<Func<T, bool>> whereExperssion,
           Expression<Func<T, string>> valueSelector,
           Expression<Func<T, string>> textProperty,
           string notSelectedValue = null,
           string notSelectedText = null,
           string selectedValue = "")
        {
            if (valueSelector == null) throw new ArgumentNullException("valueSelector");
            if (textProperty == null) throw new ArgumentNullException("textProperty");
            if (!(textProperty.Body is MemberExpression)) throw new ArgumentException("Must be a field or property.", "textProperty");

            List<SelectListItem> result;
            var item = valueSelector.Parameters[0];
            var itemValue = valueSelector.Body;
            var itemText = Expression.MakeMemberAccess(item, ((MemberExpression)textProperty.Body).Member);
            var targetType = typeof(SelectListItem);
            var bindings = new List<MemberBinding>
            {
                Expression.Bind(targetType.GetProperty("Value"), itemValue),
                Expression.Bind(targetType.GetProperty("Text"), itemText)
            };

            if (!string.IsNullOrEmpty(selectedValue))
                bindings.Add(Expression.Bind(targetType.GetProperty("Selected"), Expression.Equal(itemValue, Expression.Constant(selectedValue))));

            var selector = Expression.Lambda<Func<T, SelectListItem>>(Expression.MemberInit(Expression.New(targetType), bindings), item);
            var query = source.AsQueryable();

            if (whereExperssion != null)
                result = query.Where(whereExperssion).Select(selector).ToList();
            else
                result = query.Select(selector).ToList();

            if (notSelectedValue != null)
            {
                result = result ?? new List<SelectListItem>();

                result.Insert(0, new SelectListItem()
                {
                    Text = notSelectedText,
                    Value = notSelectedValue
                });
            }

            return result;
        }

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source,
            Expression<Func<T, bool>> whereExperssion,
            Expression<Func<T, string>> valueSelector,
            Expression<Func<T, string>> textProperty,
            string notSelectedValue = null,
            string notSelectedText = null,
            string selectedValue = "")
        {
            if (valueSelector == null) throw new ArgumentNullException("valueSelector");
            if (textProperty == null) throw new ArgumentNullException("textProperty");
            if (!(textProperty.Body is MemberExpression)) throw new ArgumentException("Must be a field or property.", "textProperty");

            List<SelectListItem> result;
            var item = valueSelector.Parameters[0];
            var itemValue = valueSelector.Body;
            var itemText = Expression.MakeMemberAccess(item, ((MemberExpression)textProperty.Body).Member);
            var targetType = typeof(SelectListItem);
            var bindings = new List<MemberBinding>
            {
                Expression.Bind(targetType.GetProperty("Value"), itemValue),
                Expression.Bind(targetType.GetProperty("Text"), itemText)
            };

            if (!string.IsNullOrEmpty(selectedValue))
                bindings.Add(Expression.Bind(targetType.GetProperty("Selected"), Expression.Equal(itemValue, Expression.Constant(selectedValue))));

            var selector = Expression.Lambda<Func<T, SelectListItem>>(Expression.MemberInit(Expression.New(targetType), bindings), item);
            var query = source.AsQueryable();
            
            if (whereExperssion != null)
                result = query.Where(whereExperssion).Select(selector).ToList();
            else
                result = query.Select(selector).ToList();

            if (notSelectedValue != null)
            {
                result = result ?? new List<SelectListItem>();

                result.Insert(0, new SelectListItem()
                {
                    Text = notSelectedText,
                    Value = notSelectedValue
                });
            }
            
            return result;
        }

        public static List<SelectListItem> ToSelectList<T>(
            this IList<T> source,
            string valueField,
            string textField,
            string notSelectedValue = null,
            string notSelectedText = null,
            string selectedValue = "")
        {
            var result = new List<SelectListItem>();

            if (notSelectedValue != null)
                result.Insert(0, new SelectListItem()
                {
                    Text = notSelectedText,
                    Value = notSelectedValue
                });

            source.ToList().ForEach(x => result.Add(new SelectListItem()
            {
                Text = (x.GetType().GetProperty(textField).GetValue(x, null) ?? String.Empty).ToString(),
                Value = (x.GetType().GetProperty(valueField).GetValue(x, null) ?? String.Empty).ToString()
            }));

            return result;
        }       

        public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string propertyName, bool descending = false)
        {
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(T), string.Empty);
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                "OrderBy" + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static IOrderedQueryable<T> OrderByColumn<T>(this IEnumerable<T> source, string propertyName, bool descending = false)
        {
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(T), string.Empty);
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                "OrderBy" + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.AsQueryable().Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.AsQueryable().Provider.CreateQuery<T>(call);
        }

        public static IOrderedQueryable<T> ThenByColumn<T>(this IQueryable<T> source, string propertyName, bool descending = false)
        {
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(T), string.Empty);
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                "ThenBy" + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static IList<T> Shuffle<T>(this IList<T> List)
        {
            int n = List.Count;

            while (n > 1)
            {
                n--;
                int k = RandomHelper.ThisThreadsRandom.Next(n + 1);
                T value = List[k];
                List[k] = List[n];
                List[n] = value;
            }

            return List;
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;

                if (count != size)
                    continue;

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            if (bucket != null && count > 0)
                yield return bucket.Take(count);
        }

        public static Expression<Func<T, object>> GetPropertySelector<T>(string propertyName, string argumentName = "x")
        {
            var arg = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(arg, propertyName);           
            var conv = Expression.Convert(property, typeof(object));
            var exp = Expression.Lambda<Func<T, object>>(conv, new ParameterExpression[] { arg });

            return exp;
        }      
        public static Expression<Func<TSource, TProperty>> GetPropertySelector<TSource, TProperty>(string propertyName, string argumentName = "x")
        {
            var arg = Expression.Parameter(typeof(TSource), "x");
            var property = Expression.Property(arg, propertyName);
            var conv = Expression.Convert(property, typeof(TProperty));
            var exp = Expression.Lambda<Func<TSource, TProperty>>(conv, new ParameterExpression[] { arg });

            return exp;
        }
    }
}
