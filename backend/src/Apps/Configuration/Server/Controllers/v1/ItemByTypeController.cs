using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using mojeEUC.Shared.Configuration.Model.Entities;
using mojeEUC.Shared.Configuration.Services;
using mojeEUC.Shared.Configuration.Contracts.v1;
using mojeEUC.Model;
using mojeEUC.Runtime;
using mojeEUC.Runtime.Server.Filters;
using Asp.Versioning;

namespace mojeEUC.Shared.Configuration.Server.Controllers.v1;

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
[Route("v{version:apiVersion}/{type:alpha}")]
public class ItemByTypeController(ILogger<ItemController> _logger, IMapper _mapper, Services.IItemService _service, IContextAccessor _contextAccessor) : ControllerBase, Contracts.v1.IItemByTypeService
{
	/// <inheritdoc />
	[HttpPut("{name:alpha}", Name = "ItemByTypeCreateOrUpdate")]
	[Authorize(Policy = "item-write")]
	public async Task<ItemByTypeResponse> CreateOrUpdateAsync(string type, string name, ItemByTypeCreateOrUpdateRequest request, CancellationToken cancellationToken = default)
	{
		Item? item = null;
		try
		{
			item = await _service.GetAsync(type, name, cancellationToken: cancellationToken);

			if (BaseFilter.TryGetValueFromHeaderAsDateTimeOffset(this.ControllerContext, HeaderNames.IfUnmodifiedSince, out DateTimeOffset lastModified))
				_contextAccessor.SetLastModifiedValue(item.Id, LastModifiedValueSource.External, lastModified);

			var model = _mapper.Map<ItemUpdateModel>(request);
			model.Name = name;
			model.Type = type;

			item = await _service.UpdateAsync(item.Id, model, cancellationToken);
		}
		catch(EntityNotFoundException)
		{
			var model = _mapper.Map<ItemCreateModel>(request);
			model.Name = name;
			model.Type = type;
			item = await _service.CreateAsync(default, model, cancellationToken);

			Response.StatusCode = StatusCodes.Status201Created;
		}

		return _mapper.Map<ItemByTypeResponse>(item);
	}

	/// <inheritdoc />
	[HttpDelete("{name:alpha}", Name = "ItemByTypeDelete")]
	[Authorize(Policy = "item-write")]
	public async Task DeleteAsync(string type, string name, CancellationToken cancellationToken = default)
	{
		var item = await GetAsync(type, name, true, cancellationToken);

		if (BaseFilter.TryGetValueFromHeaderAsDateTimeOffset(this.ControllerContext, HeaderNames.IfUnmodifiedSince, out DateTimeOffset lastModified))
			_contextAccessor.SetLastModifiedValue(item.Id, LastModifiedValueSource.External, lastModified);

		await _service.DeleteAsync(item.Id , cancellationToken);
	}

	/// <inheritdoc />
	[HttpGet("{name:alpha}", Name = "ItemByTypeGet")]
	public async Task<ItemByTypeResponse> GetAsync(string type, string name, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		var entity = await _service.GetAsync(type, name, onlyActive, cancellationToken);

		return _mapper.Map<ItemByTypeResponse>(entity);
	}

	/// <inheritdoc />
	[HttpGet(Name = "ItemByTypeList")]
#if ASYNC_ENUMERABLE
	public IAsyncEnumerable<string> ListAsync(string type, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, [FromQuery] int? skip = null, [FromQuery] int? limit = null, CancellationToken cancellationToken = default)
	{
		return _service.ListAsync(type, x => x.Name, modifiedSince, onlyActive, skip, limit, cancellationToken);
#else
	public async Task<IEnumerable<string>> ListAsync(string type, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, [FromQuery] int? skip = null, [FromQuery] int? limit = null, CancellationToken cancellationToken = default)
	{
		return _service.ListAsync(type, x => x.Name, modifiedSince, onlyActive, skip, limit, cancellationToken).ToEnumerable();
#endif
	}
}
