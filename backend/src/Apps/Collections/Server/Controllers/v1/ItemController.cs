using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mojeEUC.Runtime.Server.Controllers;
using mojeEUC.Shared.Collections.Model.Entities;
using mojeEUC.Shared.Collections.Services;
using mojeEUC.Shared.Collections.Contracts.v1;
using Asp.Versioning;

namespace mojeEUC.Shared.Collections.Server.Controllers.v1;

/// <inheritdoc cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// <response code="401">There was en error authentication user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="404">Requested resource was not found.</response>
/// <response code="412">Requested resource was modified since last read.</response>
/// <response code="500">There was a general error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
/// <response code="503">Service is not available.</response>
[ApiController]
[ApiVersion("1.0")]
[Authorize(Policy = "item-read")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/items")]
public class ItemController(ILogger<ItemController> _logger, IMapper mapper, Services.IItemService service)
	: EntityControllerWithCreateOrUpdate<Item, ItemCreateModel, ItemUpdateModel, ItemResponse, ItemCreateOrUpdateRequest, Services.IItemService>(_logger, mapper, service), Contracts.v1.IItemService
{
	/// <inheritdoc />
	[HttpPut(Name = "ItemCreateOrUpdate")]
	[Authorize(Policy = "item-write")]
	public override Task<ItemResponse> CreateOrUpdateAsync(string id, ItemCreateOrUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.CreateOrUpdateAsync(id, request, cancellationToken);
	}

	/// <inheritdoc />
	[HttpDelete("{id:guid}", Name = "ItemDelete")]
	[Authorize(Policy = "item-write")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	[HttpGet("{id:guid}", Name = "ItemGet")]
	public override Task<ItemResponse> GetAsync(string id, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	[HttpGet(Name = "ItemList")]
#if ASYNC_ENUMERABLE
	public IAsyncEnumerable<string> ListAsync(string? type = null, DateTimeOffset? modifiedSince = null, bool onlyActive = true, int? skip = null, int? limit = null, CancellationToken cancellationToken = default)
	{
		return Service.ListAsync(type, default, modifiedSince, onlyActive, skip, limit, cancellationToken);
#else
	public async Task<IEnumerable<string>> ListAsync(string? type = null, DateTimeOffset? modifiedSince = null, bool onlyActive = true, int? skip = null, int? limit = null, CancellationToken cancellationToken = default)
	{
		return Service.ListAsync(type, default, modifiedSince, onlyActive, skip, limit, cancellationToken).ToEnumerable();
#endif
	}

	/// <inheritdoc />
#if ASYNC_ENUMERABLE
	public virtual IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
#else
	public override Task<IEnumerable<string>> ListAsync(DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default)
#endif
	{
		throw new NotImplementedException();
	}
}
