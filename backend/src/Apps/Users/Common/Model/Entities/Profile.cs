using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Shared.Users.Model.Entities;

public class Profile : EntityWithName
{
	/// <summary>
	/// The user's email address.
	/// </summary>
	[Required]
	public required string Email { get; set; }

	/// <summary>
	/// The user's phone number.
	/// </summary>
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// The user's display name.
	/// </summary>
#pragma warning disable CA1056 // URI-like properties should not be strings
	public string? PhotoUrl { get; set; }
#pragma warning restore CA1056 // URI-like properties should not be strings
}
