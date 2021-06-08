using ECommerce.Domain.Validations.Order;

namespace ECommerce.Domain.Commands.Order
{
    public class CreateNewOrderCommand : OrderCommand
    {
        public CreateNewOrderCommand(string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
