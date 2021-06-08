using ECommerce.Application.Interfaces;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Notifications;
using ECommerce.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Services.Api.Controllers
{
    [Authorize]
    public class CampaignController : ApiController
    {
        private readonly ICampaignAppService _campaignAppService;
        public CampaignController(
            ICampaignAppService campaignAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _campaignAppService = campaignAppService;
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteCampaignData", Roles = Roles.Admin)]
        public async Task<IActionResult> Post([FromBody] CampaignViewModel campaignViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(campaignViewModel);
            }

            await _campaignAppService.CreateAsync(campaignViewModel);

            return Response(campaignViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var productViewModel = await _campaignAppService.GetByNamesync(name);

            return Response(productViewModel);
        }
    }
}
