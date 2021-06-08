using ECommerce.Domain.Models;

namespace ECommerce.Domain.Specifications
{
    public class CampaignFilterByProductSpecification : BaseSpecification<Campaign>
    {
        public CampaignFilterByProductSpecification(string productCode) : 
            base(x => x.ProductCode == productCode)
        {
        }
    }
}
