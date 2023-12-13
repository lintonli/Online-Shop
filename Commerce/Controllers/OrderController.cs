using AutoMapper;
using Commerce.Models;
using Commerce.Service.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrder orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }
        [HttpGet]

        public Task<ActionResult<Order>>GetOrderbyId(Guid id)
        {

        }
        [HttpPut]
        public ActionResult<string> UpdateOrder(Order order)
        {

        }
        [HttpDelete]
        public ActionResult DeleteOrder(Guid OrderId)
        {

        }
        [HttpPut]
        public Task<ActionResult<string>>AddOrder(Order order)
        {

        }
    }
}
