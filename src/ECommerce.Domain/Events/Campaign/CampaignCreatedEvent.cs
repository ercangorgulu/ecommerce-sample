using ECommerce.Domain.Core.Events;
using System;

namespace ECommerce.Domain.Events.Campaign
{
    public class CampaignCreatedEvent : Event
    {
        public CampaignCreatedEvent(
            Guid id,
            string name,
            string productCode,
            DateTime endDate,
            double priceManipulationLimit,
            int targetSalesCount)
        {
            Id = id;
            Name = name;
            ProductCode = productCode;
            EndDate = endDate;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ProductCode { get; set; }

        public DateTime EndDate { get; set; }

        public double PriceManipulationLimit { get; set; }

        public int TargetSalesCount { get; set; }
    }
}
