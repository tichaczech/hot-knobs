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
internal class ReservationController(ILogger<ReservationController> _logger, IMapper mapper, IReservationUseCases service)
	: EntityControllerWithCreateAndUpdate<Reservation, ReservationCreateModel, ReservationUpdateModel, ReservationResponse, ReservationCreateRequest, ReservationUpdateRequest, IReservationUseCases>(_logger, mapper, service), IReservationService
{
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
	public override Task<ReservationResponse> CreateAsync(ReservationCreateRequest request, CancellationToken cancellationToken = default)
	{
		return base.CreateAsync(request, cancellationToken);
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
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Reservation was found.</response>
	/// <response code="304">Reservation was not modified.</response>
	/// <response code="404">Reservation was not found.</response>
	/// <response code="409">Reservation is not active.</response>
	[HttpGet("{id}", Name = "ReservationGet")]
	public override Task<ReservationResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="304">Any reservation were modified since requested date.</response>
	[HttpGet(Name = "ReservationList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? bookId = default, [FromQuery] string? patronId = default, [FromQuery] DateTimeOffset? startsOn = null, [FromQuery] DateTimeOffset? endsOn = null, [FromQuery] DateTimeOffset? reservationInProgressOn = null, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(bookId, patronId, startsOn, endsOn, reservationInProgressOn, modifiedSince, onlyActive, cancellationToken);
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
	public override Task<ReservationResponse> UpdateAsync(string id, ReservationUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
