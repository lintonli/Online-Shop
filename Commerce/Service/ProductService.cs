using Commerce.Data;
using Commerce.Models;
using Commerce.Models.Dto;
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
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return "Product Added Successfully";
            }
            catch (Exception ex) 
            
            {
                //Console.WriteLine();
                return $"{ex.InnerException.Message}";
            }

        }

        public async Task<string> DeleteProduct(Product pro)
        {
            _context.Products.Remove(pro);
            await _context.SaveChangesAsync();
            return "Product deleted successfully";
            /*Console.WriteLine("Enter Product Id");
            var Str = Console.ReadLine();
            if (Str != null)
            {
                return "Product deleted Successfully";
            }
            return "Product ID does not exist";*/

        }

        public  async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetPaginatedProducts(int page, int pageSize)
        {
            return await _context.Products
                                 .OrderBy(p => p.ProductName) // or any other ordering
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<int> GetTotalProductCount()
        {
            return await _context.Products.CountAsync();
        }


        public async Task<Product> GetProductById(Guid ProductId)
        {
            return  await _context.Products.Where(x => x. ProductId == ProductId).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            /*_context.Products.Add(product);*/
            await _context.SaveChangesAsync();
            return "Product updated Successfully";
        }
        //filtering
        public async Task<List<Product>> FilterProducts(string ProductName, int? Price)
        {
           var query= _context.Products.AsQueryable();
            if(!string.IsNullOrEmpty(ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(ProductName));
            }
            if(Price != null)
            {
                query=query.Where(p=> p.Price != Price);
            }
            return await query.ToListAsync();
        }
    }
}
