using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Infra.Data.Repository
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IList<Campaign>> GetByProductCodeAsync(string code)
        {
            return await DbSet.AsNoTracking().Where(x => x.ProductCode == code).ToListAsync();
        }

        public async Task<Campaign> GetByNameAsync(string name)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
