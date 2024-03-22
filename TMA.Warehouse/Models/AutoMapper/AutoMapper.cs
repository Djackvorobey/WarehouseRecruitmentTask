using AutoMapper;
using System;
using TMA.Warehouse.ViewModels;

namespace TMA.Warehouse.Models.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ItemModel, ItemViewModel>()
                .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup.ToString()))
                .ForMember(dest => dest.UnitOfMeasurement, opt => opt.MapFrom(src => src.UnitOfMeasurement.ToString()));

            CreateMap<ItemViewModel, ItemModel>()
                .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => Enum.Parse(typeof(Enums.Enums.ItemGroup), src.ItemGroup)))
                .ForMember(dest => dest.UnitOfMeasurement, opt => opt.MapFrom(src => Enum.Parse(typeof(Enums.Enums.UnitOfMeasurement), src.UnitOfMeasurement)));

            CreateMap<User, AccountModel>();
            CreateMap<AccountModel, User>();

            CreateMap<TmaRequestModel, TmaRequestViewModel>()
                .ForMember(dest => dest.UnitOfMeasurement, opt => opt.MapFrom(src => src.UnitOfMeasurement.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<TmaRequestViewModel, TmaRequestModel>()
                .ForMember(dest => dest.UnitOfMeasurement, opt => opt.MapFrom(src => Enum.Parse(typeof(Enums.Enums.UnitOfMeasurement), src.UnitOfMeasurement)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(Enums.Enums.Status), src.Status)));

            CreateMap<AccountModel, AccountViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.RoleEnum, opt => opt.MapFrom(src => src.RoleEnum));

            CreateMap<AccountViewModel, AccountModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)))
                .ForMember(dest => dest.RoleEnum, opt => opt.MapFrom(src => src.RoleEnum));
        }
    }
}
