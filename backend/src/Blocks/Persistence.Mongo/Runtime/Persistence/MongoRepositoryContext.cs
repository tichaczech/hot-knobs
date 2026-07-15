using Microsoft.EntityFrameworkCore;

namespace thc.HotKnobs.Runtime.Persistence;

public abstract class MongoRepositoryContext : RepositoryContext
{
	protected MongoRepositoryContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
