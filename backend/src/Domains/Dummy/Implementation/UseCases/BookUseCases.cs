using System.Linq.Expressions;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
#pragma warning disable CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning disable CA1311 // Specify a culture or use an invariant version

/// <summary>
/// Book service.
/// </summary>
public class BookUseCases : EntityUseCases<Book, BookCreateModel, BookUpdateModel>, IBookUseCases
{
	private readonly IRepository<Book> _repository;

	private readonly IAuthorUseCases _authorUseCases;

	public BookUseCases(ILogger<BookUseCases> logger, IMapper mapper, IRepository<Book> repository, IAuthorUseCases authorUseCases) : base(logger, mapper, repository)
	{
		_repository = repository;
		_authorUseCases = authorUseCases;
	}

	/// <inheritdoc />
	public Task<Book?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken = default)
	{
		return _repository.AsQueryable().SingleOrDefaultAsync(book => book.ExternalId == externalId, cancellationToken);
	}

	public IAsyncEnumerable<string> ListAsync(string? query = default, string? authorId = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		Expression<Func<Book, bool>>? where = default;

		if (!String.IsNullOrEmpty(query))
			where = book => book.Name.ToLower().Contains(query.ToLower());

		if (!String.IsNullOrEmpty(authorId))
			where = book => book.AuthorId == authorId;

		return ListAsync(where, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	protected override async Task ValidateAsync(BookCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		try
		{
			_ = await _authorUseCases.GetAsync(model?.AuthorId!, true, cancellationToken);

		}
		catch (EntityException ex) when (ex is EntityNotFoundException or EntityNotActiveException)
		{
			EntityNotFoundOrNotActiveException.Throw<Author>(null, model?.AuthorId!);
		}
	}

	protected override Task ValidateAsync(BookUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}
