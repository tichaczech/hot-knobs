using Asp.Versioning;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Domains.Dummy.Contracts.v1;
using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.UseCases;
using thc.HotKnobs.Runtime.Server.Controllers;

using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi;

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
internal class LoanController(ILogger<LoanController> _logger, IMapper mapper, ILoanUseCases service)
	: EntityControllerWithCreateAndUpdate<Loan, LoanCreateModel, LoanUpdateModel, LoanResponse, LoanCreateRequest, LoanUpdateRequest, ILoanUseCases>(_logger, mapper, service), ILoanService
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
	public async Task<LoanResponse> CreateAsync(string reservationId, CancellationToken cancellationToken = default)
	{
		var entity = await UseCases.CreateAsync(reservationId, cancellationToken);

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
	public override Task<LoanResponse> CreateAsync(LoanCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.CreateAsync(request, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="204">Loan was deleted successfully.</response>
	/// <response code="404">Loan was not found.</response>
	/// <response code="412">Loan was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "loan-write")]
#endif
	[HttpDelete("{id}", Name = "LoanDelete")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Loan was found.</response>
	/// <response code="304">Loan was not modified.</response>
	/// <response code="404">Loan was not found.</response>
	[HttpGet("{id}", Name = "LoanGet")]
	public override Task<LoanResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="304">Any loan were modified since requested date.</response>
	[HttpGet(Name = "LoanList")]
	public IAsyncEnumerable<string> ListAsync(string? bookId = null, DateTimeOffset? dueOn = null, DateTimeOffset? loanInProgressOn = null, DateTimeOffset? loanedOn = null, DateTimeOffset? overdueOn = null, string? patronId = null, string? reservationId = null, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, bool onlyActive = true, CancellationToken cancellationToken = default)
	// public IAsyncEnumerable<string> ListAsync([FromQuery] string? bookId = default, [FromQuery] DateTimeOffset? dueOn = default, [FromQuery] DateTimeOffset? loanInProgressOn = default, [FromQuery] DateTimeOffset? loanedOn = default, [FromQuery] DateTimeOffset? overdueOn = default, [FromQuery] string? patronId = default, [FromQuery] string? reservationId = default, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(bookId, dueOn, loanInProgressOn, loanedOn, overdueOn, patronId, reservationId, modifiedSince, onlyActive, cancellationToken);
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
	public override Task<LoanResponse> UpdateAsync(string id, LoanUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
