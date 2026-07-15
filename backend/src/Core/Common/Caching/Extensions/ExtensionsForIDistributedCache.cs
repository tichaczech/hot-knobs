using System.Text;
using System.Text.Json;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Caching.Distributed;

namespace Fand.Runtime.Caching;

/// <summary>
/// Provides extension methods for the <see cref="IDistributedCache"/> interface.
/// </summary>
public static class ExtensionsForIDistributedCache
{
	/// <summary>
	/// Gets the value associated with the specified key as type T from the distributed cache.
	/// </summary>
	/// <typeparam name="T">The type of the value to get.</typeparam>
	/// <param name="cache">The distributed cache instance.</param>
	/// <param name="key">The key of the value to get.</param>
	/// <returns>The value associated with the specified key as type T, or default(T) if the key is not found.</returns>
	public static T? GetAs<T>(this IDistributedCache cache, string key)
	{
		Guard.IsNotNull(cache);

		var value = cache.Get(key);
		if (null == value)
			return default;

		var json = Encoding.UTF8.GetString(value);
		return JsonSerializer.Deserialize<T>(json);
	}

	/// <summary>
	/// Asynchronously gets the value associated with the specified key as type T from the distributed cache.
	/// </summary>
	/// <typeparam name="T">The type of the value to get.</typeparam>
	/// <param name="cache">The distributed cache instance.</param>
	/// <param name="key">The key of the value to get.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the value associated with the specified key as type T, or default(T) if the key is not found.</returns>
	public static async Task<T?> GetAsAsync<T>(this IDistributedCache cache, string key, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(cache);

		var value = await cache.GetAsync(key, cancellationToken);
		if (null == value)
			return default;

		var json = Encoding.UTF8.GetString(value);
		return JsonSerializer.Deserialize<T>(json);
	}

	/// <summary>
	/// Sets the value associated with the specified key as type T in the distributed cache.
	/// </summary>
	/// <typeparam name="T">The type of the value to set.</typeparam>
	/// <param name="cache">The distributed cache instance.</param>
	/// <param name="key">The key of the value to set.</param>
	/// <param name="value">The value to set.</param>
	/// <param name="options">The cache entry options.</param>
	public static void SetAs<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions? options = default)
	{
		Guard.IsNotNull(cache);

		var json = JsonSerializer.Serialize(value);
		var bytes = Encoding.UTF8.GetBytes(json);

		if (null == options)
		{
			cache.Set(key, bytes);
			return;
		}

		cache.Set(key, bytes, options);
	}

	/// <summary>
	/// Asynchronously sets the value associated with the specified key as type T in the distributed cache.
	/// </summary>
	/// <typeparam name="T">The type of the value to set.</typeparam>
	/// <param name="cache">The distributed cache instance.</param>
	/// <param name="key">The key of the value to set.</param>
	/// <param name="value">The value to set.</param>
	/// <param name="options">The cache entry options.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public static Task SetAsAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions? options = default, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(cache);

		var json = JsonSerializer.Serialize(value);
		var bytes = Encoding.UTF8.GetBytes(json);

		if (null == options)
			return cache.SetAsync(key, bytes, cancellationToken);

		return cache.SetAsync(key, bytes, options, cancellationToken);
	}
}
