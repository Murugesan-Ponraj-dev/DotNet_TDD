using AutoMapper;
using Moq;
using Order.Application.Services;
using Order.Domain.Common;
using Order.Domain.Common.Resources;
using Order.Domain.DTOs;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Domain.Services;
using System.Resources;
using Xunit;

namespace Order.Test.Services.Test
{
    public class ProductServiceTest
    {
        // public readonly ProductDTO _request;
        public readonly ProductService _service;
        public readonly Mock<IProductRepository> _productRepository;
        public readonly Mock<IMapper> _iMapper;
        public readonly Mock<IResourceManager> _ResourceManager;
        public readonly string productSuccessMsg;
        public readonly string productFailureMsg;
        public readonly string nullReferenceErrorMgs;
        public readonly string objectReferenceErrorMsg;
        public ProductServiceTest()
        {

            _iMapper = new Mock<IMapper>();
            _ResourceManager = new Mock<IResourceManager>();
            _productRepository = new Mock<IProductRepository>();
            _service = new ProductService(_productRepository.Object, _iMapper.Object, _ResourceManager.Object);

            ResourceManager resourceManager = new ResourceManager(ResourceConstConfig.SystemMsgResrceName, typeof(MessageResources).Assembly);
            productSuccessMsg = resourceManager?.GetString(ResourceConstConfig.ProductSuccess) ?? string.Empty;
            nullReferenceErrorMgs = resourceManager?.GetString(ResourceConstConfig.NullReferenceError) ?? string.Empty;
            productFailureMsg = resourceManager?.GetString(ResourceConstConfig.ProductFailure) ?? string.Empty;
            objectReferenceErrorMsg = resourceManager?.GetString(ResourceConstConfig.ObjectRefError) ?? string.Empty;



        }



        [Fact]
        public void Should_ReturnSuccessRespone_OnSaveSuccess()
        {
            //Arrange
            var model = new ProductDTO() { Name = "TV", Description = "OLED TV", Price = 350000 };
            _productRepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Returns(true);
            _iMapper.Setup(a => a.Map<ProductDTO, Product>(It.IsAny<ProductDTO>())).Returns(It.IsAny<Product>());

            _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productSuccessMsg);



            //Act
            var response = _service.CreateProduct(model);
            if (response is null)
                Assert.Fail(nullReferenceErrorMgs);


            //Assert
            Assert.True(response.Result.IsSuccess);
            Assert.Equal(productSuccessMsg, response.Result.Message);


        }


        [Fact]
        public void Should_ReturnFailureResponse_OnSaveFailure()
        {
            //Arrange
            var model = new ProductDTO() { Name = "TV", Description = "OLED TV", Price = 350000 };
            _productRepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Returns(false);
            _iMapper.Setup(a => a.Map<ProductDTO, Product>(It.IsAny<ProductDTO>())).Returns(It.IsAny<Product>());

            _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productFailureMsg);


            //Act
            var response = _service.CreateProduct(model);
            if (response is null)
                Assert.Fail(nullReferenceErrorMgs);


            //Assert
            Assert.True(!response.Result.IsSuccess);
            Assert.Equal(productFailureMsg, response.Result.Message);
        }



        [Fact]
        public void Should_Through_ObjectReferenceError_Exception()
        {
            //Arrange
            ProductDTO model = new ProductDTO() { Name = "TV", Description = "OLED TV", Price = 350000 };

            // Remove Setup file for product repo
            _productRepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Returns(false);


            _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productFailureMsg);

            // mapper object as null
            IMapper? resourceObject = null;

            var service = new ProductService(_productRepository.Object, resourceObject, _ResourceManager.Object);


            //Act
            var response = service.CreateProduct(model);
            if (response is null)
                Assert.Fail(nullReferenceErrorMgs);


            //Assert
            Assert.True(!response.Result.IsSuccess);
            Assert.Equal(objectReferenceErrorMsg, response.Result.Message);
        }
    }
}
