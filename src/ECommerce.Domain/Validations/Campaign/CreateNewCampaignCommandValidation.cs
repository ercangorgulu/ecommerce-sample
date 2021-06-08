using ECommerce.Domain.Commands.Campaign;

namespace ECommerce.Domain.Validations.Campaign
{
    public class CreateNewCampaignCommandValidation : CampaignValidation<CreateNewCampaignCommand>
    {
        public CreateNewCampaignCommandValidation()
        {
            ValidateCode();
        }
    }
}
