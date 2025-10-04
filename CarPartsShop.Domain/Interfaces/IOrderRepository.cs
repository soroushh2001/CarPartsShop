using CarPartsShop.Domain.Entities.Order;

namespace CarPartsShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order?> GetUserLatestOpenOrderAsync(int userId);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task<OrderDetail?> GetOrderDetailAsync(int orderId, int productId);
        void UpdateOrderDetails(OrderDetail orderDetail);
        Task<int> UpdateSumOrderAsync(int orderId);
        void UpdateOrder(Order order);
        void RemoveOrderDetail(OrderDetail orderDetail);
        Task<OrderDetail?> GetOrderDetailByIdAsync(int detailsId);
        Task<List<OrderDetail>> GetOrderDetailItemsByOrderIdAsync(int orderId);
        Task<List<Order>> GetUserFinallyOrdersAsync(int userId);
        IQueryable<Order> GetAllOrders();
        Task<Order?> GetOrderByRefCodeAsync(string refCode);
        Task SaveChangesAsync();
    }
}
