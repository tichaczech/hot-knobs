using Asp.Versioning;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Domains.Dummy.Contracts.v1;
using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.UseCases;
using thc.HotKnobs.Runtime.Server.Controllers;

namespace thc.HotKnobs.Domains.Dummy.Server.Controllers.v1;

/// <inheritdoc cref="ControllerBase" />
/// <response code="401">There was en error authentication user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="500">There was a general error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
#if !ANONYMOUS
[Authorize(Policy = "author-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
// [ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/authors")]
internal class AuthorController(ILogger<AuthorController> logger, IMapper mapper, IAuthorUseCases service)
	: EntityControllerWithCreateAndUpdate<Author, AuthorCreateModel, AuthorUpdateModel, AuthorResponse, AuthorCreateRequest, AuthorUpdateRequest, IAuthorUseCases>(logger, mapper, service), IAuthorService
{
	/// <inheritdoc />
	/// <response code="201">Author was created successfully.</response>
	/// <response code="400">Author creation request is invalid.</response>
#if !ANONYMOUS
	[Authorize(Policy = "author-write")]
#endif
	[HttpPost(Name = "AuthorCreate")]
	public override Task<AuthorResponse> CreateAsync(AuthorCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.CreateAsync(request, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="204">Author was deleted successfully.</response>
	/// <response code="404">Author was not found.</response>
	/// <response code="409">Author is not active.</response>
	/// <response code="412">Author was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "author-write")]
#endif
	[HttpDelete("{id}", Name = "AuthorDelete")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Author was not modified.</response>
	/// <response code="404">Author was not found.</response>
	/// <response code="409">Author is not active (if requested onlyActive record).</response>
	[HttpGet("{id}", Name = "AuthorGet")]
	public override Task<AuthorResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="304">Any author were modified since requested date.</response>
	[HttpGet(Name = "AuthorList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? query = null, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(query, modifiedSince, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Author was updated successfully.</response>
	/// <response code="400">Author update request is invalid.</response>
	/// <response code="404">Author was not found.</response>
	/// <response code="409">Author is not active.</response>
	/// <response code="412">Author was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "author-write")]
#endif
	[HttpPatch("{id}", Name = "AuthorUpdate")]
	public override Task<AuthorResponse> UpdateAsync(string id, AuthorUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
