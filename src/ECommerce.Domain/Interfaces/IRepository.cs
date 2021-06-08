using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAllSoftDeleted();
        void Update(TEntity obj);
        void Remove(Guid id);
        Task<int> SaveChangesAsync();
    }
}
