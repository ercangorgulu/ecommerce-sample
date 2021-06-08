using ECommerce.Domain.Commands.Product;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Notifications;
using ECommerce.Domain.Events.Product;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.CommandHandlers
{
    public class ProductCommandHandler : CommandHandler,
        IRequestHandler<CreateNewProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _bus;

        public ProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> Handle(CreateNewProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return false;
            }

            var product = new Product(Guid.NewGuid(), message.Code, message.Price, message.Stock);
            var existingProduct = await _productRepository.GetByCodeAsync(product.Code);
            if (existingProduct != null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "The product with the code has already been created."));
                return false;
            }

            _productRepository.Add(product);

            if (Commit())
            {
                await _bus.RaiseEvent(new ProductCreatedEvent(product.Id, product.Code, product.Price, product.Stock));
            }

            return true;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
