using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Shared.Users.Model.Entities;

public class Device : EntityWithName
{
	/// <summary>
	/// The device's APNs token.
	/// </summary>
	public string? APNSToken { get; set; }

	/// <summary>
	/// The device's FCM token.
	/// </summary>
	[Required]
	public required string FCMToken { get; set; }

	/// <summary>
	/// The device's platform (iOS, Android, etc.).
	/// </summary>
	[Required]
	public required string Platform { get; set; }

	/// <summary>
	/// The device's associated Profile Id.
	/// </summary>
	[Required]
	public required string ProfileId { get; set; }

	/// <summary>
	/// The device's last active timestamp.
	/// </summary>
	[Required]
	public required DateTimeOffset LastActiveAt { get; set; }
}
