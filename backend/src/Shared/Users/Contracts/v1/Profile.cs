using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Shared.Users.Contracts.v1;

/// <summary>
/// Represents a (User's) Profile.
/// </summary>
public abstract class ProfileContract : MyProfileContract { }

/// <summary>
/// General response to Profile requests.
/// </summary>
public class ProfileResponse : ProfileContract, IResourceResponse
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

/// <summary>
/// Request to update a Profile.
/// </summary>
public class ProfileUpdateRequest : ProfileContract, IResourceUpdateRequest { }

/// <summary>
/// Provides methods for managing (Users') Profiles.
/// </summary>
public interface IProfileService : IResourceOperationsWithUpdateOnly<ProfileResponse, ProfileUpdateRequest>
{
	/// <summary>
	/// Retrieves a list of Profiles based on the specified criteria.
	/// </summary>
	/// <param name="query">The query string to filter the Profiles.</param>
	/// <param name="modifiedSince">Limits the resulting list to only Profiles that have been modified since.</param>
	/// <param name="onlyActive">Limits the resulting list to only active Profiles.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of Profiles that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
