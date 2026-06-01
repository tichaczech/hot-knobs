using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Filters;

using thc.HotKnobs.Contracts;
using thc.HotKnobs.Shared.Users.Contracts.v1;
using thc.HotKnobs.Shared.Users.UseCases;
using thc.HotKnobs.Model;
using thc.HotKnobs.Runtime.Security;

using am = AutoMapper;
using Microsoft.OpenApi;

namespace thc.HotKnobs.Shared.Users.Server.Controllers.v1;

/// <inheritdoc cref="ControllerBase" />
/// <response code="401">There was en error authentication user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="500">There was a general error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
#if !ANONYMOUS
[Authorize]
#else
[AllowAnonymous]
#endif
[ApiController]
// [ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/my/profile")]
[Tags("My Profile")]
internal class MyProfileController : ControllerBase, ISingletonResourceOperations<MyProfileResponse, MyProfileCreateOrUpdateRequest>
{
	/// <summary>
	/// AutoMapper.
	/// </summary>
	private readonly am.IMapper _mapper;

	/// <summary>
	/// Entity service.
	/// </summary>
	private readonly IProfileUseCases _useCases;

	private readonly IUserProvider _userProvider;

	/// <inheritdoc />
	public MyProfileController(ILogger<MyProfileController> logger, am.IMapper mapper, IProfileUseCases useCases, IUserProvider userProvider)
	{
		_mapper = mapper;
		_useCases = useCases;
		_userProvider = userProvider;
	}

	/// <inheritdoc />
	/// <response code="200">Profile was updated successfully.</response>
	/// <response code="201">Profile was created successfully.</response>
	/// <response code="400">Profile create or update request is invalid.</response>
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	[SwaggerResponseHeader(StatusCodes.Status201Created, "ETag", JsonSchemaType.String, "ETag of the resource")]
	[HttpPut(Name = "MyProfileCreateOrUpdate")]
	public async Task<MyProfileResponse> CreateOrUpdateAsync(MyProfileCreateOrUpdateRequest request, CancellationToken cancellationToken = default)
	{
		var uid = _userProvider.GetCurrentUser()?.Identity?.Name ?? throw new InvalidOperationException("User identity is not set.");

		MyProfileResponse response;
		int statusCode;
		try
		{
			var profile = await _useCases.GetAsync(uid, true, cancellationToken);
			var model = _mapper.Map<ProfileUpdateModel>(request);
			profile = await _useCases.UpdateAsync(uid, model, cancellationToken);

			response = _mapper.Map<MyProfileResponse>(profile);
			statusCode = StatusCodes.Status200OK;
		}
		catch (EntityNotFoundException)
		{
			var model = _mapper.Map<ProfileCreateModel>(request);
			var profile = await _useCases.CreateAsync(uid, model, cancellationToken);

			response = _mapper.Map<MyProfileResponse>(profile);
			statusCode = StatusCodes.Status201Created;
		}

		Response.StatusCode = statusCode;
		return response;
	}

	/// <inheritdoc />
	/// <response code="304">Profile was not modified.</response>
	/// <response code="404">Profile was not found.</response>
	/// <response code="409">Profile is not active (if requested onlyActive record).</response>
	[ProducesResponseType(StatusCodes.Status200OK)]
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	[HttpGet(Name = "MyProfileGet")]
	public async Task<MyProfileResponse> GetAsync(CancellationToken cancellationToken = default)
	{
		var uid = _userProvider.GetCurrentUser()?.Identity?.Name ?? throw new InvalidOperationException("User identity is not set.");

		var profile = await _useCases.GetAsync(uid, true, cancellationToken);
		var response = _mapper.Map<MyProfileResponse>(profile);

		return response;
	}
}
