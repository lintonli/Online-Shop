namespace Commerce.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string? Description {  get; set; }
        public string? Status {  get; set; }
        public List<Product> products { get; set; }= new List<Product>();
    }
}
