using AutoMapper;
using ECommerce.Application.Services;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.CommandResults.Campaign;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DDD.Application.UnitTests.Services
{
    public class ProductAppServiceTests
    {
        [Fact]
        public void GetById()
        {
            // Arrange
            var product = new Product(new Guid(), "001", 100, 10);

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.GetByIdAsync(product.Id).Returns(Task.FromResult(product));

            var bus = Substitute.For<IMediatorHandler>();
            bus.RunCommand<ApplyCampaignResult>(Arg.Any<ApplyCampaignCommand>()).Returns(
                Task.FromResult(new ApplyCampaignResult
                {
                    Success = false
                }));

            var mapper = Substitute.For<IMapper>();
            mapper.Map<ProductViewModel>(product).Returns(new ProductViewModel
            {
                Id = product.Id,
                Code = product.Code,
                Price = product.Price,
                Stock = product.Stock
            });

            // Act
            var sut = new ProductAppService(mapper, bus, productRepository);
            var result = sut.GetByIdAsync(product.Id).Result;

            // Assert
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Code, result.Code);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Stock, result.Stock);
        }
    }
}
