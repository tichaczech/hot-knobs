using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Azure.Monitor.OpenTelemetry.Exporter;

using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Runtime.Configuration;
using thc.HotKnobs.Runtime.Security;
using thc.HotKnobs.Runtime.Worker;

using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// <see cref="IHostApplicationBuilder"/> implementation for Runtime.Server.
/// </summary>
public class WorkerSetupHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		// Add common services
		_ = builder.Services.AddSingleton<IConcurrencyTokenContext, ConcurrencyTokenContext>();
		_ = builder.Services.AddSingleton<IConcurrencyTokenContextAccessor>(x => x.GetRequiredService<IConcurrencyTokenContext>());
		_ = builder.Services.AddSingleton<IUserProvider, NullUserProvider>();


		_ = builder.Services.Configure<JsonSerializerOptions>(options =>
		{
			options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			options.Converters.Add(new UpperCaseEnumConverter());
		});

		return builder;
	}
}

public class UpperCaseEnumConverter : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		return typeToConvert?.IsEnum ?? false;
	}

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var converterType = typeof(UpperCaseEnumConverterInner<>).MakeGenericType(typeToConvert);

		return (JsonConverter)Activator.CreateInstance(converterType)!;
	}

	private class UpperCaseEnumConverterInner<T> : JsonConverter<T> where T : struct, Enum
	{
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumValue = reader.GetString();

			return Enum.TryParse(enumValue, true, out T result) ? result : throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString().ToUpper(CultureInfo.CurrentCulture));
		}
	}
}
