using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Commands.Order;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IOrderRepository _orderRepository;

        public OrderAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public IEnumerable<OrderViewModel> GetAll()
        {
            return _orderRepository.GetAll()
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<OrderViewModel> GetAll(int skip, int take)
        {
            return _orderRepository.GetAll(new OrderFilterPaginatedSpecification(skip, take))
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id)
        {
            return _mapper.Map<OrderViewModel>(await _orderRepository.GetByIdAsync(id));
        }

        public async Task CreateAsync(OrderViewModel orderViewModel)
        {
            var createCommand = _mapper.Map<CreateNewOrderCommand>(orderViewModel);
            await _bus.SendCommand(createCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
