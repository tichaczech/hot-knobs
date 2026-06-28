using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Shared.Users.Contracts.v1;

/// <summary>
/// Represents a User's own Profile.
/// </summary>
public abstract class MyProfileContract
{
	public string? Description { get; set; }

	/// <summary>
	/// The user's display name.
	/// </summary>
	[Required]
	public required string DisplayName { get; set; }

	/// <summary>
	/// The user's email address.
	/// </summary>
	[Required]
	public required string Email { get; set; }

	///<summary>
	/// The user's phone number.
	///</summary>
	public string? PhoneNumber { get; set; }

	///<summary>
	/// The user's photo URL.
	///</summary>
	public Uri? PhotoUrl { get; set; }
}

/// <summary>
/// Represents a User's Profile creation or update request.
/// </summary>
public class MyProfileCreateOrUpdateRequest : ProfileContract, ISingletonResourceCreateOrUpdateRequest { }

/// <summary>
/// Represents a User's Profile creation or update request.
/// </summary>
public class MyProfileResponse : ProfileContract, ISingletonResourceResponse
{
	/// <summary>
	/// Indicates whether the Profile is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Profile ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }
}

public interface IMyProfileService : ISingletonResourceOperations<MyProfileResponse, MyProfileCreateOrUpdateRequest> { }
