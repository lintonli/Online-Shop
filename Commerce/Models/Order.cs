namespace Commerce.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string? Description {  get; set; }
        public string? Status {  get; set; }
      public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
