using CarPartsShop.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Orders
{
    public class OrderFilterSpecification : BaseQueryParams
    {
        public OrderStatusForAdmin OrderStatus { get; set; }
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
