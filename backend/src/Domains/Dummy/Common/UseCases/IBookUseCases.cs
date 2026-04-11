using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

/// <summary>
/// Represents a book model with basic information.
/// </summary>
public abstract class BookModel
{
	/// <summary>
	/// The author of the book.
	/// </summary>
	[Required]
	public string? AuthorId { get; set; }

	/// <summary>
	/// The external id of the book.
	/// </summary>
	public string? ExternalId { get; set; }

	/// <summary>
	/// The Name of the book.
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
/// Represents a model used for creating a book.
/// Inherits from <see cref="BookModel"/> and <see cref="ICreateModel"/>.
/// </summary>
public class BookCreateModel : BookModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating an book.
/// Inherits from <see cref="BookModel"/> and <see cref="IUpdateModel"/>.
/// </summary>
public class BookUpdateModel : BookModel, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing books.
/// </summary>
public interface IBookUseCases : IEntityUseCases<Book, BookCreateModel, BookUpdateModel>
{
	/// <summary>
	/// Retrieves an book by the specified external id.
	/// </summary>
	/// <param name="externalId">The external id of the book to retrieve.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>The Book with the specified external id, if found; otherwise, <c>null</c>.</returns>
	Task<Book?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of books based on the specified criteria.
	/// </summary>
	/// <param name="query">Optional. Filters the list to include only books that match the specified query.</param>
	/// <param name="authorId">Optional. Filters the list to include only books of this author.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only books modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active books in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the IDs of books that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? query = default, string? authorId = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
