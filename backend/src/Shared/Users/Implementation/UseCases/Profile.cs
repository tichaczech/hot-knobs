using System.Linq.Expressions;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Shared.Users.UseCases;

#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
#pragma warning disable CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning disable CA1311 // Specify a culture or use an invariant version

/// <inheritdoc />
public class ProfileUseCases : EntityUseCases<Profile, ProfileCreateModel, ProfileUpdateModel>, IProfileUseCases
{
	/// <inheritdoc />
	public ProfileUseCases(ILogger<ProfileUseCases> logger, AutoMapper.IMapper mapper, IRepository<Profile> repository) : base(logger, mapper, repository) { }

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		Expression<Func<Profile, bool>>? where = profile => true;

		if (!String.IsNullOrEmpty(query))
			where = profile => profile.Name.ToLower().Contains(query.ToLower());

		return ListAsync(where, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(ProfileCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(ProfileUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}

#pragma warning restore CA1311 // Specify a culture or use an invariant version
#pragma warning restore CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning restore CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
