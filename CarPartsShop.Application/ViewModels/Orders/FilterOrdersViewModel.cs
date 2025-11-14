using CarPartsShop.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class FilterOrdersViewModel : Paging<UserOrderViewModel>
    {
        public string? RefId { get; set; }
        public SortOrders SortOrders { get; set; }
        public OrderStatusForAdmin OrderStatus { get; set; }

    }
    public enum SortOrders
    {
        [Display(Name = "جدیدترین")]
        Ascending,
        [Display(Name = "قدیمی ترین")]
        Descending,
    }
    public enum OrderStatusForAdmin
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "در حال آماده سازی")]
        InPreparation,
        [Display(Name = "تحویل اداره پست داده شد")]
        Post,
        [Display(Name = "دریافت شد")]
        Received

    }
}
