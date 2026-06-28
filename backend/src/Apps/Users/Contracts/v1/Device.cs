using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Shared.Users.Contracts.v1;

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
/// Represents a (User's) Device.
/// </summary>
public abstract class DeviceContract
{
	/// <summary>
	/// The name of the device, e.g. "John's iPhone" or "Work Laptop".
	/// </summary>
	[Required]
	public required string Name { get; set; }
}

/// <summary>
/// Request to create a device.
/// </summary>
public class DeviceCreateOrUpdateRequest : DeviceFullContract, IResourceCreateOrUpdateRequest
{
	/// <summary>
	/// APNS or FCM token for the device, used for push notifications.
	/// </summary>
	/// <remarks>
	/// This is expected to be provided by the client and can be used to send push notifications to the device. The value should be a valid token obtained from the respective push notification service (APNS for iOS devices and FCM for Android devices). The server can use this token to send targeted notifications to the device when needed. It is important to ensure that the token is securely stored on the client side and transmitted securely to the server to prevent unauthorized access.
	/// </remarks>
	public required string PushNotificationToken { get; set; }
}

public abstract class DeviceFullContract : DeviceContract
{
	/// <summary>
	/// Unique identifier for the device, e.g. UUID generated and stored in device secure storage.
	/// </summary>
	[Required]
	public required string DeviceId { get; set; }

	/// <summary>
	/// The device model name, e.g. "iPhone 14 Pro".
	/// </summary>
	/// <remarks>
	/// This is used for informational purposes and is not required to be unique. It can be provided by the client or derived from the device identifier (e.g. using a device info service). The value is expected to be in a human-readable format that can be displayed in the UI. It is not expected to follow a specific format or naming convention, but it should provide enough information to identify the type of device being registered. For example, it could include the manufacturer and model, such as "Apple iPhone 14 Pro" or "Samsung Galaxy S22". The exact content and format of this field can be determined based on the requirements of the application and the information available from the client.
	/// </remarks>
	[Required]
	public required string ModelName { get; set; }

	///<summary>
	/// The device platform, e.g. "iOS" or "Android".
	///</summary>
	[Required]
	public required DevicePlatform Platform { get; set; }

	/// <summary>
	/// The name of the device's operating system, e.g. "iOS 16.4" or "Android 13".
	/// </summary>
	public required string SystemName { get; set; }
}

/// <summary>
/// General response to Device requests.
/// </summary>
public class DeviceResponse : DeviceFullContract, IResourceResponse
{
	/// <summary>
	/// An optional description for the device.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Indicates whether the device is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Device ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }

	///<summary>
	/// The date and time when the device was last active.
	///</summary>
	[Required]
	public required DateTimeOffset LastActiveAt { get; set; }

	/// <summary>
	/// The ID of the profile the device belongs to.
	/// </summary>
	[Required]
	public required string ProfileId { get; set; }
}

/// <summary>
/// Request to update a Device.
/// </summary>
public class DeviceUpdateRequest : DeviceContract, IResourceUpdateRequest
{
	public string? Description { get; set; }
}

/// <summary>
/// Provides methods for managing (Users') Devices.
/// </summary>
public interface IDeviceService : IResourceOperationsWithUpdateOnly<DeviceResponse, DeviceUpdateRequest>
{
	/// <summary>
	/// Gets the push notification token for the device.
	/// </summary>
	/// <param name="id">The ID of the device.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The push notification token for the device.</returns>
	Task<string> GetPushNotificationToken(string id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Registers a new device or updates an existing device with new values.
	/// </summary>
	/// <remarks>
	/// If a device with the same DeviceId already exists for the user, it will be updated with the new values provided in the request. If no such device exists, a new device will be created and registered to the user's profile. This method allows clients to easily register their devices for push notifications and keep their information up to date without needing to manage separate create and update operations.
	/// </remarks>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<DeviceResponse> Register(DeviceCreateOrUpdateRequest request, CancellationToken cancellationToken = default);
}
