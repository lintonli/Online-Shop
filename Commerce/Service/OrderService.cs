using Commerce.Data;
using Commerce.Models;
using Commerce.Service.Iservice;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Service
{
    public class OrderService : IOrder
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            return "Order added Successfully";
        }

        public string DeleteOrder(Guid Orderid)
        {
            Console.WriteLine("Enter OrderId");
            var str =Console.ReadLine();
            if (str != null)
            {
                return "Order Deleted Successfully";
            }
            return "OrderId Does not exist";
        }

        public async Task<Order> GetOrderById(Guid OrderId)
        {
            return await _context.Orders.Where(x=>x .OrderId == OrderId).FirstOrDefaultAsync();

        }

        public async Task<List<Order>> GetOrders()
        {
           return await _context.Orders.ToListAsync();
        }

        public string UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            return "Order Successfully updated";
        }
    }
}
