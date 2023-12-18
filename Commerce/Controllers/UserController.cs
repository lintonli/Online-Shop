using AutoMapper;
using Commerce.Models;
using Commerce.Models.Dto;
using Commerce.Service.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly IJwt _jwtservice;
        public UserController(IUser user, IMapper mapper, IJwt jwtservice)
        {
            _user = user;
            _mapper = mapper;
            _jwtservice = jwtservice;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(LoginDto user)
        {
            var check = await _user.GetUserByEmail(user.Email);
           
            if (check == null)
            {
                return BadRequest("invalid credentials");
            }
            var passverify = BCrypt.Net.BCrypt.Verify(user.Password, check.Password);
            if (!passverify)
            {
                return BadRequest("Invalid credentials");
            }
            var token = _jwtservice.GenerateToken(check);
            /*  return Ok($"Welcome  {check.Name}");*/
            return Ok(token);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<string>>RegisterUser (UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var check = await _user.GetUserByEmail(userDto.Email);
            if (check != null)
            {
                return BadRequest("Email Exists.Try a new One");
            }
            var response = await _user.RegisterUser(user);
            return Ok(response);
        }
    }
}
