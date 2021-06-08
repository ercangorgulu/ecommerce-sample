using ECommerce.Domain.Core.Commands;
using System;

namespace ECommerce.Domain.Commands.Order
{
    public abstract class OrderCommand : Command
    {
        public Guid Id { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }
    }
}
