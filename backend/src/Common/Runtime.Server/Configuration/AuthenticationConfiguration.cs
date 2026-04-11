using Fand.Runtime.Configuration;

namespace thc.HotKnobs.Runtime.Configuration;

public class AuthenticationConfiguration : IOptionsBase
{
	public string? Audience { get; set; }

	public string? Authority { get; set; }

#pragma warning disable CA1056 // URI-like properties should not be strings
	public string? MetadataUrl { get; set; }
#pragma warning restore CA1056 // URI-like properties should not be strings

	public string? Issuer { get; set; }

	public bool RequireAudience { get; set; } = true;

	public bool RequireExpirationTime { get; set; } = true;

	public bool RequireSignedTokens { get; set; } = true;

	public IDictionary<string, string> Scopes { get; } = new Dictionary<string, string>();

	public bool ValidateActor { get; set; }

	public bool ValidateIssuer { get; set; } = true;

	public bool ValidateIssuerSigningKey { get; set; }

	public bool ValidateLifetime { get; set; } = true;

	public bool ValidateAudience { get; set; } = true;
}
