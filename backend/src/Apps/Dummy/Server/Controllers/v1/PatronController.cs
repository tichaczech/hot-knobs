using Asp.Versioning;

using Fand.Runtime.Mapping;

using Mediator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Domains.Dummy.Commands;
using thc.HotKnobs.Domains.Dummy.Contracts.v1;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;
using thc.HotKnobs.Runtime.Server.Controllers;

namespace thc.HotKnobs.Domains.Dummy.Server.Controllers.v1;

/// <inheritdoc />
/// <response code="401">There was en error authentication user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="500">There was a general error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
#if !ANONYMOUS
[Authorize(Policy = "patron-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
[ApiVersion("1.0")]
// [ApiExplorerSettings(GroupName = "v2")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/patrons")]
internal class PatronController(ILogger<PatronController> _logger, IMapper mapper, IMediator mediator) : EntityControllerWithCreateAndUpdate<Patron, PatronCreateModel, PatronUpdateModel, PatronResponse, PatronCreateRequest, PatronUpdateRequest, PatronCreateCommand, PatronDeleteCommand, PatronUpdateCommand, PatronGetByIdQuery, PatronListQuery>(_logger, mapper, mediator), IPatronService
{
	/// <inheritdoc />
	/// <response code="201">Patron was created successfully.</response>
	/// <response code="400">Patron creation request is invalid.</response>
#if !ANONYMOUS
	[Authorize(Policy = "patron-write")]
#endif
	[HttpPost(Name = "PatronCreate")]
	public override ValueTask<PatronResponse> Create(PatronCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.Create(request, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="204">Patron was deleted successfully.</response>
	/// <response code="404">Patron was not found.</response>
	/// <response code="409">Patron is not active.</response>
	/// <response code="412">Patron was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "patron-write")]
#endif
	[HttpDelete("{id}", Name = "PatronDelete")]
	public override ValueTask Delete([FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Delete(id, etag, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Patron was found.</response>
	/// <response code="304">Patron was not modified.</response>
	/// <response code="404">Patron was not found.</response>
	/// <response code="409">Patron is not active.</response>
	[HttpGet("{id}", Name = "PatronGet")]
	public override ValueTask<PatronResponse> Get([FromRoute] string id, [FromHeader(Name = "If-None-Match")] string? etag = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.Get(id, etag, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Any patron were modified since requested date.</response>
	[HttpGet(Name = "PatronList")]
	public override IAsyncEnumerable<dynamic> List([FromQuery] string? columns = default, [FromQuery] string? filter = null, [FromQuery] bool onlyActive = true, [FromQuery] string? orderBy = null, [FromQuery] string? paginationToken = null, [FromQuery] string? search = null, [FromQuery] string? synchronizationToken = null, CancellationToken cancellationToken = default)
	{
		return base.List(columns, filter, onlyActive, orderBy, paginationToken, search, synchronizationToken, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Patron was updated successfully.</response>
	/// <response code="400">Patron update request is invalid.</response>
	/// <response code="404">Patron was not found.</response>
	/// <response code="409">Patron is not active.</response>
	/// <response code="412">Patron was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "patron-write")]
#endif
	[HttpPatch("{id}", Name = "PatronUpdate")]
	public override ValueTask<PatronResponse> Update([FromBody] PatronUpdateRequest request, [FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Update(request, id, etag, cancellationToken);
	}
}
