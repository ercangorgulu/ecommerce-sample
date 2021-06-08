using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Services;
using FluentValidation;
using System;

namespace ECommerce.Domain.Validations.Campaign
{
    public abstract class CampaignValidation<T> : AbstractValidator<T> where T : CampaignCommand
    {
        protected void ValidateCode()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Please ensure you have entered the Code")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(DateTimeService.Current).WithMessage("Past dates are not allowed for campaigns");

            RuleFor(x => x.PriceManipulationLimit)
                .ExclusiveBetween(0, 100).WithMessage("Price manupilation should be between (0,100)");
        }

        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
