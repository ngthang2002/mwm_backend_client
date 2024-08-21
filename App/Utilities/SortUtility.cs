using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Project.App.Utilities
{
    public class SortUtility<T> where T : class
    {
        public static IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }

            string[] orderParams = orderByQueryString.Trim().Split(',');
            StringBuilder orderQueryBuilder = new();

            foreach (string param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                string propertyFromQueryName = param.Trim().Split(" ")[0];
                PropertyInfo objectProperty = GetPropertyRecursive(typeof(T), propertyFromQueryName);

                if
                (
                    objectProperty is null
                    ||
                    (
                        objectProperty.PropertyType != typeof(string) &&
                        typeof(IEnumerable).IsAssignableFrom(objectProperty.PropertyType)
                    )
                )
                {
                    continue;
                }
                string sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{propertyFromQueryName} {sortingOrder}, ");
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrEmpty(orderQuery))
            {
                return entities;
            }
            return entities.OrderBy(orderQuery);
        }

        public static List<T> ApplySort(List<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }

            string[] orderParams = orderByQueryString.Trim().Split(',');
            StringBuilder orderQueryBuilder = new();

            foreach (string param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                string propertyFromQueryName = param.Trim().Split(" ")[0];
                PropertyInfo objectProperty = GetPropertyRecursive(typeof(T), propertyFromQueryName);

                if
                (
                    objectProperty is null
                    ||
                    (
                        objectProperty.PropertyType != typeof(string) &&
                        typeof(IEnumerable).IsAssignableFrom(objectProperty.PropertyType)
                    )
                )
                {
                    continue;
                }
                string sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{propertyFromQueryName} {sortingOrder}, ");
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrEmpty(orderQuery))
            {
                return entities;
            }
            return entities.AsQueryable().OrderBy(orderQuery).ToList();
        }


        protected static PropertyInfo GetPropertyRecursive(Type baseType, string propertyName)
        {
            string[] parts = propertyName.Split('.');

            if (baseType.GetProperty(parts[0], BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) is null)
            {
                return null;
            }

            return (parts.Length > 1)
                ? GetPropertyRecursive(baseType.GetProperty(parts[0], BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).PropertyType, parts.Skip(1).Aggregate((a, i) => a + "." + i))
                : baseType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        }

    }
}
