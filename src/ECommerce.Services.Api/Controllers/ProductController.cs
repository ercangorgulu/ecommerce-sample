using ECommerce.Application.Interfaces;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Notifications;
using ECommerce.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Services.Api.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly IProductAppService _productAppService;

        public ProductController(
            IProductAppService productAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Response(_productAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("id/{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var productViewModel = await _productAppService.GetByIdAsync(id);

            return Response(productViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var productViewModel = await _productAppService.GetByCodeAsync(code);

            return Response(productViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteProductData", Roles = Roles.Admin)]
        public async Task<IActionResult> Post([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(productViewModel);
            }

            await _productAppService.CreateAsync(productViewModel);

            return Response(productViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_productAppService.GetAll(skip, take));
        }
    }
}
