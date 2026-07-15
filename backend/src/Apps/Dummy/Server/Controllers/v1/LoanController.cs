using Asp.Versioning;

using Fand.Runtime.Mapping;

using Mediator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.Filters;

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
[Authorize(Policy = "loan-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
// [ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/loans")]
internal class LoanController(ILogger<LoanController> _logger, IMapper mapper, IMediator mediator) : EntityControllerWithCreateAndUpdate<Loan, LoanCreateModel, LoanUpdateModel, LoanResponse, LoanCreateRequest, LoanUpdateRequest, LoanCreateCommand, LoanDeleteCommand, LoanUpdateCommand, LoanGetByIdQuery, LoanListQuery>(_logger, mapper, mediator), ILoanService
{
	/// <inheritdoc />
	/// <response code="201">Loan was created successfully.</response>
	/// <response code="410">One of the following problems occurred: <br/>
	/// - Book does not exists or is not active <br/>
	/// - Patron does not exists or is not active <br/>
	/// - Reservation does not exists or is not active <br/>
	/// </response>
#if !ANONYMOUS
	[Authorize(Policy = "loan-write")]
#endif
	[HttpPost("{reservationId}", Name = "LoanCreateFromReservation")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[SwaggerResponseHeader(201, "Last-Modified", JsonSchemaType.String, "Last modified date of the resource")]
	public async ValueTask<LoanResponse> Create(string reservationId, CancellationToken cancellationToken = default)
	{
		var entity = await Mediator.Send(new LoanCreateFromReservationCommand { ReservationId = reservationId }, cancellationToken);

		Response.StatusCode = StatusCodes.Status201Created;
		return Mapper.Map<LoanResponse>(entity);
	}

	/// <inheritdoc />
	/// <response code="201">Loan was created successfully.</response>
	/// <response code="400">Loan creation request is invalid.</response>
	/// <response code="409">Book is already reserved for requested period.</response>
	/// <response code="410">One of the following problems occurred: <br/>
	/// - Book does not exists or is not active <br/>
	/// - Patron does not exists or is not active <br/>
	/// </response>
#if !ANONYMOUS
	[Authorize(Policy = "loan-write")]
#endif
	[HttpPost(Name = "LoanCreate")]
	public override ValueTask<LoanResponse> Create(LoanCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.Create(request, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="204">Loan was deleted successfully.</response>
	/// <response code="404">Loan was not found.</response>
	/// <response code="412">Loan was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "loan-write")]
#endif
	[HttpDelete("{id}", Name = "LoanDelete")]
	public override ValueTask Delete([FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Delete(id, etag, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Loan was found.</response>
	/// <response code="304">Loan was not modified.</response>
	/// <response code="404">Loan was not found.</response>
	[HttpGet("{id}", Name = "LoanGet")]
	public override ValueTask<LoanResponse> Get([FromRoute] string id, [FromHeader(Name = "If-None-Match")] string? etag = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.Get(id, etag, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Any loan were modified since requested date.</response>
	[HttpGet(Name = "LoanList")]
	public override IAsyncEnumerable<dynamic> List([FromQuery] string? columns = default, [FromQuery] string? filter = null, [FromQuery] bool onlyActive = true, [FromQuery] string? orderBy = null, [FromQuery] string? paginationToken = null, [FromQuery] string? search = null, [FromQuery] string? synchronizationToken = null, CancellationToken cancellationToken = default)
	{
		return base.List(columns, filter, onlyActive, orderBy, paginationToken, search, synchronizationToken, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Loan was updated successfully.</response>
	/// <response code="400">Loan update request is invalid.</response>
	/// <response code="404">Loan was not found.</response>
	/// <response code="409">Loan is not active.</response>
	/// <response code="412">Loan was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "loan-write")]
#endif
	[HttpPatch("{id}", Name = "LoanUpdate")]
	public override ValueTask<LoanResponse> Update([FromBody] LoanUpdateRequest request, [FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Update(request, id, etag, cancellationToken);
	}
}
