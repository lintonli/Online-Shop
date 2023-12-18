using AutoMapper;
using Commerce.Models;
using Commerce.Models.Dto;
using Commerce.Service.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {

            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }
        [HttpGet("{OrderId}")]
        [Authorize]

        public async Task<ActionResult<Order>> GetOrderbyId(Guid OrderId)
        {
            var ord = await _orderService.GetOrderById(OrderId);
            if (ord == null)
            {
                return NotFound("Order not Found");
            }
            return Ok(ord);
        }
        [HttpPut("{OrderId}")]
        [Authorize(Policy = "Admin")]
        public  async Task<ActionResult<string>> UpdateOrder( Guid OrderId,OrderDto order)
        {
            var updateorder = await _orderService.GetOrderById(OrderId);
            if (updateorder == null)
            {
                return NotFound("Product not found");
            }
            var updatedorder = _mapper.Map(order, updateorder);
            var response = await _orderService.UpdateOrder(updatedorder);
            return Ok(response);
        }
        [HttpDelete("{OrderId}")]
        [Authorize(Policy ="Admin")]
        public async Task<ActionResult<string>> DeleteOrder(Guid OrderId)
        {
            var myorder = await _orderService.GetOrderById(OrderId);
            if(myorder == null)
            {
                return NotFound("Order not found");
            }
            var response = await _orderService.DeleteOrder(myorder);
            return Ok(response);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> AddOrder(OrderDto orderDto)
        {
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            var newOrder = _mapper.Map<Order>(orderDto);
            newOrder.UserId=new Guid (userId);
            var response = await _orderService.AddOrder(newOrder);
            return Created($"Order/{newOrder.OrderId}", response);
        }
        //orders by id
        [HttpGet("user/{Id}")]
        [Authorize]
        public async Task<ActionResult<List<Order>>>GetUserOrders(Guid Id)
        {
            try
            {
                var orders = await _orderService.GetOrderByUserId(Id);
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch
            {
                return StatusCode(500,"Server Error");
            }
            
        }
    }
}
