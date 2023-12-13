using Commerce.Models;

namespace Commerce.Service.Iservice
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid ProductId);
        Task<string> AddProduct(Product product);
        string DeleteProduct(Guid ProductId);
        string UpdateProduct(Product product);
    }
}
