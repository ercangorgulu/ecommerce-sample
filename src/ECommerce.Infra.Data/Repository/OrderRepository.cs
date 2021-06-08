using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Infra.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IList<Order>> GetByCampaignIdAsync(Guid campaignId)
        {
            return await DbSet.AsNoTracking().Where(x => x.CampaignId == campaignId).ToListAsync();
        }

        public async Task<IList<Order>> GetByProductCodeAsync(string code)
        {
            return await DbSet.AsNoTracking().Where(x => x.ProductCode == code).ToListAsync();
        }
    }
}
