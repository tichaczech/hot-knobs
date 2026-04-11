using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Runtime.Persistence;

public static class ExtensionsForModelBuilder
{
	public static ModelBuilder AddEntity<TEntity>(this ModelBuilder modelBuilder, string containerName, Action<EntityTypeBuilder<TEntity>> buildAction)
		where TEntity : Entity
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);
		ArgumentNullException.ThrowIfNull(containerName);

		_ = modelBuilder.Entity<TEntity>(e =>
		{
			_ = e.HasNoDiscriminator();
			_ = e.ToTable(containerName);
			_ = e.HasKey(e => e.Id);

			_ = e.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd().HasValueGenerator<StringValueGenerator>();
			_ = e.Property(e => e.CreatedAt).HasColumnName("createdAt"); // ValueGeneratedOnAdd is not working in PostgreSQL, so we set it manually in IRepository.SaveChangesAsync
			_ = e.Property(e => e.CreatedBy).HasColumnName("createdBy");
			_ = e.Property(e => e.ETag).HasColumnName("etag").IsConcurrencyToken(); // ValueGeneratedOnAddOrUpdate is not working in PostgreSQL, so we set it manually in IRepository.SaveChangesAsync
			_ = e.Property(a => a.IsActive).HasColumnName("isActive");
			_ = e.Property(a => a.UpdatedAt).HasColumnName("updatedAt"); // ValueGeneratedOnAddOrUpdate is not working in PostgreSQL, so we set it manually in IRepository.SaveChangesAsync
			_ = e.Property(a => a.UpdatedBy).HasColumnName("updatedBy");
		});

		if (buildAction != null)
			_ = modelBuilder.Entity(buildAction);

		return modelBuilder;
	}

	public static ModelBuilder AddEntityWithName<TEntity>(this ModelBuilder modelBuilder, string containerName, Action<EntityTypeBuilder<TEntity>> buildAction)
		where TEntity : EntityWithName
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);
		ArgumentException.ThrowIfNullOrEmpty(containerName);
		ArgumentNullException.ThrowIfNull(buildAction);

		_ = modelBuilder.AddEntity<TEntity>(containerName, e =>
		{
			_ = e.Property(a => a.Description).HasColumnName("description");
			_ = e.Property(a => a.Name).HasColumnName("name");
		});

		if (buildAction != null)
			_ = modelBuilder.Entity(buildAction);

		return modelBuilder;
	}
}
