using AutoMapper;
using mojeEUC.Shared.Configuration.Contracts.v1;
using mojeEUC.Shared.Configuration.Model.Entities;
using mojeEUC.Shared.Configuration.Services;

namespace mojeEUC.Shared.Configuration.Server.Automapper.v1;

public class ItemByTypeAutoMapperProfile : Profile
{
	public ItemByTypeAutoMapperProfile()
	{
		CreateMap<ItemByTypeCreateOrUpdateRequest, ItemCreateModel>();
		CreateMap<ItemByTypeCreateOrUpdateRequest, ItemUpdateModel>();
		CreateMap<Item, ItemByTypeResponse>();
	}
}
