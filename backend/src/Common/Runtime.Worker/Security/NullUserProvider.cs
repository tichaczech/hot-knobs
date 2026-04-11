using System.Security.Claims;

namespace thc.HotKnobs.Runtime.Security;

// TODO: Create proper User Provider
public class NullUserProvider : IUserProvider
{
	public ClaimsPrincipal? GetCurrentUser() => null;
}

