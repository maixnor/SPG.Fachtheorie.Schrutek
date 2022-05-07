using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Services.Helpers
{
    public class PagenatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public PagenatedList(List<T> items, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(items.Count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PagenatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var items = await source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagenatedList<T>(items, pageIndex, pageSize);
        }
    }
}