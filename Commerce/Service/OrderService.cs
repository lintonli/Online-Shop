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
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return "Order added Successfully";
            }
            catch (Exception ex)
            {
                return $"{ex.InnerException.Message}";
            }
        }

        public async  Task<string> DeleteOrder(Order ord)
        {
              _context.Orders.Remove(ord);
            await _context.SaveChangesAsync();
            return "Order deleted successfully";
        }

        public async Task<Order> GetOrderById(Guid OrderId)
        {
            return await _context.Orders.Where(x=>x .OrderId == OrderId).FirstOrDefaultAsync();

        }

        public Task<List<Order>> GetOrderByUserId(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrders()
        {
           return await _context.Orders.ToListAsync();
        }

        public async Task<string> UpdateOrder(Order order)
        {
            /*await _context.Orders.Update(order);*/
            await _context.SaveChangesAsync();
            return "Order Successfully updated";
        }
    }
}
