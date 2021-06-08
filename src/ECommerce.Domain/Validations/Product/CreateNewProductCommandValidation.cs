using ECommerce.Domain.Commands.Product;

namespace ECommerce.Domain.Validations.Product
{
    public class CreateNewProductCommandValidation : ProductValidation<CreateNewProductCommand>
    {
        public CreateNewProductCommandValidation()
        {
            ValidateCode();
            ValidatePrice();
            ValidateStock();
        }
    }
}
