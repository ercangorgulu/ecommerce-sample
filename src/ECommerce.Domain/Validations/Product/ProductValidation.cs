using ECommerce.Domain.Commands.Product;
using FluentValidation;
using System;

namespace ECommerce.Domain.Validations.Product
{
    public abstract class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateCode()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Please ensure you have entered the Code")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidatePrice()
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The price can't be negative value");
        }

        protected void ValidateStock()
        {
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The stock can't be negative value");
        }

        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
