using AutoMapper;
using mojeEUC.Shared.Configuration.Model.Entities;
using mojeEUC.Shared.Configuration.Services;

namespace mojeEUC.Shared.Configuration;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		#region v1

		// Item
		CreateMap<ItemCreateModel, Item>()
			.ForMember(a=>a.Id, opt=>opt.Ignore())
			.ForMember(a=>a.IsActive, opt=>opt.Ignore());
		CreateMap<ItemUpdateModel, Item>()
			.ForMember(a=>a.Id, opt=>opt.Ignore())
			.ForMember(a=>a.IsActive, opt=>opt.Ignore());

		#endregion
	}
}
