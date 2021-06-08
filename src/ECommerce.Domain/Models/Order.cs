using ECommerce.Domain.Core.Models;
using ECommerce.Domain.Services;
using System;

namespace ECommerce.Domain.Models
{
    public class Order : Entity
    {
        public Order(string productCode, int quantity, decimal price, Guid? campaignId)
        {
            ProductCode = productCode;
            Quantity = quantity;
            CampaignId = campaignId;
            Price = price;
            CreatedAt = DateTimeService.Current;
        }

        protected Order() { }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Guid? CampaignId { get; set; }

        public DateTime CreatedAt { get; set; }

        //public string UserId { get; set; }
    }
}
