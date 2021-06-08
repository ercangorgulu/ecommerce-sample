using ECommerce.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderAppService : IDisposable
    {
        Task CreateAsync(OrderViewModel orderViewModel);
        IEnumerable<OrderViewModel> GetAll();
        IEnumerable<OrderViewModel> GetAll(int skip, int take);
        Task<OrderViewModel> GetByIdAsync(Guid id);
    }
}
