using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using thc.HotKnobs.Model.Entities;

using MongoDB.EntityFrameworkCore.Extensions;

namespace thc.HotKnobs.Runtime.Persistence;

public static class ExtensionsForModelBuilder
{
	public static ModelBuilder AddEntity<TEntity>(this ModelBuilder modelBuilder, string containerName, Action<EntityTypeBuilder<TEntity>> buildAction)
		where TEntity : Entity
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);
		ArgumentException.ThrowIfNullOrEmpty(containerName);
		ArgumentNullException.ThrowIfNull(buildAction);

		_ = modelBuilder.Entity<TEntity>(e =>
		{
			_ = e.HasNoDiscriminator();
			_ = e.ToCollection(containerName);
			_ = e.HasKey(e => e.Id);

			_ = e.Property(e => e.Id).HasElementName("_id").HasBsonRepresentation(MongoDB.Bson.BsonType.String).ValueGeneratedOnAdd();
			_ = e.Property(e => e.CreatedAt).HasElementName("createdAt"); //.ValueGeneratedOnAdd();
			_ = e.Property(e => e.CreatedBy).HasElementName("createdBy");
			_ = e.Property(e => e.ETag).HasElementName("etag"); //.HasBsonRepresentation(MongoDB.Bson.BsonType.String).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
			_ = e.Property(a => a.IsActive).HasElementName("isActive");
			_ = e.Property(a => a.UpdatedAt).HasElementName("updatedAt"); //.ValueGeneratedOnAddOrUpdate();
			_ = e.Property(a => a.UpdatedBy).HasElementName("updatedBy");
		});

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
			_ = e.Property(a => a.Description).HasElementName("description");
			_ = e.Property(a => a.Name).HasElementName("name");
		});

		if (buildAction != null)
			_ = modelBuilder.Entity(buildAction);

		return modelBuilder;
	}
}
