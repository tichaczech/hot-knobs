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
public interface IAuthorService : IResourceOperationsWithCreateAndUpdate<AuthorResponse, AuthorCreateRequest, AuthorUpdateRequest> { }
