using ECommerce.Domain.Models;

namespace ECommerce.Domain.Specifications
{
    public class ProductFilterPaginatedSpecification : BaseSpecification<Product>
    {
        public ProductFilterPaginatedSpecification(int skip, int take)
            : base(x => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
