using AutoMapper;
using Commerce.Models;
using Commerce.Models.Dto;

namespace Commerce.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles() 
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }

    }
}
