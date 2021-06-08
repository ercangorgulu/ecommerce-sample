using ECommerce.Domain.Models;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByCodeAsync(string code);
    }
}
