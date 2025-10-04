using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Application.ViewModels.Orders;
using CarPartsShop.Domain.Entities.Order;
using CarPartsShop.Domain.Interfaces;

namespace CarPartsShop.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task AddToCart(int userId, int productId, int count = 1)
        {
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (order == null)
            {
                var newOrder = new Order
                {
                    UserId = userId,
                    Sum = product.Price * count
                };
                await _orderRepository.AddOrderAsync(newOrder);
                await _orderRepository.SaveChangesAsync();
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.Id,
                    Count = count,
                    Price = product.Price,
                    ProductId = productId,
                };
                await _orderRepository.AddOrderDetailAsync(orderDetail);
                await _orderRepository.SaveChangesAsync();
            }
            else
            {
                var orderDetail = await _orderRepository.GetOrderDetailAsync(order.Id, productId);
                if (orderDetail == null)
                {
                    orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        Count = count,
                        Price = product.Price,
                        ProductId = productId,
                    };
                    await _orderRepository.AddOrderDetailAsync(orderDetail);
                    await _orderRepository.SaveChangesAsync();
                }
                else
                {
                    orderDetail.Count += count;
                    _orderRepository.UpdateOrderDetails(orderDetail);
                    await _orderRepository.SaveChangesAsync();
                }
                order.Sum = await _orderRepository.UpdateSumOrderAsync(order.Id);
                _orderRepository.UpdateOrder(order);
                await _orderRepository.SaveChangesAsync();
            }

        }

        public async Task<CartViewModel> GetCartAsync(int userId)
        {
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            if (order == null)
                return null;
            return new CartViewModel
            {
                OrderId = order.Id,
                Sum = order.Sum,
                Items = order.OrderDetails.Select(x => new CartItemViewModel
                {
                    Count = x.Count,
                    MainImage = x.Product.MainImage,
                    Price = x.Product.Price,
                    ProductId = x.ProductId,
                    ProductTitle = x.Product.Title,
                    OrderDetailId = x.Id
                }).ToList()
            };
        }

        public async Task RemoveItemFromCartAsync(int orderDetailId, int userId)
        {
            var orderDetail = await _orderRepository.GetOrderDetailByIdAsync(orderDetailId);
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            _orderRepository.RemoveOrderDetail(orderDetail!);
            await _orderRepository.SaveChangesAsync();
            order.Sum = await _orderRepository.UpdateSumOrderAsync(order.Id);
            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();

        }

        public async Task IncreaseDecreaseCartItemAsync(int orderDetailId, string op, int userId)
        {
            var orderDetail = await _orderRepository.GetOrderDetailByIdAsync(orderDetailId);
            switch (op)
            {
                case "i":
                    orderDetail.Count += 1;
                    break;
                case "d":

                    orderDetail.Count -= 1;

                    break;
            }

            if (orderDetail.Count == 0)
            {
                _orderRepository.RemoveOrderDetail(orderDetail);
            }
            else
            {
                _orderRepository.UpdateOrderDetails(orderDetail);
            }
            await _orderRepository.SaveChangesAsync();
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            order.Sum = await _orderRepository.UpdateSumOrderAsync(order.Id);
            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<int> GetCurrentCartTotalPriceAsync(int userId)
        {
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            return order.Sum;
        }

        public async Task FinallyOrderAsync(int userId, string refId)
        {
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            foreach (var item in order.OrderDetails)
            {
                item.Price = item.Product.Price;
                _orderRepository.UpdateOrderDetails(item);
            }
            await _orderRepository.SaveChangesAsync();
            order.Status = OrderStatus.InPreparation;
            order.PaymentDate = DateTime.Now;
            order.RefId = refId;
            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CompleteOrderInformation(int userId, CompleteOrderInformationViewModel orderInfo)
        {
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            order.Address = orderInfo.Address;
            order.RecipientName = orderInfo.RecipientName;
            order.ZipCode = orderInfo.ZipCode;
            order.PhoneNumber = orderInfo.PhoneNumber;
            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<CartItemViewModel>> GetCartItemsByOrderIdAsync(int orderId)
        {
            var details = await _orderRepository.GetOrderDetailItemsByOrderIdAsync(orderId);
            return details.Select(x => new CartItemViewModel
            {
                Count = x.Count,
                MainImage = x.Product.MainImage,
                ProductTitle = x.Product.Title,
                ProductId = x.Product.Id,
                OrderDetailId = x.Id,
                Price = x.Price,
            }).ToList();
        }

        public async Task<List<UserOrderViewModel>> GetUserFinallyOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetUserFinallyOrdersAsync(userId);
            return orders.Select(x => new UserOrderViewModel
            {
                RefId = x.RefId,
                OrderStatus = x.Status,
                PaymentDate = x.PaymentDate,
                Sum = x.Sum,
                OrderId = x.Id
            }).ToList();
        }

        public async Task<List<OrderDetailsViewModel>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var details = await _orderRepository.GetOrderDetailItemsByOrderIdAsync(orderId);
            return details.Select(x => new OrderDetailsViewModel
            {
                MainImage = x.Product.MainImage,
                ProductTitle = x.Product.Title,
                Price = x.Price,
                Count = x.Count,
            }).ToList();

        }

        public async Task<int> GetCurrentCartItemsCountAsync(int userId)
        {
            var cart = await _orderRepository.GetUserLatestOpenOrderAsync(userId);
            if (cart == null)
                return 0;
            return cart.OrderDetails.Count();
        }

        public async Task<FilterOrdersViewModel> FilterOrdersAsync(OrderFilterSpecification specification)
        {
            var query = _orderRepository.GetAllOrders();

            switch (specification.OrderStatus)
            {
                case OrderStatusForAdmin.All:
                    break;
                case OrderStatusForAdmin.InPreparation:
                    query = query.Where(x => x.Status == OrderStatus.InPreparation);
                    break;
                case OrderStatusForAdmin.Post:
                    query = query.Where(x => x.Status == OrderStatus.Post); break;
                case OrderStatusForAdmin.Received:
                    query = query.Where(x => x.Status == OrderStatus.Received);
                    break;
            }


            if (!string.IsNullOrEmpty(specification.Search))
            {
                query = query.Where(x => x.RefId == specification.Search);
            }

            query = query.OrderByDescending(X => X.PaymentDate);
       
            var items = query.Select(x => new UserOrderViewModel
            {
                OrderId = x.Id,
                OrderStatus = x.Status,
                PaymentDate = x.PaymentDate,
                Sum = x.Sum,
                RefId = x.RefId
            }).AsQueryable();

            var paging = await PaginatedList<UserOrderViewModel>.CreateAsync(items, specification.PageIndex, specification.PageSize);

            return new FilterOrdersViewModel
            {
                Orders = paging,
                Specification = specification,
            };
        }

        public async Task<ChangeOrderStatusViewModel?> GetCurrentOrderStatusToChangeAsync(string refCode)
        {
            var order = await _orderRepository.GetOrderByRefCodeAsync(refCode);
            if (order == null)
                return null;
            return new ChangeOrderStatusViewModel
            {
                RefCode = refCode,
                OrderStatus = order.Status,
            };
        }

        public async Task<bool> ChangeOrderStatusAsync(ChangeOrderStatusViewModel change)
        {
            var orderToEdit = await _orderRepository.GetOrderByRefCodeAsync(change.RefCode);
            if (orderToEdit == null)
                return false;
            orderToEdit.Status = change.OrderStatus;
            _orderRepository.UpdateOrder(orderToEdit);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<RecipientInfoViewModel?> GetRecipientInfoAsync(string refCode)
        {
            var order = await _orderRepository.GetOrderByRefCodeAsync(refCode);
            if (order == null)
                return null;
            return new RecipientInfoViewModel
            {
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                RecipientName = order.RecipientName,
                ZipCode = order.ZipCode,
                RefId = refCode
            };
        }
    }
}
