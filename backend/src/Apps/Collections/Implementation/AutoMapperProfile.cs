using AutoMapper;

using thc.HotKnobs.Shared.Collections.Model.Entities;
using thc.HotKnobs.Shared.Collections.Services;

namespace thc.HotKnobs.Shared.Collections;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		_ = CreateMap<ItemCreateModel, Item>()
			.ForMember(a => a.Id, opt => opt.Ignore())
			.ForMember(a => a.IsActive, opt => opt.Ignore());
		_ = CreateMap<ItemUpdateModel, Item>()
			.ForMember(a => a.Id, opt => opt.Ignore())
			.ForMember(a => a.IsActive, opt => opt.Ignore());
	}
}
