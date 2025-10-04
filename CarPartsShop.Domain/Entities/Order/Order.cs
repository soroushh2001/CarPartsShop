using CarPartsShop.Domain.Entities.Account;
using CarPartsShop.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPartsShop.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public int Sum { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public string? RefId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? RecipientName { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        [Display(Name = "در انتظار پرداخت")]
        WaitToPayment,
        [Display(Name = "در حال آماده سازی")]
        InPreparation,
        [Display(Name = "تحویل اداره پست داده شد")]
        Post,
        [Display(Name = "دریافت شد")]
        Received
    }

}
