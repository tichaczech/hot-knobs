using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Model;
using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Shared.Users.UseCases;

public enum DevicePlatform
{
	Unknown,
	iOS,
	Android,
	Windows,
	macOS,
	Linux
}

/// <summary>
/// Represents a model used for creating a Device.
/// Inherits from <see cref="ModelWithName"/> and <see cref="ICreateModel"/>.
/// </summary>
public class DeviceCreateModel : ModelWithName, IEntityCreateModel
{
	/// <summary>
	/// Unique identifier for the device, e.g. UUID generated and stored in device secure storage.
	/// </summary>
	[Required]
	public required string DeviceId { get; set; }

	// ///<summary>
	// /// The date and time when the device was last active.
	// ///</summary>
	// [Required]
	// public required DateTimeOffset LastActiveAt { get; set; }

	/// <summary>
	/// The device model name, e.g. "iPhone 14 Pro".
	/// </summary>
	[Required]
	public required string ModelName { get; set; }

	///<summary>
	/// The device platform, e.g. "iOS" or "Android".
	///</summary>
	[Required]
	public required DevicePlatform Platform { get; set; }

	// /// <summary>
	// /// The ID of the profile the device belongs to.
	// /// </summary>
	// [Required]
	// public required string ProfileId { get; set; }

	/// <summary>
	/// APNS or FCM token for the device, used for push notifications.
	/// </summary>
	[Required]
	public required string PushNotificationToken { get; set; }

	/// <summary>
	/// The name of the device's operating system, e.g. "iOS 16.4" or "Android 13".
	/// </summary>
	[Required]
	public required string SystemName { get; set; }
}

public class DeviceRegisterModel : DeviceCreateModel { }

/// <summary>
/// Represents a model used for updating a Device.
/// Inherits from <see cref="ModelWithName"/> and <see cref="IUpdateModel"/>.
/// </summary>
public class DeviceUpdateModel : ModelWithName, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing Devices.
/// </summary>
public interface IDeviceUseCases : IEntityUseCases<Device, DeviceCreateModel, DeviceUpdateModel>
{

	/// <summary>
	/// Retrieves a list of Devices based on the specified criteria.
	/// </summary>
	/// <param name="query">Optional. Filters the list to include only Devices matching the specified query.</param>
	/// <param name="profileId">Optional. Filters the list to include only Devices associated with the specified Profile Id.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only Devices modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active Devices in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the ids of Devices that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, string? profileId = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);

	/// <summary>
	/// Registers a Device for the specified Profile Id.
	/// </summary>
	/// <param name="profileId"></param>
	/// <param name="model"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<Device> RegisterAsync(string profileId, DeviceRegisterModel model, CancellationToken cancellationToken = default);
}
