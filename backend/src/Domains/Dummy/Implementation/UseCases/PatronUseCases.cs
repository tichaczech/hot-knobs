using System.Linq.Expressions;

using AutoMapper;

using Fand.Runtime;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

/// <summary>
/// Patron service.
/// </summary>
public class PatronUseCases : EntityUseCases<Patron, PatronCreateModel, PatronUpdateModel>, IPatronUseCases
{
	public PatronUseCases(ILogger<PatronUseCases> logger, IMapper mapper, IRepository<Patron> repository) : base(logger, mapper, repository)
	{
	}

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		// TODO: Add support for diacritics normalization
		Expression<Func<Patron, bool>> where = (patron => true);
		if (!String.IsNullOrEmpty(query))
			where = where.AndAlso(patron => patron.FirstName.StartsWith(query) || patron.LastName.StartsWith(query));

		return ListAsync(where, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	protected override Task ValidateAsync(PatronCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}

	protected override Task ValidateAsync(PatronUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}
