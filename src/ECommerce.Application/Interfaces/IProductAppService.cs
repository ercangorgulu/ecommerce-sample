using ECommerce.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        Task CreateAsync(ProductViewModel productViewModel);
        IEnumerable<ProductViewModel> GetAll();
        IEnumerable<ProductViewModel> GetAll(int skip, int take);
        Task<ProductViewModel> GetByIdAsync(Guid id);
        Task<ProductViewModel> GetByCodeAsync(string code);
    }
}
