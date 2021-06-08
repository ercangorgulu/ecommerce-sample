using ECommerce.Application.Interfaces;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Services.Api.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(
            IOrderAppService orderAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(orderViewModel);
            }

            await _orderAppService.CreateAsync(orderViewModel);

            return Response(orderViewModel);
        }
    }
}
