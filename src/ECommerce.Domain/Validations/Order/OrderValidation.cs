using ECommerce.Domain.Commands.Order;
using FluentValidation;
using System;

namespace ECommerce.Domain.Validations.Order
{
    public abstract class OrderValidation<T> : AbstractValidator<T> where T : OrderCommand
    {
        protected void ValidateCode()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Please ensure you have entered the Code")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
