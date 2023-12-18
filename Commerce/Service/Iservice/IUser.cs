using Commerce.Models;

namespace Commerce.Service.Iservice
{
    public interface IUser
    {
        Task <User> GetUserByEmail (string email);
        Task<string> RegisterUser(User user);
    }
}
