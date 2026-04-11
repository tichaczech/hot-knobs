using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Domains.Dummy.Contracts.v2;

/// <summary>
/// Represents an author.
/// </summary>
public abstract class AuthorContract
{
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

	/// <summary>
	/// The nationality of the author.
	/// </summary>
	public string? Nationality { get; set; }
}

/// <summary>
/// Request to create an author.
/// </summary>
public class AuthorCreateRequest : AuthorContract, IResourceCreateRequest { }

/// <summary>
/// General response to author requests.
/// </summary>
public class AuthorResponse : AuthorContract, IResourceResponse
{
	/// <summary>
	/// Indicates whether the author is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Author ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }

}

/// <summary>
/// Request to update an author.
/// </summary>
public class AuthorUpdateRequest : AuthorContract, IResourceUpdateRequest { }

/// <summary>
/// Provides methods for managing authors.
/// </summary>
public interface IAuthorService : IResourceOperationsWithCreateAndUpdate<AuthorResponse, AuthorCreateRequest, AuthorUpdateRequest>
{
	/// <summary>
	/// Retrieves a list of authors based on the specified criteria.
	/// </summary>
	/// <param name="query">The query string to filter the authors.</param>
	/// <param name="modifiedSince">Limits the resulting list to only authors that have been modified since.</param>
	/// <param name="onlyActive">Limits the resulting list to only active authors.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of authors that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
