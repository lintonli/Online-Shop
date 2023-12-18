namespace Commerce.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //automaticall registers a user
        public string Roles { get; set; } = "User";

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
