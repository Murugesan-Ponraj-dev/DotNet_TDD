using Order.Domain.Common;
using Order.Domain.DTOs;

namespace Order.Domain.Services
{
    public interface IProductService
    {

        //  ApiResponse<ProductDTO> GetAllProduct();
        //  ApiResponse<ProductDTO> GetProductById(int id);
        Task<ApiResponse<bool>> CreateProduct(ProductDTO productDTO);
        // ApiResponse<bool> UpdateProduct();

    }
}
