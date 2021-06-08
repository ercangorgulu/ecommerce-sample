using ECommerce.Domain.Events.Order;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.EventHandlers
{
    public class OrderEventHandler :
        INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send notification e-mail to start delivery

            return Task.CompletedTask;
        }
    }
}
