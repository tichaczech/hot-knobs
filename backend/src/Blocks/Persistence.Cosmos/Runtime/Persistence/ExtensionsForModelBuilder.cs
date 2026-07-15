using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using thc.HotKnobs.Models;

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
			_ = e.ToContainer(containerName);
			_ = e.HasKey(e => e.Id);

			_ = e.Property(e => e.Id).ToJsonProperty("id").ValueGeneratedOnAdd().HasValueGenerator<StringValueGenerator>();
			_ = e.Property(e => e.CreatedAt).ToJsonProperty("createdAt"); //.ValueGeneratedOnAdd();
			_ = e.Property(e => e.CreatedBy).ToJsonProperty("createdBy");
			_ = e.Property(e => e.ETag).ToJsonProperty("_etag").IsConcurrencyToken();
			_ = e.Property(a => a.IsActive).ToJsonProperty("isActive");
			_ = e.Property(a => a.UpdatedAt).ToJsonProperty("updatedAt"); //.ValueGeneratedOnAddOrUpdate();
			_ = e.Property(a => a.UpdatedBy).ToJsonProperty("updatedBy");

			var partitionKeyPropertyInfo = typeof(TEntity).GetProperties()
				.FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(PartitionKeyAttribute)));

			if (partitionKeyPropertyInfo == null)
				return;

			// TODO: Explain this!
			// e.HasPartitionKey(partitionKeyPropertyInfo.Name); // property name of .net object is not the same as the json property name
			var parameter = Expression.Parameter(typeof(TEntity), "e");
			var property = Expression.Property(parameter, partitionKeyPropertyInfo);
			var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);
			_ = e.HasPartitionKey(lambda);
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
			_ = e.Property(a => a.Description).ToJsonProperty("description");
			_ = e.Property(a => a.Name).ToJsonProperty("name");
		});

		if (buildAction != null)
			_ = modelBuilder.Entity(buildAction);

		return modelBuilder;
	}
}
