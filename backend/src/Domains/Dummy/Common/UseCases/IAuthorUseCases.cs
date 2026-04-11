using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

/// <summary>
/// Represents a author model with basic information.
/// </summary>
public abstract class AuthorModel
{
	/// <summary>
	/// The external id of the author.
	/// </summary>
	public string? ExternalId { get; set; }

	/// <summary>
	/// The first name of the author.
	/// </summary>
	[Required]
	public string? FirstName { get; set; }

	/// <summary>
	/// The last name of the author.
	/// </summary>
	[Required]
	public string? LastName { get; set; }
}

/// <summary>
/// Represents a model used for creating an author.
/// Inherits from <see cref="AuthorModel"/> and <see cref="ICreateModel"/>.
/// </summary>
public class AuthorCreateModel : AuthorModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating an author.
/// Inherits from <see cref="AuthorModel"/> and <see cref="IUpdateModel"/>.
/// </summary>
public class AuthorUpdateModel : AuthorModel, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing authors.
/// </summary>
public interface IAuthorUseCases : IEntityUseCases<Author, AuthorCreateModel, AuthorUpdateModel>
{
	/// <summary>
	/// Retrieves an author by the specified external id.
	/// </summary>
	/// <param name="externalId">The external id of the author to retrieve.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active authors in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>The Author with the specified external id, if found; otherwise, <c>null</c>.</returns>
	Task<Author?> GetByExternalIdAsync(string externalId, bool onlyActive = true, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of authors based on the specified criteria.
	/// </summary>
	/// <param name="query">Optional. Filters the list to include only authors matching the specified query.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only authors modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active authors in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the ids of authors that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
