using AM = AutoMapper;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Shared.Users.UseCases;

namespace thc.HotKnobs.Shared.Users;

public class AutoMapperProfile : AM.Profile
{
	public AutoMapperProfile()
	{
		// Device
		_ = CreateMap<Device, DeviceUpdateModel>(); // See DeviceUseCases.RegisterAsync for details on why this mapping is needed.
		_ = CreateMap<DeviceCreateModel, Device>();
		_ = CreateMap<DeviceRegisterModel, DeviceCreateModel>();
		_ = CreateMap<DeviceUpdateModel, Device>();

		// Profile
		_ = CreateMap<ProfileCreateModel, Profile>()
			.ForMember(s => s.CreatedAt, opt => opt.Ignore())
			.ForMember(s => s.Id, opt => opt.Ignore())
			.ForMember(s => s.IsActive, opt => opt.Ignore())
			.ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(s => s.UpdatedAt, opt => opt.Ignore());
		_ = CreateMap<ProfileUpdateModel, Profile>()
			.ForMember(s => s.CreatedAt, opt => opt.Ignore())
			.ForMember(s => s.Id, opt => opt.Ignore())
			.ForMember(s => s.IsActive, opt => opt.Ignore())
			.ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(s => s.UpdatedAt, opt => opt.Ignore());
	}
}
