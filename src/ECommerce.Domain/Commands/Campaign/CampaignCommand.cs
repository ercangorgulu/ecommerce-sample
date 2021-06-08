using ECommerce.Domain.Core.Commands;
using System;

namespace ECommerce.Domain.Commands.Campaign
{
    public abstract class CampaignCommand : Command
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ProductCode { get; set; }

        public DateTime EndDate { get; set; }

        public double PriceManipulationLimit { get; set; }

        public int TargetSalesCount { get; set; }
    }
}
