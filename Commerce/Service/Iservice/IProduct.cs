using Commerce.Models;
using Commerce.Models.Dto;

namespace Commerce.Service.Iservice
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts();

         Task<List<Product>> GetPaginatedProducts(int page, int pageSize); // New method for pagination
        Task<int> GetTotalProductCount();
        Task<Product> GetProductById(Guid ProductId);
        Task<string> AddProduct(Product product);
        Task<string> DeleteProduct(Product pro);
        Task<string> UpdateProduct(Product product);
        //filtering
        Task<List<Product>> FilterProducts(string ProductName, int? Price);
    }
}
