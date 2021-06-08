using AutoMapper;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Commands.Order;
using ECommerce.Domain.Commands.Product;

namespace ECommerce.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CampaignViewModel, CreateNewCampaignCommand>()
                .ConstructUsing(x => new CreateNewCampaignCommand(x.Name, x.ProductCode,
                    x.EndDate, x.PriceManipulationLimit, x.TargetSalesCount));

            CreateMap<OrderViewModel, CreateNewOrderCommand>()
                .ConstructUsing(x => new CreateNewOrderCommand(x.ProductCode, x.Quantity));

            CreateMap<ProductViewModel, CreateNewProductCommand>()
                .ConstructUsing(x => new CreateNewProductCommand(x.Code, x.Price, x.Stock));
        }
    }
}
