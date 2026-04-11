using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Shared.Users.Runtime.Persistence;

public sealed class CosmosUsersContext(DbContextOptions<CosmosUsersContext> options) : CosmosRepositoryContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntityWithName<Device>("devices", e =>
		{
			_ = e.Property(a => a.APNSToken).ToJsonProperty("apnsToken");
			_ = e.Property(a => a.FCMToken).ToJsonProperty("fcmToken").IsRequired();
			_ = e.Property(a => a.Platform).ToJsonProperty("platform").IsRequired();
			_ = e.Property(a => a.ProfileId).ToJsonProperty("profileId").IsRequired();
			_ = e.Property(a => a.LastActiveAt).ToJsonProperty("lastActiveAt").IsRequired();
		});

		_ = modelBuilder.AddEntityWithName<Profile>("profiles", e =>
		{
			_ = e.Property(a => a.Email).ToJsonProperty("email").IsRequired();
			_ = e.Property(a => a.PhoneNumber).ToJsonProperty("phone_number");
			_ = e.Property(a => a.PhotoUrl).ToJsonProperty("photo_url");
		});
	}
}
