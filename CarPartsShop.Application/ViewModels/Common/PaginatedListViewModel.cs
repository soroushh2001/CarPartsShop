namespace CarPartsShop.Application.ViewModels.Common;

public class PaginatedListViewModel<T>
{
    public List<T>? Items { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public IEnumerable<int> VisiblePages { get; set; }


    public PaginationPartialViewModel ToPaginationPartial(int pageIndex)
    {
        return new PaginationPartialViewModel
        {
            HasPreviousPage = HasPreviousPage,
            HasNextPage = HasNextPage,
            VisiblePages = VisiblePages,
            PageIndex = pageIndex
        };
    }
}

public class PaginationPartialViewModel
{
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public IEnumerable<int> VisiblePages { get; set; }
    public int PageIndex { get; set; }
}