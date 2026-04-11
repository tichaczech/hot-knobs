using Microsoft.EntityFrameworkCore;

namespace thc.HotKnobs.Runtime.Persistence;

public abstract class PostgresRepositoryContext : RepositoryContext
{
	protected PostgresRepositoryContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
