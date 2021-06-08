using ECommerce.Domain.Models;
using System;
using System.Linq.Expressions;

namespace ECommerce.Domain.Specifications
{
    public class OrderFilterPaginatedSpecification : BaseSpecification<Order>
    {
        public OrderFilterPaginatedSpecification(int skip, int take)
            : base(x => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
