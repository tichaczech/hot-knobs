using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public record ListResponse<TRepresentation>
	where TRepresentation : class, IModel
{
	public IAsyncEnumerable<TRepresentation> Items { get; init; } = Array.Empty<TRepresentation>().ToAsyncEnumerable();

	public string PagingToken { get; init; }

	public string SynchronizationToken { get; init; }

	public ListResponse(IAsyncEnumerable<TRepresentation> items, string pagingToken, string synchronizationToken)
	{
		Items = items;
		PagingToken = pagingToken;
		SynchronizationToken = synchronizationToken;
	}
}
