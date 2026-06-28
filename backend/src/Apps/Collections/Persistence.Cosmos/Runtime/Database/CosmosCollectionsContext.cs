using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Runtime.Persistence;
using thc.HotKnobs.Shared.Collections.Model.Entities;

namespace thc.HotKnobs.Shared.Collections.Runtime.Database;

public sealed class CosmosCollectionsContext(DbContextOptions<CosmosCollectionsContext> options) : CosmosRepositoryContext(options)
{
	// private static IDictionary<string, object>? Deserialize(string? value) => value != null ? JsonSerializer.Deserialize<IDictionary<string, object>>(value) : null;

	// private static string? Serialize(IDictionary<string, object>? value) => value != null && value.Any() ? JsonSerializer.Serialize(value) : null;

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntityWithName<Item>("items", e =>
		{
			_ = e.Property(a => a.Code).ToJsonProperty("code").IsRequired();
			_ = e.Property(a => a.Data).ToJsonProperty("data");
			_ = e.Property(a => a.Type).ToJsonProperty("type").IsRequired();
		});
	}
}
