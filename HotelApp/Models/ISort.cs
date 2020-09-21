using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public interface ISort<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities,
                                string orderBy);
    }
}
