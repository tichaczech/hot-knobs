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
[Authorize(Policy = "reservation-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
[ApiVersion("1.0")]
// [ApiExplorerSettings(GroupName = "v2")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/reservations")]
internal class ReservationController : EntityControllerWithCreateAndUpdate<Reservation, ReservationCreateModel, ReservationUpdateModel, ReservationResponse, ReservationCreateRequest, ReservationUpdateRequest, ReservationCreateCommand, ReservationDeleteCommand, ReservationUpdateCommand, ReservationGetByIdQuery, ReservationListQuery>, IReservationService
{
	public ReservationController(ILogger<ReservationController> logger, IMapper mapper, IMediator mediator) : base(logger, mapper, mediator)
	{
	}

	/// <inheritdoc />
	/// <response code="202">Reservation was requested and will be created in background.</response>
	/// <response code="400">Reservation creation request is invalid.</response>
	/// <response code="410">One of the following problems occurred: <br/>
	/// - Book does not exists or is not active <br/>
	/// - Patron does not exists or is not active <br/>
	/// </response>
#if !ANONYMOUS
	[Authorize(Policy = "reservation-write")]
#endif
	[HttpPost(Name = "ReservationCreate")]
	public override ValueTask<ReservationResponse> Create(ReservationCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.Create(request, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="204">Reservation was deleted successfully.</response>
	/// <response code="404">Reservation was not found.</response>
	/// <response code="409">Reservation is not active.</response>
	/// <response code="412">Reservation was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "reservation-write")]
#endif
	[HttpDelete("{id}", Name = "ReservationDelete")]
	public override ValueTask Delete([FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Delete(id, etag, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Reservation was found.</response>
	/// <response code="304">Reservation was not modified.</response>
	/// <response code="404">Reservation was not found.</response>
	/// <response code="409">Reservation is not active.</response>
	[HttpGet("{id}", Name = "ReservationGet")]
	public override ValueTask<ReservationResponse> Get([FromRoute] string id, [FromHeader(Name = "If-None-Match")] string? etag = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.Get(id, etag, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Any reservation were modified since requested date.</response>
	[HttpGet(Name = "ReservationList")]
	public override IAsyncEnumerable<dynamic> List([FromQuery] string? columns = default, [FromQuery] string? filter = null, [FromQuery] bool onlyActive = true, [FromQuery] string? orderBy = null, [FromQuery] string? paginationToken = null, [FromQuery] string? search = null, [FromQuery] string? synchronizationToken = null, CancellationToken cancellationToken = default)
	{
		return base.List(columns, filter, onlyActive, orderBy, paginationToken, search, synchronizationToken, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Reservation was updated successfully.</response>
	/// <response code="400">Reservation update request is invalid.</response>
	/// <response code="404">Reservation was not found.</response>
	/// <response code="409">Reservation is not active.</response>
	/// <response code="412">Reservation was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "reservation-write")]
#endif
	[HttpPatch("{id}", Name = "ReservationUpdate")]
	public override ValueTask<ReservationResponse> Update([FromBody] ReservationUpdateRequest request, [FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		return base.Update(request, id, etag, cancellationToken);
	}
}
