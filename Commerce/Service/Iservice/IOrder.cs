using Commerce.Models;

namespace Commerce.Service.Iservice
{
    public interface IOrder
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(Guid Orderid);
        Task<string> AddOrder(Order order);
        string UpdateOrder(Order order);
        string DeleteOrder(Guid Orderid);
    }
}
