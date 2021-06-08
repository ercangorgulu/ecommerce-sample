using ECommerce.Domain.Core.Models;
using System;

namespace ECommerce.Domain.Models
{
    public class Campaign : EntityAudit
    {
        public Campaign(
            Guid id,
            string name,
            string productCode,
            DateTime endDate,
            double priceManipulationLimit,
            int targetSalesCount)
        {
            Id = id;
            Name = name;
            ProductCode = productCode;
            EndDate = endDate;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
        }

        protected Campaign() { }

        public string Name { get; set; }
        public string ProductCode { get; set; }
        public DateTime EndDate { get; set; }
        public double PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
    }
}
