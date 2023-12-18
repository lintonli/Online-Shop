using Commerce.Models;
using Commerce.Service.Iservice;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Commerce.Service
{
     
    public class JwtService : IJwt
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
            public string GenerateToken(User user)
        {
            //read the appsettings
            var secretkey = _configuration.GetSection("JwtOptions:SecretKey").Value;
            var audience = _configuration.GetSection("JwtOptions:Audience").Value;
            var issuer = _configuration.GetSection("JwtOptions:Issuer").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            //security algorithms 
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payloads
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Roles", user.Roles));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokendescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
