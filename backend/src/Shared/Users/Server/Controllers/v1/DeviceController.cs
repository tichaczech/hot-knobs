using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Shared.Users.Contracts.v1;
using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Shared.Users.UseCases;
using thc.HotKnobs.Runtime.Server.Controllers;

using AM = AutoMapper;

namespace thc.HotKnobs.Shared.Users.Server.Controllers.v1;

/// <inheritdoc cref="ControllerBase" />
/// <response code="401">There was an error authenticating user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="500">General error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
#if !ANONYMOUS
[Authorize(Policy = "device-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/devices")]
[Tags("Devices")]
internal class DeviceController : EntityControllerWithUpdateOnly<Device, DeviceCreateModel, DeviceUpdateModel, DeviceResponse, DeviceUpdateRequest, IDeviceUseCases>, IDeviceService
{
	public DeviceController(ILogger<DeviceController> logger, AM.IMapper mapper, IDeviceUseCases useCases) : base(logger, mapper, useCases)
	{
	}

	/// <inheritdoc />
	/// <response code="204">Device was deleted successfully.</response>
	/// <response code="404">Device was not found.</response>
	/// <response code="409">Device is not active.</response>
	/// <response code="412">Device was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "device-write")]
#endif
	[HttpDelete("{id}", Name = "DeviceDelete")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Device was not modified.</response>
	/// <response code="404">Device was not found.</response>
	/// <response code="409">Device is not active (if requested onlyActive record).</response>
	[HttpGet("{id}", Name = "DeviceGet")]
	public override Task<DeviceResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <summary>List devices (IDs) filtered by optional query.</summary>
	/// <response code="304">No devices were modified since requested date.</response>
	[HttpGet(Name = "DeviceList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? query = default, [FromQuery] string? profileId = default, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(query, profileId, modifiedSince, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Device was updated successfully.</response>
	/// <response code="400">Device update request is invalid.</response>
	/// <response code="404">Device was not found.</response>
	/// <response code="409">Device is not active.</response>
	/// <response code="412">Device was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "device-write")]
#endif
	[HttpPatch("{id}", Name = "DeviceUpdate")]
	public override Task<DeviceResponse> UpdateAsync(string id, DeviceUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
