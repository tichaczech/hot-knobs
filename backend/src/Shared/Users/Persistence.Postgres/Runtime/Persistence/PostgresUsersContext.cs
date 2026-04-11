using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Shared.Users.Model.Entities;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Shared.Users.Runtime.Persistence;

public sealed class PostgresUsersContext : PostgresRepositoryContext
{
	public PostgresUsersContext() : base(new DbContextOptionsBuilder<PostgresRepositoryContext>().UseNpgsql("Host=postgres;Database=shared-users;Username=postgres;Password=postgres").Options) { }

	public PostgresUsersContext(DbContextOptions<PostgresUsersContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntityWithName<Device>("devices", e =>
		{
			_ = e.Property(a => a.APNSToken).HasColumnName("apnsToken");
			_ = e.Property(a => a.FCMToken).HasColumnName("fcmToken").IsRequired();
			_ = e.Property(a => a.Platform).HasColumnName("platform").IsRequired();
			_ = e.Property(a => a.ProfileId).HasColumnName("profileId").IsRequired();
			_ = e.Property(a => a.LastActiveAt).HasColumnName("lastActiveAt").IsRequired();
		});

		_ = modelBuilder.AddEntityWithName<Profile>("profiles", e =>
		{
			_ = e.Property(a => a.Email).HasColumnName("email").IsRequired();
			_ = e.Property(a => a.PhoneNumber).HasColumnName("phone_number");
			_ = e.Property(a => a.PhotoUrl).HasColumnName("photo_url");
		});
	}
}
