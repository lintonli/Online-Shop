using Commerce.Data;
using Commerce.Models;
using Commerce.Service.Iservice;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Service
{
    public class ProductService : IProduct
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            return "Product Added Successfully";

        }

        public string DeleteProduct(Guid ProductId)
        {
            Console.WriteLine("Enter Product Id");
            var Str = Console.ReadLine();
            if (Str != null)
            {
                return "Product deleted Successfully";
            }
            return "Product ID does not exist";

        }

        public  async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<Product> GetProductById(Guid ProductId)
        {
            return _context.Products.Where(x => x. ProductId == ProductId).FirstOrDefaultAsync();
        }

        public string UpdateProduct(Product product)
        {
            _context.Products.Add(product);
            return "Product updated Successfully";
        }
    }
}
