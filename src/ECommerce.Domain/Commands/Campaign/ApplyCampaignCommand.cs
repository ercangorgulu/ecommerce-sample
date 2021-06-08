using ECommerce.Domain.CommandResults.Campaign;
using ECommerce.Domain.Core.Commands;
using ECommerce.Domain.Validations.Campaign;
using MediatR;

namespace ECommerce.Domain.Commands.Campaign
{
    public class ApplyCampaignCommand : Command, IRequest<ApplyCampaignResult>
    {
        public ApplyCampaignCommand(string productCode, decimal productPrice, int? quantity = null)
        {
            ProductCode = productCode;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public string ProductCode { get; set; }
        public decimal ProductPrice { get; set; }
        public int? Quantity { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ApplyCampaignCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
