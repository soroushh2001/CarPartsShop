using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Application.ViewModels.Common
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageRange { get; private set; } // Number of pages to show before and after the current page

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int pageRange = 2)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageRange = pageRange;

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public IEnumerable<int> VisiblePages
        {
            get
            {
                int startPage = Math.Max(1, PageIndex - PageRange);
                int endPage = Math.Min(TotalPages, PageIndex + PageRange);
                return Enumerable.Range(startPage, endPage - startPage + 1);
            }
        }

        public static async Task<PaginatedListViewModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, int pageRange = 2)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var paginated = new PaginatedList<T>(items, count, pageIndex, pageSize, pageRange);
            return new PaginatedListViewModel<T>()
            {
                Items = paginated.ToList(),
                HasPreviousPage = paginated.HasPreviousPage,
                HasNextPage = paginated.HasNextPage,
                VisiblePages = paginated.VisiblePages
            };
        }
    }
}