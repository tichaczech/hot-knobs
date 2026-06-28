using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Shared.Users.Runtime.Persistence;

public sealed class MongoUsersContext(DbContextOptions<MongoUsersContext> options) : MongoRepositoryContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntityWithName<Device>("devices", e =>
		{
			_ = e.Property(a => a.APNSToken).HasElementName("apnsToken");
			_ = e.Property(a => a.FCMToken).HasElementName("fcmToken").IsRequired();
			_ = e.Property(a => a.Platform).HasElementName("platform").IsRequired();
			_ = e.Property(a => a.ProfileId).HasElementName("profileId").IsRequired();
			_ = e.Property(a => a.LastActiveAt).HasElementName("lastActiveAt").IsRequired();
		});

		_ = modelBuilder.AddEntityWithName<Profile>("profiles", e =>
		{
			_ = e.Property(a => a.Email).HasElementName("email").IsRequired();
			_ = e.Property(a => a.PhoneNumber).HasElementName("phone_number");
			_ = e.Property(a => a.PhotoUrl).HasElementName("photo_url");
		});
	}
}
