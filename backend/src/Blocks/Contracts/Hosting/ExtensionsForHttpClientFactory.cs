// using System.Text.Json;
// using System.Text.Json.Serialization;

// using Microsoft.Extensions.DependencyInjection;

// using Polly;
// using Polly.Extensions.Http;

// using Refit;

// namespace thc.HotKnobs.Contracts.Hosting;

// public static class ExtensionsForHttpClientFactory
// {
// 	public static IServiceCollection AddConfiguredRefitClient<T>(this IServiceCollection services, string address, IAsyncPolicy<HttpResponseMessage> policy) where T : class
// 	{
// 		ArgumentNullException.ThrowIfNull(services);
// 		ArgumentException.ThrowIfNullOrEmpty(address);
// 		ArgumentNullException.ThrowIfNull(policy);

// 		var jsonOptions = new JsonSerializerOptions
// 		{
// 			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
// 		};
// 		jsonOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));

// 		var rc = services.AddRefitClient<T>(new RefitSettings
// 		{
// 			ContentSerializer = new SystemTextJsonContentSerializer(jsonOptions)
// 		});

// 		_ = rc.ConfigureHttpClient(client => client.BaseAddress = new Uri(address));
// 		// _ = rc.AddHttpMessageHandler<TokenDelegatingHandler>()
// 		_ = rc.AddPolicyHandler(policy);

// 		return services;
// 	}

// 	public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy(this IServiceCollection services, ServiceDiscoveryConfiguration config)
// 	{
// 		ArgumentNullException.ThrowIfNull(services);
// 		ArgumentNullException.ThrowIfNull(config);

// 		return HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(config.RetryCount, retryAttempt => TimeSpan.FromSeconds(config.RetrySeconds));
// 	}
// }
