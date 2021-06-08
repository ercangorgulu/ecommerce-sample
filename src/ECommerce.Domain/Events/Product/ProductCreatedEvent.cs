using ECommerce.Domain.Core.Events;
using System;

namespace ECommerce.Domain.Events.Product
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent(Guid id, string code, decimal price, int stock)
        {
            Id = id;
            Code = code;
            Price = price;
            Stock = stock;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
