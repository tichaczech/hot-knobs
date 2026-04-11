using Microsoft.EntityFrameworkCore;

namespace thc.HotKnobs.Runtime.Persistence;

/// <summary>
/// Base class for repository contexts.
/// </summary>
public abstract class RepositoryContext : DbContext
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryContext"/> class.
	/// </summary>
	/// <param name="options">The options to be used by this context.</param>
	protected RepositoryContext(DbContextOptions options) : base(options) { }
}
