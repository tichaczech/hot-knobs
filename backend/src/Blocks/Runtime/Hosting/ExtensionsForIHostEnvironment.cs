using Microsoft.Extensions.Hosting;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// Extension methods for <see cref="IHostEnvironment" />.
/// </summary>
public static class ExtensionsForIHostEnvironment
{
	internal static string ENVIRONMENT_NAME_LOCAL = "local";

	/// <summary>
	/// Determines whether the current hosting environment name is Local.
	/// </summary>
	/// <param name="hostEnvironment">The <see cref="IHostEnvironment"/> instance.</param>
	/// <returns><c>true</c> if the current hosting environment name is Local; otherwise, <c>false</c>.</returns>
	public static bool IsLocal(this IHostEnvironment hostEnvironment) =>
		hostEnvironment?.EnvironmentName.Equals(ENVIRONMENT_NAME_LOCAL, StringComparison.OrdinalIgnoreCase) ?? false;
}
