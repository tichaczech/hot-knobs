using System.Runtime.CompilerServices;

using Asp.Versioning;

using Fand.Runtime.Mapping;

using Mediator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;

using thc.HotKnobs.Domains.Dummy.Commands;
using thc.HotKnobs.Domains.Dummy.Contracts.v2;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;
using thc.HotKnobs.Runtime.Server.Controllers;

namespace thc.HotKnobs.Domains.Dummy.Server.Controllers.v2;

// internal class AuthorODataController : ODataController
// {
// 	private readonly IAuthorUseCases _service;

// 	public AuthorODataController(IAuthorUseCases service)
// 	{
// 		_service = service;
// 	}

// 	[EnableQuery]
// 	public IActionResult Get()
// 	{
// 		return Ok(_service.ListAsync(null, null, true, CancellationToken.None));
// 	}

// 	[EnableQuery]
// 	public async Task<IActionResult> GetAsync(string key)
// 	{
// 		var author = await _service.GetAsync(key);
// 		if (author == null)
// 			return NotFound();

// 		return Ok(author);
// 	}
// }

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
[ApiVersion("2.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/authors")]
internal class AuthorController : EntityControllerWithCreateAndUpdate<Author, AuthorCreateModel, AuthorUpdateModel, AuthorResponse, AuthorCreateRequest, AuthorUpdateRequest, AuthorCreateCommand, AuthorDeleteCommand, AuthorUpdateCommand, AuthorGetByIdQuery, AuthorListQuery>, IAuthorService
{
	public AuthorController(ILogger<AuthorController> _logger, IMapper mapper, IMediator mediator) : base(_logger, mapper, mediator)
	{
	}

	/// <inheritdoc />
	/// <response code="201">Author was created successfully.</response>
	/// <response code="400">Author creation request is invalid.</response>
#if !ANONYMOUS
	[Authorize(Policy = "author-write")]
#endif
	[HttpPost(Name = "AuthorCreate")]
	public override ValueTask<AuthorResponse> Create(AuthorCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.Create(request, cancellationToken);
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
	public override ValueTask Delete([FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Delete(id, etag, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Author was not modified.</response>
	/// <response code="404">Author was not found.</response>
	/// <response code="409">Author is not active (if requested onlyActive record).</response>
	[HttpGet("{id}", Name = "AuthorGet")]
	public override ValueTask<AuthorResponse> Get([FromRoute] string id, [FromHeader(Name = "If-None-Match")] string? etag = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.Get(id, etag, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Any author were modified since requested date.</response>
	[HttpGet(Name = "AuthorList")]
	public override IAsyncEnumerable<dynamic> List([FromQuery] string? columns = default, [FromQuery] string? filter = null, [FromQuery] bool onlyActive = true, [FromQuery] string? orderBy = null, [FromQuery] string? paginationToken = null, [FromQuery] string? search = null, [FromQuery] string? synchronizationToken = null, CancellationToken cancellationToken = default)
	{
		// var builder = new ODataConventionModelBuilder();
		// _ = builder.EntitySet<Author>("Authors");
		// var model = builder.GetEdmModel();

		// var ctx = new ODataQueryContext(model, model.FindDeclaredType("thc.HotKnobs.Domains.Dummy.Model.Entities.Author"), null);
		// var ops = new ODataQueryOptions<Author>(ctx, Request);
		// var qry = Enumerable.Empty<Author>().AsQueryable();
		// var qrz = ops.ApplyTo(qry);
		// var exp = qrz.Expression;

		// var parser = new ODataUriParser(model, new Uri("/thc.HotKnobs.Domains.Dummy.Model.Entities.Author" + Request.QueryString, UriKind.Relative));
		// var filter = parser.ParseFilter();

		return base.List(columns, filter, onlyActive, orderBy, paginationToken, search, synchronizationToken, cancellationToken);
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
	public override ValueTask<AuthorResponse> Update([FromBody] AuthorUpdateRequest request, [FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Update(request, id, etag, cancellationToken);
	}
}
