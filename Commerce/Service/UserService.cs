using Commerce.Data;
using Commerce.Models;
using Commerce.Service.Iservice;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Service
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        //login
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x=>x.Email.ToLower()==email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<string> RegisterUser(User user)
        {
           _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User added successfully";
        }
    }
}
