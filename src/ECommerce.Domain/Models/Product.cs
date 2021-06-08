using ECommerce.Domain.Core.Models;
using System;

namespace ECommerce.Domain.Models
{
    public class Product : EntityAudit
    {
        public Product(Guid id, string code, decimal price, int stock)
        {
            Id = id;
            Code = code;
            Price = price;
            Stock = stock;
        }

        protected Product() { }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
