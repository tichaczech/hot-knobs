using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Domains.Dummy.Contracts.v1;

/// <summary>
/// Represents a book.
/// </summary>
public abstract class BookContract
{
	/// <summary>
	/// Tha author of the book
	/// </summary>
	[Required]
	public string? AuthorId { get; set; }

	/// <summary>
	/// The Name of the book
	/// </summary>
	[Required]
	public string? Name { get; set; }

	/// <summary>
	/// The original price of the book.
	/// </summary>
	[Required]
	public decimal OriginalPrice { get; set; }

	/// <summary>
	/// The year the book was published.
	/// </summary>
	[Required]
	public short PublishYear { get; set; }
}

/// <summary>
/// Request to create a book.
/// </summary>
public class BookCreateOrUpdateRequest : BookContract, IResourceCreateOrUpdateRequest { }

/// <summary>
/// General response to book requests.
/// </summary>
public class BookResponse : BookContract, IResourceResponse
{
	/// <summary>
	/// Indicates whether the book is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Book ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }

	/// <summary>
	/// The date the book was purchased.
	/// </summary>
	[Required]
	public DateTimeOffset PurchasedOn { get; set; }
}

/// <summary>
/// Provides methods for managing books.
/// </summary>
public interface IBookService : IResourceOperationsWithCreateOrUpdate<BookResponse, BookCreateOrUpdateRequest>
{
	/// <summary>
	/// Retrieves a list of books based on the specified criteria.
	/// </summary>
	/// <param name="authorId">The ID of the author to filter the books by.</param>
	/// <param name="query">The query string to filter the books.</param>
	/// <param name="modifiedSince">Limits the resulting list to only books that have been modified since.</param>
	/// <param name="onlyActive">Limits the resulting list to only active books.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of books that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? authorId = default, string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
