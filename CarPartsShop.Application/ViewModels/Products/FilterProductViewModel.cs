using CarPartsShop.Application.ViewModels.Common;
using MobileStore.Application.ViewModels.Products;

namespace CarPartsShop.Application.ViewModels.Products;

public class FilterProductViewModel
{
    public PaginatedListViewModel<ProductsListViewModel>? Products { get; set; }
    public ProductFilterSpecification? Specification { get; set; }
}



