using ECommerce.Domain.CommandResults.Campaign;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Notifications;
using ECommerce.Domain.Events.Campaign;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.CommandHandlers
{
    public class CampaignCommandHandler : CommandHandler,
        IRequestHandler<CreateNewCampaignCommand, bool>,
        IRequestHandler<ApplyCampaignCommand, ApplyCampaignResult>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMediatorHandler _bus;
        private readonly IOrderRepository _orderRepository;

        public CampaignCommandHandler(
            ICampaignRepository campaignRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _campaignRepository = campaignRepository;
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(CreateNewCampaignCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return false;
            }

            var campaign = new Campaign(Guid.NewGuid(), message.Name, message.ProductCode, message.EndDate,
                message.PriceManipulationLimit, message.TargetSalesCount);
            _campaignRepository.Add(campaign);

            if (Commit())
            {
                await _bus.RaiseEvent(new CampaignCreatedEvent(campaign.Id, campaign.Name, campaign.ProductCode,
                    campaign.EndDate, campaign.PriceManipulationLimit, campaign.TargetSalesCount));
            }

            return true;
        }

        public async Task<ApplyCampaignResult> Handle(ApplyCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaigns = await _campaignRepository.GetByProductCodeAsync(request.ProductCode);
            if (!campaigns.Any())
            {
                return new ApplyCampaignResult
                {
                    Success = false
                };
            }

            var (availableCampaign, orders) = await SelectAvailableCampaignAsync(campaigns, request.Quantity ?? 0);
            if (availableCampaign == null)
            {
                return new ApplyCampaignResult
                {
                    Success = false
                };
            }

            //dummy price calculation
            var campaignDuration = (availableCampaign.EndDate - availableCampaign.CreatedAt).TotalHours;
            var passedCampaignDuration = (DateTimeService.Current - availableCampaign.CreatedAt).TotalHours;
            var totalQuantity = orders.Sum(c => c.Quantity);
            var targetQuantity = (int)(availableCampaign.TargetSalesCount / campaignDuration * passedCampaignDuration);
            var ratio = totalQuantity == 0 ? 1 : Math.Min(2, (double)targetQuantity / totalQuantity) / 2.0;
            var finalPrice = request.ProductPrice * (100m - (decimal)(availableCampaign.PriceManipulationLimit * ratio)) / 100m;

            return new ApplyCampaignResult
            {
                Success = true,
                CampaignId = availableCampaign.Id,
                FinalPrice = finalPrice
            };
        }

        private async Task<(Campaign campaign, IList<Order> orders)> SelectAvailableCampaignAsync(IList<Campaign> campaigns, int quantity)
        {
            foreach (var campaign in campaigns.OrderByDescending(x => x.PriceManipulationLimit))
            {
                var orders = await _orderRepository.GetByCampaignIdAsync(campaign.Id);
                var totalQuantity = orders.Sum(x => x.Quantity) + quantity;
                if (totalQuantity <= campaign.TargetSalesCount)
                {
                    return (campaign, orders);
                }
            }
            return (null, null);
        }
    }
}
