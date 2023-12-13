using Microsoft.AspNetCore.Mvc;
using Order.Domain.Common;
using Order.Domain.DTOs;
using Order.Domain.Services;


namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController>  _logger;

        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ApiResponse<bool>> Post(ProductDTO productDTO)
        {
            return await _productService.CreateProduct(productDTO);
        }

    }
}
