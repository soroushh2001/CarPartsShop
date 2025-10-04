using CarPartsShop.Domain.Entities.Order;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CarPartsShopDbContext _context;

        public OrderRepository(CarPartsShopDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);  
        }

        public async Task<Order?> GetUserLatestOpenOrderAsync(int userId)
        {
            return await _context.Orders
                .Include(x=> x.OrderDetails)
                .ThenInclude(x=> x.Product)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.WaitToPayment);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
        }

        public async Task<OrderDetail?> GetOrderDetailAsync(int orderId, int productId)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public void UpdateOrderDetails(OrderDetail orderDetail)
        {
            _context.Update(orderDetail);
        }

        public async Task<int> UpdateSumOrderAsync(int orderId)
        {
            return await _context.OrderDetails.Where(o => o.OrderId == orderId).Select(d => d.Count * d.Price).SumAsync();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
        }

        public Task<OrderDetail?> GetOrderDetailByIdAsync(int detailsId)
        {
            return _context.OrderDetails.FirstOrDefaultAsync(x => x.Id == detailsId);
        }

        public async Task<List<OrderDetail>> GetOrderDetailItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails.Include(x=> x.Product).Include(x=> x.Order).Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<List<Order>> GetUserFinallyOrdersAsync(int userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId && x.Status!= OrderStatus.WaitToPayment)
                .OrderByDescending(x=> x.PaymentDate)
                .ToListAsync();
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders.Where(x => x.Status != OrderStatus.WaitToPayment);
        }

        public async Task<Order?> GetOrderByRefCodeAsync(string refCode)
        {
            return await _context.Orders.FirstOrDefaultAsync(x=> x.RefId == refCode);
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}
