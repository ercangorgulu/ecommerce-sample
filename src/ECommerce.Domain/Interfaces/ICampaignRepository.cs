using ECommerce.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<IList<Campaign>> GetByProductCodeAsync(string code);
        Task<Campaign> GetByNameAsync(string name);
    }
}
