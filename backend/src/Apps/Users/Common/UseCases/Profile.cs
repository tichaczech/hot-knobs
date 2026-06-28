using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Model;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Shared.Users.UseCases;

/// <summary>
/// Represents a Profile model with basic information.
/// </summary>
public abstract class ProfileModel : ModelWithName
{
	/// <summary>
	/// The User's email address.
	/// </summary>
	public required string Email { get; set; }

	///<summary>
	/// The User's phone number.
	///</summary>
	public string? PhoneNumber { get; set; }

	///<summary>
	/// The User's photo URL.
	///</summary>
	public Uri? PhotoUrl { get; set; }
}

/// <summary>
/// Represents a model used for creating a Profile.
/// Inherits from <see cref="ProfileModel"/> and <see cref="ICreateModel"/>.
/// </summary>
public class ProfileCreateModel : ProfileModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating a Profile.
/// Inherits from <see cref="ProfileModel"/> and <see cref="IUpdateModel"/>.
/// </summary>
public class ProfileUpdateModel : ProfileModel, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing Profiles.
/// </summary>
public interface IProfileUseCases : IEntityUseCases<Profile, ProfileCreateModel, ProfileUpdateModel>
{
	/// <summary>
	/// Retrieves a list of Profiles based on the specified criteria.
	/// </summary>
	/// <param name="query">Optional. Filters the list to include only Profiles matching the specified query.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only Profiles modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active Profiles in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the ids of Profiles that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
