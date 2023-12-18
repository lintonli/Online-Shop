using Commerce.Models;

namespace Commerce.Service.Iservice
{
    public interface IOrder
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(Guid Orderid);
        Task<string> AddOrder(Order order);
        Task<string> UpdateOrder(Order order);
        Task<string> DeleteOrder(Order ord);
        Task<List<Order>> GetOrderByUserId(Guid UserId);
    }
}
