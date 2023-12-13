using AutoMapper;
using Order.Domain.Common;
using Order.Domain.Common.Resources;
using Order.Domain.DTOs;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Domain.Services;

namespace Order.Application.Services
{
    public class ProductService : IProductService
    {

        public readonly IProductRepository _productRepository;
        public readonly IMapper _mapper;
        public readonly IResourceManager _resourceManager;
        public ProductService(IProductRepository productRepository, IMapper mapper, IResourceManager resourceManager)
        {

            _productRepository = productRepository;
            _mapper = mapper;
            _resourceManager = resourceManager;
        }


        /// <summary>
        /// Create Product 
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public Task<ApiResponse<bool>> CreateProduct(ProductDTO productDTO)
        {
            try
            {

                string successMesage = _resourceManager.GetResourceValue<MessageResources>(ResourceConstConfig.SystemMsgResrceName, ResourceConstConfig.ProductSuccess);
                string failureMessage = _resourceManager.GetResourceValue<MessageResources>(ResourceConstConfig.SystemMsgResrceName, ResourceConstConfig.ProductFailure);

                var product = _mapper.Map<Product>(productDTO);
                bool isSuccess = _productRepository.CreateProduct(product);

                if (!isSuccess)
                    return Task.FromResult(ApiResponse<bool>.Fail(failureMessage));
                return Task.FromResult(ApiResponse<bool>.Success(isSuccess, successMesage));
            }
            catch (Exception ex)
            {

                return Task.FromResult(ApiResponse<bool>.Fail(ex));
            }

        }
    }
}
