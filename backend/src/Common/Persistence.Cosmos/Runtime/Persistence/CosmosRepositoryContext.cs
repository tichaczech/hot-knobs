using Microsoft.EntityFrameworkCore;

namespace thc.HotKnobs.Runtime.Persistence;

public abstract class CosmosRepositoryContext : RepositoryContext
{
	protected CosmosRepositoryContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
