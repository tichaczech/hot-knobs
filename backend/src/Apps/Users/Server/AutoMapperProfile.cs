using AM = AutoMapper;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Shared.Users.UseCases;

namespace thc.HotKnobs.Shared.Users.Server;

internal class AutoMapperProfile : AM.Profile
{
	public AutoMapperProfile()
	{
		#region v1

		// Device
		_ = CreateMap<Device, Contracts.v1.DeviceResponse>();

		// My Device
		_ = CreateMap<Contracts.v1.MyDeviceRegisterRequest, DeviceRegisterModel>();
		_ = CreateMap<Device, Contracts.v1.MyDeviceResponse>();

		// My Profile
		_ = CreateMap<Contracts.v1.MyProfileCreateOrUpdateRequest, ProfileCreateModel>()
			.ForMember(s => s.Name, opt => opt.MapFrom(src => src.DisplayName));
		_ = CreateMap<Contracts.v1.MyProfileCreateOrUpdateRequest, ProfileUpdateModel>()
			.ForMember(s => s.Name, opt => opt.MapFrom(src => src.DisplayName));
		_ = CreateMap<Profile, Contracts.v1.MyProfileResponse>()
			.ForMember(s => s.DisplayName, opt => opt.MapFrom(src => src.Name));

		// Profile
		_ = CreateMap<Profile, Contracts.v1.ProfileResponse>()
			.ForMember(s => s.DisplayName, opt => opt.MapFrom(src => src.Name));

		#endregion
	}
}
