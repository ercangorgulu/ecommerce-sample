using ECommerce.Domain.Core.Commands;
using System;

namespace ECommerce.Domain.Commands.Product
{
    public abstract class ProductCommand : Command
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
