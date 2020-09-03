using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace HotelApp.API.Models
{
    public class Sort<T> : ISort<T>
    {
        public IQueryable<T> ApplySort(IQueryable<T> entities, string orderBy)
        {
            if (!entities.Any())
            {
                return entities;
            }
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return entities;
            }
            var parameters = orderBy.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var entityQueryBuilder = new StringBuilder();

            foreach (var param in parameters)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName,
                               StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                entityQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var entityQuery = entityQueryBuilder.ToString().TrimEnd(',', ' ');

            return entities.OrderBy(entityQuery);
        }
    }
}
