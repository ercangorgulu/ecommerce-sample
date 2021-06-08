using ECommerce.Domain.Events.Campaign;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.EventHandlers
{
    public class CampaignEventHandler :
        INotificationHandler<CampaignCreatedEvent>
    {
        public Task Handle(CampaignCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send notification e-mail to users

            return Task.CompletedTask;
        }
    }
}
