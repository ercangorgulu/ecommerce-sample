using ECommerce.Domain.Validations.Campaign;
using System;

namespace ECommerce.Domain.Commands.Campaign
{
    public class CreateNewCampaignCommand : CampaignCommand
    {
        public CreateNewCampaignCommand(
            string name,
            string productCode,
            DateTime endDate,
            double priceManipulationLimit,
            int targetSalesCount)
        {
            Name = name;
            ProductCode = productCode;
            EndDate = endDate;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewCampaignCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
