using ECommerce.Domain.Commands.Campaign;
using FluentValidation;

namespace ECommerce.Domain.Validations.Campaign
{
    public class ApplyCampaignCommandValidation : AbstractValidator<ApplyCampaignCommand>
    {
        public ApplyCampaignCommandValidation()
        {
            ValidateCode();
        }

        protected void ValidateCode()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Please ensure you have entered the Code")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidatePrice()
        {
            RuleFor(x => x.ProductPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The price can't be negative value");
        }

        protected void ValidateQuantity()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The stock can't be negative value");
        }
    }
}
