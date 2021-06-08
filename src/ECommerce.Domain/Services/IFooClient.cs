using Refit;
using System.Threading.Tasks;

namespace ECommerce.Domain.Services
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
