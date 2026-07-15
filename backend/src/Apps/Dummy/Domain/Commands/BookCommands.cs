using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Domains.Dummy.Models;

namespace thc.HotKnobs.Domains.Dummy.Commands;

/// <summary>
/// Represents a book model with basic information.
/// </summary>
public abstract record BookModel
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

public record BookCreateModel : BookModel;

public record BookUpdateModel : BookModel;

public record BookCreateCommand : EntityCreateCommand<BookCreateModel, Book>;

public record BookDeleteCommand : EntityDeleteCommand;

public record BookUpdateCommand : EntityUpdateCommand<BookUpdateModel, Book>;
