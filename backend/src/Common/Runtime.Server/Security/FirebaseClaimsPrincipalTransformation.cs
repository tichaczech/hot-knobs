using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;

namespace thc.HotKnobs.Runtime.Security;

public class FirebaseClaimsPrincipalTransformation : IClaimsTransformation
{
	public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	{
		ArgumentNullException.ThrowIfNull(principal);

		// Current principal cannot be modified, so we need to clone it.
		var newPrincipal = principal.Clone();
		var newIdentity = newPrincipal.Identity as ClaimsIdentity ?? throw new InvalidOperationException("ClaimsPrincipal.Identity is not a ClaimsIdentity.");
		newIdentity.Label = principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

		return Task.FromResult(newPrincipal);
	}
}
