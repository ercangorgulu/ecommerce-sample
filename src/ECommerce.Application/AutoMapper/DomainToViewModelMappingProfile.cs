using AutoMapper;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Models;

namespace ECommerce.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Campaign, CampaignViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}
