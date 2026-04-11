using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Shared.Users.Contracts.v1;

/// <summary>
/// General response to Device requests.
/// </summary>
public class DeviceResponse : MyDeviceResponse
{
	[Required]
	public required string ProfileId { get; set; }
}

/// <summary>
/// Request to update a Device.
/// </summary>
public class DeviceUpdateRequest : MyDeviceUpdateRequest { }

/// <summary>
/// Provides methods for managing (Users') Devices.
/// </summary>
public interface IDeviceService : IResourceOperationsWithUpdateOnly<DeviceResponse, DeviceUpdateRequest>
{
	/// <summary>
	/// Retrieves a list of Devices based on the specified criteria.
	/// </summary>
	/// <param name="query">The query string to filter the Devices.</param>
	/// <param name="profileId">The Profile ID to filter the Devices.</param>
	/// <param name="modifiedSince">Limits the resulting list to only Devices that have been modified since.</param>
	/// <param name="onlyActive">Limits the resulting list to only active Devices.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of Devices that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, string? profileId = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
