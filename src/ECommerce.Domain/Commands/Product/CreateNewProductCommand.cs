using ECommerce.Domain.Validations;
using ECommerce.Domain.Validations.Product;

namespace ECommerce.Domain.Commands.Product
{
    public class CreateNewProductCommand : ProductCommand
    {
        public CreateNewProductCommand(string code, decimal price, int stock)
        {
            Code = code;
            Price = price;
            Stock = stock;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
