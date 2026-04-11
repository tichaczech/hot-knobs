using System.Linq.Expressions;

using AutoMapper;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
#pragma warning disable CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning disable CA1311 // Specify a culture or use an invariant version

/// <inheritdoc />
public class AuthorUseCases : EntityUseCases<Author, AuthorCreateModel, AuthorUpdateModel>, IAuthorUseCases
{
	private readonly IRepository<Author> _repository;

	public AuthorUseCases(ILogger<AuthorUseCases> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) => _repository = repository;

	/// <inheritdoc />
	public async Task<Author?> GetByExternalIdAsync(string externalId, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		var author = await _repository.FindAsync(author => author.ExternalId == externalId, cancellationToken);
		if (author == default)
			return default;

		EntityNotActiveException.ThrowIfNotTrue(author!, a => onlyActive && a.IsActive || !onlyActive);

		return author;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		Expression<Func<Author, bool>>? where = default;

		if (!String.IsNullOrEmpty(query))
			where = author => author.FirstName.ToLower().Contains(query.ToLower()) || author.LastName.ToLower().Contains(query.ToLower());

		return ListAsync(where, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(AuthorCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(AuthorUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}

#pragma warning restore CA1311 // Specify a culture or use an invariant version
#pragma warning restore CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning restore CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
