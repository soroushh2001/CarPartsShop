namespace CarPartsShop.Application.ViewModels.Common;

public class BaseQueryParams
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; } = "Desc";
    public string? SortBy { get; set; } = "ModifiedDate";
    public string? Search { get; set; }
}