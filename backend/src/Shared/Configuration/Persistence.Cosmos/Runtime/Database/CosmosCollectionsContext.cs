using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using mojeEUC.Shared.Configuration.Model.Entities;
using mojeEUC.Runtime.Database;
using System.Text.Json;

namespace mojeEUC.Shared.Configuration.Runtime.Database;

public sealed class CosmosConfigurationContext(DbContextOptions<CosmosConfigurationContext> options) : CosmosRepositoryContext(options)
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static IDictionary<string, object>? Deserialize(string? value) => value != null ? JsonSerializer.Deserialize<IDictionary<string, object>>(value) : null;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static string? Serialize(IDictionary<string, object>? value) => value != null && value.Any() ? JsonSerializer.Serialize(value) : null;

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.AddEntity<Item>("items", e =>
		{
			e.HasPartitionKey(a => a.Type);

			e.Property(a => a.Data).ToJsonProperty("data").HasConversion(
			 		v => Serialize(v),
			 		v => Deserialize(v)
			 	);
			e.Property(a => a.DeviceId).ToJsonProperty("deviceId");
			e.Property(a => a.Name).ToJsonProperty("name");
			e.Property(a => a.PatientId).ToJsonProperty("patientId");
			e.Property(a => a.Type).ToJsonProperty("type");
			e.Property(a => a.UserId).ToJsonProperty("userId");
		});
	}
}
