using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Extensions.Pagination
{
    public static class PagingExtensions
    {
        //Linq to sql
        public static IQueryable<Tsource> Page<Tsource>(this IQueryable<Tsource> source, int page, int pageSize) where Tsource : class
        {
            return source.Skip(page).Take(pageSize);
        }

        //Linq to enumerate
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize) where TSource : class
        {
            return source.Skip(page).Take(pageSize);
        }

    }
}
