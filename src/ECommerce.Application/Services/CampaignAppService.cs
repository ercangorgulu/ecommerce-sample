using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Enums;
using ECommerce.Application.Interfaces;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Services;
using ECommerce.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class CampaignAppService : ICampaignAppService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public CampaignAppService(
            ICampaignRepository campaignRepository,
            IOrderRepository orderRepository,
            IMapper mapper,
            IMediatorHandler bus)
        {
            _campaignRepository = campaignRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public IEnumerable<CampaignInfoViewModel> GetAll()
        {
            return _campaignRepository.GetAll()
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .Select(x => GetCampaignInfoAsync(x).Result);
        }

        public IEnumerable<CampaignInfoViewModel> GetAll(string productCode)
        {
            return _campaignRepository.GetAll(new CampaignFilterByProductSpecification(productCode))
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .Select(x => GetCampaignInfoAsync(x).Result);
        }

        public async Task<CampaignInfoViewModel> GetByIdAsync(Guid id)
        {
            var result = _mapper.Map<CampaignViewModel>(await _campaignRepository.GetByIdAsync(id));
            return await GetCampaignInfoAsync(result);
        }

        public async Task<CampaignInfoViewModel> GetByNamesync(string name)
        {
            var result = _mapper.Map<CampaignViewModel>(await _campaignRepository.GetByNameAsync(name));
            return await GetCampaignInfoAsync(result);
        }

        public async Task CreateAsync(CampaignViewModel orderViewModel)
        {
            var createCommand = _mapper.Map<CreateNewCampaignCommand>(orderViewModel);
            await _bus.SendCommand(createCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private async Task<CampaignInfoViewModel> GetCampaignInfoAsync(CampaignViewModel campaignViewModel)
        {
            var orders = await _orderRepository.GetByCampaignIdAsync(campaignViewModel.Id);
            var totalSalesCount = orders.Sum(c => c.Quantity);
            var totalMoney = orders.Sum(c => c.Price * c.Quantity);
            return new CampaignInfoViewModel
            {
                Name = campaignViewModel.Name,
                ProductCode = campaignViewModel.ProductCode,
                TargetSalesCount = campaignViewModel.TargetSalesCount,
                Status = DateTimeService.Current > campaignViewModel.EndDate
                    ? CampaignStatus.Ended
                    : CampaignStatus.Active,
                TotalSalesCount = totalSalesCount,
                AverageItemPrice = totalMoney / totalSalesCount
            };
        }
    }
}
