using AutoMapper;
using mojeEUC.Shared.Collections.Contracts.v1;
using mojeEUC.Shared.Collections.Model.Entities;
using mojeEUC.Shared.Collections.Services;

namespace mojeEUC.Shared.Collections.Server.Automapper.v1;

public class ItemAutoMapperProfile : Profile
{
	public ItemAutoMapperProfile()
	{
		#region v1

		// Item
		CreateMap<ItemCreateOrUpdateRequest, ItemCreateModel>();
		CreateMap<ItemCreateOrUpdateRequest, ItemUpdateModel>();
		CreateMap<Item, ItemResponse>();
		CreateMap<Item, ItemUpdateModel>();

		// ItemByType
		CreateMap<ItemByTypeCreateOrUpdateRequest, ItemCreateModel>();
		CreateMap<ItemByTypeCreateOrUpdateRequest, ItemUpdateModel>();
		CreateMap<Item, ItemByTypeResponse>();

		#endregion
	}
}
