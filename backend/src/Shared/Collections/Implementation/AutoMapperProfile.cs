using AutoMapper;
using mojeEUC.Shared.Collections.Model.Entities;
using mojeEUC.Shared.Collections.Services;

namespace mojeEUC.Shared.Collections;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<ItemCreateModel, Item>()
			.ForMember(a=>a.Id, opt=>opt.Ignore())
			.ForMember(a=>a.IsActive, opt=>opt.Ignore());
		CreateMap<ItemUpdateModel, Item>()
			.ForMember(a=>a.Id, opt=>opt.Ignore())
			.ForMember(a=>a.IsActive, opt=>opt.Ignore());
	}
}
