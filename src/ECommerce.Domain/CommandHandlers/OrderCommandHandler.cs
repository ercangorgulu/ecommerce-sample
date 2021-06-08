using ECommerce.Domain.CommandResults.Campaign;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Commands.Order;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Notifications;
using ECommerce.Domain.Events.Order;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.CommandHandlers
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<CreateNewOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _bus;

        public OrderCommandHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> Handle(CreateNewOrderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return false;
            }

            var product = await _productRepository.GetByCodeAsync(message.ProductCode);
            if (product == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Non-existing product is tried to be ordered"));
                return false;
            }
            if (product.Stock < message.Quantity)
            {
                return false;
            }

            product.Stock -= message.Quantity;
            _productRepository.Update(product);

            var applyCampaignCommand = new ApplyCampaignCommand(message.ProductCode, product.Price, message.Quantity);
            var applyCampaignResult = await _bus.RunCommand<ApplyCampaignResult>(applyCampaignCommand);

            var order = applyCampaignResult.Success
                ? new Order(message.ProductCode, message.Quantity, applyCampaignResult.FinalPrice, applyCampaignResult.CampaignId)
                : new Order(message.ProductCode, message.Quantity, product.Price, null);

            _orderRepository.Add(order);

            if (Commit())
            {
                await _bus.RaiseEvent(new OrderCreatedEvent(order.Id, order.ProductCode, order.Quantity));
            }

            return true;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
