using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Shared.Collections.Model.Entities;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Shared.Collections.Runtime.Persistence;

public sealed class PostgresCollectionsContext : PostgresRepositoryContext
{
	public PostgresCollectionsContext() : base(new DbContextOptionsBuilder<PostgresRepositoryContext>().UseNpgsql("Host=postgres;Database=shared-collections;Username=postgres;Password=postgres").Options) { }

	public PostgresCollectionsContext(DbContextOptions<PostgresCollectionsContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntityWithName<Item>("items", e =>
		{
			_ = e.Property(a => a.Code).HasColumnName("code").IsRequired();
			_ = e.Property(a => a.Data).HasColumnName("data");
			_ = e.Property(a => a.Type).HasColumnName("type").IsRequired();
		});
	}
}
