using CarPartsShop.Application.ViewModels.Orders;
using CarPartsShop.Domain.Entities.Order;

namespace CarPartsShop.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddToCart(int userId, int productId,int count=1);
        Task<CartViewModel> GetCartAsync(int userId);
        Task RemoveItemFromCartAsync(int orderDetailId, int userId);
        Task IncreaseDecreaseCartItemAsync(int orderDetailId,string op, int userId);
        Task<int> GetCurrentCartTotalPriceAsync(int userId);
        Task FinallyOrderAsync(int userId,string refId);
        Task CompleteOrderInformation(int userId,CompleteOrderInformationViewModel orderInfo);
        Task<List<CartItemViewModel>> GetCartItemsByOrderIdAsync(int orderId);
        Task<List<UserOrderViewModel>> GetUserFinallyOrdersAsync(int userId);
        Task<List<OrderDetailsViewModel>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task<int> GetCurrentCartItemsCountAsync(int userId);
        Task<FilterOrdersViewModel> FilterOrdersAsync(FilterOrdersViewModel filter);
        Task<ChangeOrderStatusViewModel?> GetCurrentOrderStatusToChangeAsync(string refCode);
        Task<bool> ChangeOrderStatusAsync(ChangeOrderStatusViewModel change);
        Task<RecipientInfoViewModel?> GetRecipientInfoAsync(string refCode);
        Task DeleteProductFromCartIfNotExistedAsync(int userId);
        Task<List<CartItemViewModel>> GetCartItemByUserIdAsync(int userId);
    }
}
