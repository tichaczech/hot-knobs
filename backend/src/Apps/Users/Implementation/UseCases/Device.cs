using System.Linq.Expressions;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;
using thc.HotKnobs.Runtime;

using Fand.Runtime;

namespace thc.HotKnobs.Shared.Users.UseCases;

#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
#pragma warning disable CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning disable CA1311 // Specify a culture or use an invariant version

/// <inheritdoc />
public class DeviceUseCases : EntityUseCases<Device, DeviceCreateModel, DeviceUpdateModel>, IDeviceUseCases
{
	private readonly IConcurrencyTokenContext _ctContext;

	private readonly TimeProvider _timeProvider;

	/// <inheritdoc />
	public DeviceUseCases(ILogger<DeviceUseCases> logger, AutoMapper.IMapper mapper, IConcurrencyTokenContext ctContext, IRepository<Device> repository, TimeProvider timeProvider) : base(logger, mapper, repository)
	{
		_ctContext = ctContext;
		_timeProvider = timeProvider;
	}

	public async Task<Device> RegisterAsync(string profileId, DeviceRegisterModel model, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(model);

		var now = _timeProvider.GetUtcNow();
		var device = await Repository.FindAsync(x => x.FCMToken == model.FCMToken, cancellationToken);
		if (device != null)
		{
			// if (device.APNSToken != model.APNSToken || device.Name != model.Name || device.Platform != model.Platform || device.ProfileId != profileId)
			if (device.ProfileId != profileId) // APSN token, FCM token, device name and platform can be updated by the same device, so we should not consider them as a conflict. The only conflict is if the same FCM token is registered to a different profile.
				throw new InvalidOperationException("Device information does not match! Device with the same FCM token has been already registered with a different profile.");

			var updateModel = Mapper.Map<DeviceUpdateModel>(device);
			updateModel.LastActiveAt = now;

			// Faking concurrency token to ensure that device will be updated. This operation is idempotent, so we can ignore concurrency conflicts here.
			_ctContext.SetValue(device.Id, ConcurrencyTokenValueSource.External, new ETag(device.ETag));
			return await UpdateAsync(device.Id, updateModel, cancellationToken);
		}

		var createModel = Mapper.Map<DeviceCreateModel>(model);
		createModel.LastActiveAt = now;
		createModel.ProfileId = profileId;
		return await CreateAsync(default, createModel, cancellationToken);
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(DeviceCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
	/// <inheritdoc />
	protected override Task ValidateAsync(DeviceUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}

#pragma warning restore CA1311 // Specify a culture or use an invariant version
#pragma warning restore CA1304 // Specify CultureInfo for ToLower to ensure consistent behavior across cultures
#pragma warning restore CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
