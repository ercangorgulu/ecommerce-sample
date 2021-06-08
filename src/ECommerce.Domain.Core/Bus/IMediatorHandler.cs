using ECommerce.Domain.Core.Commands;
using ECommerce.Domain.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace ECommerce.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task<TResult> RunCommand<TResult>(IRequest<TResult> command);
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
