namespace thc.HotKnobs.Queries;

public record ListResponse
{
	public IAsyncEnumerable<dynamic> Items { get; init; } = Array.Empty<dynamic>().ToAsyncEnumerable();

	public ulong MaxPageSize { get; init; }

	public string PaginationToken { get; init; }

	public string SynchronizationToken { get; init; }

	public ListResponse(IAsyncEnumerable<dynamic> items, string paginationToken, string synchronizationToken, ulong maxPageSize = 100)
	{
		Items = items;
		PaginationToken = paginationToken;
		SynchronizationToken = synchronizationToken;
		MaxPageSize = maxPageSize;
	}
}
