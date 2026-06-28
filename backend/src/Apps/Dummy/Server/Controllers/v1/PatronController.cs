using Asp.Versioning;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Domains.Dummy.Contracts.v1;
using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.UseCases;
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
internal class PatronController(ILogger<PatronController> _logger, IMapper mapper, IPatronUseCases service)
	: EntityControllerWithCreateAndUpdate<Patron, PatronCreateModel, PatronUpdateModel, PatronResponse, PatronCreateRequest, PatronUpdateRequest, IPatronUseCases>(_logger, mapper, service), IPatronService
{
	/// <inheritdoc />
	/// <response code="201">Patron was created successfully.</response>
	/// <response code="400">Patron creation request is invalid.</response>
#if !ANONYMOUS
	[Authorize(Policy = "patron-write")]
#endif
	[HttpPost(Name = "PatronCreate")]
	public override Task<PatronResponse> CreateAsync(PatronCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.CreateAsync(request, cancellationToken);
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
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Patron was found.</response>
	/// <response code="304">Patron was not modified.</response>
	/// <response code="404">Patron was not found.</response>
	/// <response code="409">Patron is not active.</response>
	[HttpGet("{id}", Name = "PatronGet")]
	public override Task<PatronResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="304">Any patron were modified since requested date.</response>
	[HttpGet(Name = "PatronList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? query = default, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(query, modifiedSince, onlyActive, cancellationToken);
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
	public override Task<PatronResponse> UpdateAsync(string id, PatronUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
