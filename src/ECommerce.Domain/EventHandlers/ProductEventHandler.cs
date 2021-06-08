using ECommerce.Domain.Events.Product;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.EventHandlers
{
    public class ProductEventHandler :
        INotificationHandler<ProductCreatedEvent>
    {
        public Task Handle(ProductCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }
    }
}
