using AutoMapper;
using mojeEUC.Shared.Configuration.Contracts.v1;
using mojeEUC.Shared.Configuration.Model.Entities;
using mojeEUC.Shared.Configuration.Services;

namespace mojeEUC.Shared.Configuration.Server.Automapper.v1;

public class ItemAutoMapperProfile : Profile
{
	public ItemAutoMapperProfile()
	{
		CreateMap<ItemCreateOrUpdateRequest, ItemCreateModel>();
		CreateMap<ItemCreateOrUpdateRequest, ItemUpdateModel>();
		CreateMap<Item, ItemResponse>();
		CreateMap<Item, ItemUpdateModel>();
	}
}
