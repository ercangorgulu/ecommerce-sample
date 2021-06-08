using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.CommandResults.Campaign;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Commands.Product;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IProductRepository _productRepository;

        public ProductAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _productRepository = productRepository;
        }

        //TODO: campaign
        public IEnumerable<ProductViewModel> GetAll()
        {
            return _productRepository.GetAll()
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
        }

        //TODO: campaign
        public IEnumerable<ProductViewModel> GetAll(int skip, int take)
        {
            return _productRepository.GetAll(new ProductFilterPaginatedSpecification(skip, take))
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            var result = _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
            return await ApplyCampaignAsync(result);
        }

        public async Task<ProductViewModel> GetByCodeAsync(string code)
        {
            var result = _mapper.Map<ProductViewModel>(await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Code == code));
            return await ApplyCampaignAsync(result);
        }

        public async Task CreateAsync(ProductViewModel productViewModel)
        {
            var createCommand = _mapper.Map<CreateNewProductCommand>(productViewModel);
            await _bus.SendCommand(createCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private async Task<ProductViewModel> ApplyCampaignAsync(ProductViewModel productViewModel)
        {
            var applyCampaignCommand = new ApplyCampaignCommand(productViewModel.Code, productViewModel.Price);
            var applyCampaignResult = await _bus.RunCommand<ApplyCampaignResult>(applyCampaignCommand);
            if (applyCampaignResult.Success)
            {
                productViewModel.Price = applyCampaignResult.FinalPrice;
            }
            return productViewModel;
        }
    }
}
