using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

/// <summary>
/// Represents a patron model with basic information.
/// </summary>
public abstract class PatronModel
{
	/// <summary>
	/// The first name of the patron.
	/// </summary>
	[Required]
	public string? FirstName { get; set; }

	/// <summary>
	/// The last name of the patron.
	/// </summary>
	[Required]
	public string? LastName { get; set; }

	/// <summary>
	/// Date of birth of the patron.
	/// </summary>
	[Required]
	public DateTimeOffset DateOfBirth { get; set; }
}

/// <summary>
/// Represents a model used for creating a patron.
/// </summary>
public class PatronCreateModel : PatronModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating a patron.
/// </summary>
public class PatronUpdateModel : PatronModel, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing patrons.
/// </summary>
public interface IPatronUseCases : IEntityUseCases<Patron, PatronCreateModel, PatronUpdateModel>
{
	/// <summary>
	/// Retrieves a list of patrons based on the specified criteria.
	/// </summary>
	/// <param name="query">Optional. Filters the list to include only patrons matching the specified query.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only patrons modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active patrons in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the IDs of patrons that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
