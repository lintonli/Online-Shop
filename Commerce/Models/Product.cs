using System.ComponentModel.DataAnnotations;

namespace Commerce.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ? ProductName { get; set; }
        public string ? Description { get; set; }
        public int Price { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
      
        

    }
}
