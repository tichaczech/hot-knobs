using System.Security.Claims;

namespace thc.HotKnobs.Runtime.Security;

/// <summary>
/// Provides an interface for retrieving the current user's claims principal.
/// </summary>
public interface IUserProvider
{
	/// <summary>
	/// Gets the current user's claims principal.
	/// </summary>
	/// <returns>The current user's claims principal, or null if the user is not authenticated.</returns>
	ClaimsPrincipal? GetCurrentUser();
}
