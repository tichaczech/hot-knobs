using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Shared.Collections.Model.Entities;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Shared.Collections.Runtime.Persistence;

public sealed class MongoCollectionsContext(DbContextOptions<MongoCollectionsContext> options) : MongoRepositoryContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntityWithName<Item>("items", e =>
		{
			_ = e.Property(a => a.Code).HasElementName("code").IsRequired();
			_ = e.Property(a => a.Data).HasElementName("data");
			_ = e.Property(a => a.Type).HasElementName("type").IsRequired();
		});
	}
}
