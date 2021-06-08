using ECommerce.Domain.Core.Events;
using System;

namespace ECommerce.Domain.Events.Order
{
    public class OrderCreatedEvent : Event
    {
        public OrderCreatedEvent(Guid id, string productCode, int quantity)
        {
            Id = id;
            ProductCode = productCode;
            Quantity = quantity;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }
    }
}
