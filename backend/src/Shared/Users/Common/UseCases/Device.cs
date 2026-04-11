using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Model;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Shared.Users.UseCases;

/// <summary>
/// Represents a Device model with basic information.
/// </summary>
public abstract class DeviceModel : ModelWithName
{
	/// <summary>
	/// The device's Apple Push Notification service Token.
	/// </summary>
	public string? APNSToken { get; set; }

	///<summary>
	/// The device's Firebase Cloud Messaging Token.
	///</summary>
	public required string FCMToken { get; set; }

	///<summary>
	/// The device's platform.
	///</summary>
	public required string Platform { get; set; }

	/// <summary>
	/// The device's owning user Id.
	/// </summary>
	public required string ProfileId { get; set; }

	///<summary>
	/// The date and time when the device was last active.
	///</summary>
	public required DateTimeOffset LastActiveAt { get; set; }
}

/// <summary>
/// Represents a model used for creating a Device.
/// Inherits from <see cref="DeviceModel"/> and <see cref="ICreateModel"/>.
/// </summary>
public class DeviceCreateModel : DeviceModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating a Device.
/// Inherits from <see cref="DeviceModel"/> and <see cref="IUpdateModel"/>.
/// </summary>
public class DeviceUpdateModel : DeviceModel, IEntityUpdateModel { }

public class DeviceRegisterModel : DeviceModel { }

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
