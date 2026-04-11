using Asp.Versioning;

using AM = AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Shared.Users.UseCases;
using thc.HotKnobs.Runtime.Server.Controllers;
using thc.HotKnobs.Shared.Users.Contracts.v1;

namespace thc.HotKnobs.Shared.Users.Server.Controllers.v1;

/// <inheritdoc cref="ControllerBase" />
/// <response code="401">There was en error authentication user.</response>
/// <response code="403">User is not authorized to perform requested operation.</response>
/// <response code="500">There was a general error during request processing.</response>
/// <response code="501">Requested operation is not implemented.</response>
#if !ANONYMOUS
[Authorize(Policy = "profile-read")]
#else
[AllowAnonymous]
#endif
[ApiController]
// [ApiExplorerSettings(GroupName = "v2")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/profiles")]
[Tags("Profiles")]
internal class ProfileController : EntityControllerWithUpdateOnly<Profile, ProfileCreateModel, ProfileUpdateModel, ProfileResponse, ProfileUpdateRequest, IProfileUseCases>, IProfileService
{
	public ProfileController(ILogger<ProfileController> logger, AM.IMapper mapper, IProfileUseCases useCases) : base(logger, mapper, useCases)
	{
	}

	/// <inheritdoc />
	/// <response code="204">Profile was deleted successfully.</response>
	/// <response code="404">Profile was not found.</response>
	/// <response code="409">Profile is not active.</response>
	/// <response code="412">Profile was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "profile-write")]
#endif
	[HttpDelete("{id}", Name = "ProfileDelete")]
	public override Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="304">Profile was not modified.</response>
	/// <response code="404">Profile was not found.</response>
	/// <response code="409">Profile is not active (if requested onlyActive record).</response>
	[HttpGet("{id}", Name = "ProfileGet")]
	public override Task<ProfileResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return base.GetAsync(id, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	public override IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	/// <response code="304">Any profile were modified since requested date.</response>
	[HttpGet(Name = "ProfileList")]
	public IAsyncEnumerable<string> ListAsync([FromQuery] string? query = null, [FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = null, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(query, modifiedSince, onlyActive, cancellationToken);
	}

	/// <inheritdoc />
	/// <response code="200">Profile was updated successfully.</response>
	/// <response code="400">Profile update request is invalid.</response>
	/// <response code="404">Profile was not found.</response>
	/// <response code="409">Profile is not active.</response>
	/// <response code="412">Profile was modified by another request.</response>
#if !ANONYMOUS
	[Authorize(Policy = "profile-write")]
#endif
	[HttpPatch("{id}", Name = "ProfileUpdate")]
	public override Task<ProfileResponse> UpdateAsync(string id, ProfileUpdateRequest request, CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(id, request, cancellationToken);
	}
}
