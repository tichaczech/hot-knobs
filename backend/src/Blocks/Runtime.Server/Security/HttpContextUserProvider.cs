using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace thc.HotKnobs.Runtime.Security;

/// <summary>
/// <see cref="IUserProvider"/> implementation for server Runtime.Server.
/// </summary>
public class HttpContextUserProvider : IUserProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public HttpContextUserProvider(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

	/// <inheritdoc />
	public ClaimsPrincipal? GetCurrentUser()
	{
		// NOTE: We expect that the HttpContext is always set => we don't check for null.
		return _httpContextAccessor.HttpContext!.User;
	}
}
