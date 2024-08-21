using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project.App.Utilities
{
    public class ElasticSortUtility<T> : SortUtility<T> where T : class
    {
        public static SortDescriptor<T> ApplyElasticSort(SortDescriptor<T> sortDescriptor, string orderByQueryString, string defaultQueryString = "")
        {
            if (sortDescriptor is null)
            {
                return sortDescriptor;
            }

            if (string.IsNullOrWhiteSpace(orderByQueryString) && string.IsNullOrWhiteSpace(defaultQueryString))
            {
                return sortDescriptor;
            }

            string[] orderParams = (string.IsNullOrWhiteSpace(orderByQueryString) ? defaultQueryString : orderByQueryString).Trim().Split(',');

            foreach (string param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                string propertyFromQueryName = param.Trim().Split(" ")[0];
                PropertyInfo objectProperty = GetPropertyRecursive(typeof(T), propertyFromQueryName);

                if (objectProperty is null)
                    continue;

                object[] attrs = objectProperty.GetCustomAttributes(true);
                bool isKeyword = attrs.Any(x => x.GetType().Name.Equals("KeywordAttribute"));

                string fieldName = propertyFromQueryName + (isKeyword ? string.Empty : (objectProperty.PropertyType == typeof(string) ? ".keyword" : string.Empty));
                SortOrder sortingOrder = param.EndsWith(" desc") ? SortOrder.Descending : SortOrder.Ascending;

                sortDescriptor.Field(fieldName, sortingOrder);
            }

            return sortDescriptor;
        }

    }
}
