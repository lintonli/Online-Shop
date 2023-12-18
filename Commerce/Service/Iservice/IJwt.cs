using Commerce.Models;

namespace Commerce.Service.Iservice
{
    public interface IJwt
    {
        string GenerateToken(User user);
    }
}
