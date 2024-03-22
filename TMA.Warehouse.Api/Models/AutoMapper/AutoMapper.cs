using AutoMapper;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.DataBase.Enums;

namespace TMA.Warehouse.Api.Models.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AccountModel, Account>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => (int)src.RoleEnum));

            CreateMap<int, Roles.RoleEnum>().ConvertUsing(src => (Roles.RoleEnum)src);

            CreateMap<Item, ItemModel>();
            CreateMap<ItemModel, Item>();

            CreateMap<TmaRequestModel, TmaRequest>();
            CreateMap<TmaRequest, TmaRequestModel>();

            CreateMap<UnitOfMeasurements.UnitOfMeasurement, int>().ConvertUsing(src => (int)src);
            CreateMap<Statuses.Status, int>().ConvertUsing(src => (int)src);

            CreateMap<Account, AccountModel>()
                .ForMember(dest => dest.RoleEnum, opt => opt.MapFrom(src => (Roles.RoleEnum)src.RoleId));
        }
    }
}
