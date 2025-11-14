using CarPartsShop.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Products;

public class FilterProductViewModel : Paging<ProductsListViewModel>
{
    public string? Search { get; set; }
    public List<string>? CategoryUrls { get; set; }
    public string? CategoryTitle { get; set; }

    public string? BrandTitle { get; set; }

    public ProductQualityForFilter ProductQualityForFilter { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool? IsExisted { get; set; }

    public ProductSortBy SortBy { get; set; }
    public ProductOrderBy OrderBy { get; set; }

}
public enum ProductQualityForFilter
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "اصلی")]
    Original,
    [Display(Name = "شرکتی")]
    Corporate,
    [Display(Name = "متفرفه")]
    Others
}

public enum ProductSortBy
{

    [Display(Name = "تاریخ")]
    Date,
    [Display(Name = "قیمت")]
    Price,
    [Display(Name = "پرفروش ترین ها")]
    BestSeller,
    [Display(Name = "عنوان")]
    Title
}

public enum ProductOrderBy
{
    Asc,
    Desc
}


