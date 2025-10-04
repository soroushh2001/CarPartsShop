using CarPartsShop.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace MobileStore.Application.ViewModels.Products;

public class ProductFilterSpecification : BaseQueryParams
{
    public List<string>? CategoryUrls { get; set; }
    public string? CategoryTitle { get; set; }
    
    public string? BrandTitle { get; set; }
    
    public ProductQualityForFilter ProductQualityForFilter { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool? IsExisted { get; set; }

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


