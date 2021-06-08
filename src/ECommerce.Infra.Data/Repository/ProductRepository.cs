using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Infra.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Product> GetByCodeAsync(string code)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
