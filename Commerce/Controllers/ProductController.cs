using AutoMapper;
using Commerce.Models;
using Commerce.Models.Dto;
using Commerce.Service.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IMapper _mapper;
        public ProductController(IProduct product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Product>>> GetAllProducts(int page =1, int pagesize=10)
        {
            try
            {
                var totalProd = await _product.GetTotalProductCount();
                var totalpages = (int)totalProd / pagesize;
                if(page > totalpages)
                {
                    return BadRequest("Page number is out of range");
                }
                var products = await _product.GetPaginatedProducts(page, pagesize);
                /*var productDtos = _mapper.Map<List<ProductDto>>(products);*/
                /*   var products = await _product.GetAllProducts();*/
               /* var response = new
                {
                    Data = productDtos,
                    TotalPages = totalpages,
                    CurrentPage = page,
                    TotalRecords = totalProd
                };*/
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
        [HttpGet("{ProductId}")]
        
        public async Task<ActionResult<Product>>GetProductById(Guid ProductId)
        {
            var product = await _product.GetProductById(ProductId);
            if (product == null)
            {
                return NotFound("Id not found");
            }
            return Ok(product);
        }
        [HttpDelete("{ProductId}")]
        [Authorize(Policy= "Admin")]
        public async Task<ActionResult<string>> DeleteProduct(Guid ProductId)
        {
            var myproduct = await _product.GetProductById(ProductId);
            if(myproduct == null)
            {
                return NotFound("Product not found");
            }
            var response = await _product.DeleteProduct(myproduct);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Policy="Admin")]
        public async Task<ActionResult<string>> AddProduct(ProductDto productDto)
        {
            var newProduct= _mapper.Map<Product>(productDto);
            var response = await _product.AddProduct(newProduct);
            return Created($"Product/{newProduct.ProductName}", response);

        }
        [HttpPut("{ProductId}")]
        [Authorize(Policy= "Admin")]
        public async Task<ActionResult<string>>UpdateProduct(Guid ProductId, ProductDto product)
        {
            var updateproduct = await _product.GetProductById(ProductId);
            if(updateproduct == null)
            {
                return NotFound("Product not found");
            }
            var updatedproduct = _mapper.Map(product,updateproduct);
             var response = await _product.UpdateProduct(updatedproduct);
            return Ok(response);
        }
        //filtering
        [HttpGet("filter")]
        public async Task<ActionResult<List<Product>>> FilterProducts([FromQuery] string ProductName, [FromQuery] int? Price)
        {
            try
            {
                var filterProducts = await _product.FilterProducts(ProductName, Price);
                return Ok(filterProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
