using ECommerce.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface ICampaignAppService : IDisposable
    {
        Task CreateAsync(CampaignViewModel campaignViewModel);
        IEnumerable<CampaignInfoViewModel> GetAll();
        IEnumerable<CampaignInfoViewModel> GetAll(string productCode);
        Task<CampaignInfoViewModel> GetByIdAsync(Guid id);
        Task<CampaignInfoViewModel> GetByNamesync(string name);
    }
}
