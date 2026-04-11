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
[Authorize(Policy = "book-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
// [ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/books")]
internal class BookController(ILogger<BookController> _logger, IMapper mapper, IBookUseCases service)
	: EntityControllerWithCreateOrUpdate<Book, BookCreateModel, BookUpdateModel, BookResponse, BookCreateOrUpdateRequest, IBookUseCases>(_logger, mapper, service), IBookService
{
	/// <inheritdoc />
	/// <response code="200">Book was updated successfully.</response>
	/// <response code="201">Book was created successfully.</response>
	/// <response code="400">Book creation request is invalid.</response>
	/// <response code="409">Book is not active.</response>
	/// <response code="410">One of the following problems occurred: <br/>
	/// - Author does not exists or is not active <br/>
	/// </response>
	/// <response code="412">Book was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "book-write")]
#endif
	[HttpPut("{id}", Name = "BookCreateOrUpdate")]
	public override Task<BookResponse> CreateOrUpdateAsync(string id, BookCreateOrUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.CreateOrUpdateAsync(id, request, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="204">Book was deleted successfully.</response>
	/// <response code="404">Book was not found.</response>
	/// <response code="409">Book is not active.</response>
	/// <response code="412">Book was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "book-write")]
#endif
	[HttpDelete("{id}", Name = "BookDelete")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Author was found.</response>
	/// <response code="304">Book was not modified.</response>
	/// <response code="404">Book was not found.</response>
	/// <response code="409">Book is not active (if requested onlyActive record).</response>
	[HttpGet("{id}", Name = "BookGet")]
	public override Task<BookResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="304">Any book were modified since requested date.</response>
	[HttpGet(Name = "BookList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? authorId = default, [FromQuery] string? query = default, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(query, authorId, modifiedSince, onlyActive, cancellationToken);
	}
}
