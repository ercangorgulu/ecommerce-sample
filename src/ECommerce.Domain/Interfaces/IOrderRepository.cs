using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IList<Order>> GetByCampaignIdAsync(Guid campaignId);
        Task<IList<Order>> GetByProductCodeAsync(string code);
    }
}
