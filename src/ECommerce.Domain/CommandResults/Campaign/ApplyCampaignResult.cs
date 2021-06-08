using System;

namespace ECommerce.Domain.CommandResults.Campaign
{
    public class ApplyCampaignResult
    {
        public bool Success { get; set; }
        public Guid CampaignId { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
