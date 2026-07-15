namespace thc.HotKnobs.Runtime.Configuration;

public class PersistenceConfiguration
{
	/// <summary>
	/// The connection string for the persistence target.
	/// </summary>
	public required string ConnectionString { get; set; }

	/// <summary>
	/// The name of the database in the persistence target.
	/// </summary>
	public required string DatabaseName { get; set; }
}
