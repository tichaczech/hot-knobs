using Microsoft.Azure.Cosmos;

using thc.HotKnobs.Models;
using thc.HotKnobs.Runtime;
using thc.HotKnobs.Runtime.Persistence;
using thc.HotKnobs.Runtime.Security;

namespace thc.HotKnobs.Model.Repository;

public class CosmosRepository<TEntity, TContext>(TContext context, IConcurrencyTokenContext concurrencyTokenContext, IUserProvider userProvider) : Repository<TEntity, TContext>(context, concurrencyTokenContext, userProvider)
	where TEntity : Entity
	where TContext : CosmosRepositoryContext
{
	// override Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
	// {
	// 	using var client = new CosmosClient("context.ConnectionString");
	// 	var db = client.GetDatabase("test");
	// 	var col = db.GetContainer("test");

	// 	var items = col.GetItemLinqQueryable<TEntity>(true);

	// 	// entity.ConcurrencyToken = _ctContext.GetNextConcurrencyToken();
	// 	// entity.CreatedBy = UserProvider.GetCurrentUserId();
	// 	// entity.CreatedAt = DateTime.UtcNow;

	// 	return base.CreateAsync(entity, cancellationToken);
	// }
}
