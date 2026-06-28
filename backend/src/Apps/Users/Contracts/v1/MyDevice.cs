
// using System.ComponentModel.DataAnnotations;

// using thc.HotKnobs.Contracts;

// namespace thc.HotKnobs.Shared.Users.Contracts.v1;

// ///<summary>
// /// Represents a (User's) Device.
// ///</summary>
// public abstract class MyDeviceContract
// {
// 	public required string Name { get; set; }
// }

// ///<summary>
// /// Represents a (User's) Device.
// ///</summary>
// public abstract class MyDeviceFullContract : MyDeviceContract
// {
// 	///<summary>
// 	/// The device Apple Push Notification service Token.
// 	///</summary>
// 	public string? ApnsToken { get; set; }

// 	///<summary>
// 	/// The device Firebase Cloud Messaging Token.
// 	///</summary>
// 	public required string FcmToken { get; set; }

// 	///<summary>
// 	/// The device platform.
// 	///</summary>
// 	public required string Platform { get; set; }
// }

// /// <summary>
// /// Request to register a Device.
// /// </summary>
// public class MyDeviceRegisterRequest : MyDeviceFullContract, IResourceCreateRequest { }

// /// <summary>
// /// General response to Device requests.
// /// </summary>
// public class MyDeviceResponse : MyDeviceFullContract, IResourceResponse
// {
// 	/// <summary>
// 	/// An optional description for the device.
// 	/// </summary>
// 	public string? Description { get; set; }

// 	/// <summary>
// 	/// Indicates whether the device is active (or soft-deleted).
// 	/// </summary>
// 	[Required]
// 	public bool IsActive { get; set; }

// 	/// <summary>
// 	/// Device ID.
// 	/// </summary>
// 	[Required]
// 	public required string Id { get; set; }

// 	///<summary>
// 	/// The date and time when the device was last active.
// 	///</summary>
// 	public required DateTimeOffset LastActiveAt { get; set; }
// }

// /// <summary>
// /// Request to update a Device.
// /// </summary>
// public class MyDeviceUpdateRequest : MyDeviceContract, IResourceUpdateRequest
// {
// 	public string? Description { get; set; }
// }

// /// <summary>
// /// Provides methods for managing (Users') Devices.
// /// </summary>
// public interface IMyDeviceService : IResourceOperationsWithUpdateOnly<MyDeviceResponse, MyDeviceUpdateRequest>
// {
// 	/// <summary>
// 	/// Retrieves a list of Devices based on the specified criteria.
// 	/// </summary>
// 	/// <param name="query">The query string to filter the Devices.</param>
// 	/// <param name="modifiedSince">Limits the resulting list to only Devices that have been modified since.</param>
// 	/// <param name="onlyActive">Limits the resulting list to only active Devices.</param>
// 	/// <param name="cancellationToken">A cancellation token.</param>
// 	/// <returns>A collection representing the ids of Devices that match the specified criteria.</returns>
// 	IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);

// 	Task<MyDeviceResponse> Register(MyDeviceRegisterRequest request, CancellationToken cancellationToken = default);
// }
