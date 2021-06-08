using ECommerce.Domain.Commands.Order;

namespace ECommerce.Domain.Validations.Order
{
    public class CreateNewOrderCommandValidation : OrderValidation<CreateNewOrderCommand>
    {
        public CreateNewOrderCommandValidation()
        {
            ValidateCode();
        }
    }
}
