using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Shared.Users.Contracts.v1;
using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Shared.Users.UseCases;
using thc.HotKnobs.Runtime.Security;
using thc.HotKnobs.Runtime.Server.Controllers;

using AM = AutoMapper;

namespace thc.HotKnobs.Shared.Users.Server.Controllers.v1;

/// <inheritdoc cref="ControllerBase" />
/// <response code="401">There was an error authenticating user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="500">General error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
#if !ANONYMOUS
[Authorize]
#else
[AllowAnonymous]
#endif
[ApiController]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/my/devices")]
[Tags("My Devices")]
internal class MyDeviceController : EntityControllerWithUpdateOnly<Device, DeviceCreateModel, DeviceUpdateModel, MyDeviceResponse, MyDeviceUpdateRequest, IDeviceUseCases>, IMyDeviceService
{
	private readonly IUserProvider _userProvider;

	public MyDeviceController(ILogger<MyDeviceController> logger, AM.IMapper mapper, IDeviceUseCases useCases, IUserProvider userProvider) : base(logger, mapper, useCases)
	{
		_userProvider = userProvider;
	}

	/// <inheritdoc />
	/// <response code="204">Device was deleted successfully.</response>
	/// <response code="404">Device was not found.</response>
	/// <response code="409">Device is not active.</response>
	/// <response code="412">Device was modified by another request.</response>
	[HttpDelete("{id}", Name = "MyDeviceDelete")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Device get operation completed successfully..</response>
	/// <response code="304">Device was not modified.</response>
	/// <response code="404">Device was not found.</response>
	/// <response code="409">Device is not active (if requested onlyActive record).</response>
	[HttpGet("{id}", Name = "MyDeviceGet")]
	public override Task<MyDeviceResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="200">Device list operation completed successfully..</response>
	/// <response code="304">No devices were modified since requested date.</response>
	[HttpGet(Name = "MyDeviceList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? query = default, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		var uid = _userProvider.GetCurrentUser()?.Identity?.Name ?? throw new InvalidOperationException("User identity is not set.");

		return UseCases.ListAsync(query, uid, modifiedSince, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Device registration was created or updated successfully.</response>
	/// <response code="400">Device create or update request is invalid.</response>
	[HttpPut("register", Name = "MyDeviceRegister")]
	public async Task<MyDeviceResponse> Register(MyDeviceRegisterRequest request, CancellationToken cancellationToken = default)
	{
		var uid = _userProvider.GetCurrentUser()?.Identity?.Name ?? throw new InvalidOperationException("User identity is not set.");

		var model = Mapper.Map<DeviceRegisterModel>(request);
		var device = await UseCases.RegisterAsync(uid, model, cancellationToken);
		var response = Mapper.Map<MyDeviceResponse>(device);

		return response;
	}

	/// <inheritdoc />
	/// <response code="200">Device was updated successfully.</response>
	/// <response code="400">Device update request is invalid.</response>
	/// <response code="404">Device was not found.</response>
	/// <response code="409">Device is not active.</response>
	/// <response code="412">Device was modified by another request.</response>
	[HttpPatch("{id}", Name = "MyDeviceUpdate")]
	public override Task<MyDeviceResponse> UpdateAsync(string id, MyDeviceUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
