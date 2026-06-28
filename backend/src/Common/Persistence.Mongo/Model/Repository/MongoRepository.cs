using MongoDB.Driver;

using thc.HotKnobs.Models;
using thc.HotKnobs.Runtime;
using thc.HotKnobs.Runtime.Persistence;
using thc.HotKnobs.Runtime.Security;

namespace thc.HotKnobs.Model.Repository;

public class MongoRepository<TEntity, TContext>(TContext context, IConcurrencyTokenContext concurrencyTokenContext, IUserProvider userProvider) : Repository<TEntity, TContext>(context, concurrencyTokenContext, userProvider)
	where TEntity : Entity
	where TContext : MongoRepositoryContext
{
	// public override Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
	// {
	// 	using var client = new MongoClient("mongodb://localhost:27017");
	// 	var db = client.GetDatabase("test");
	// 	var col = db.GetCollection<TEntity>("test");

	// 	var filter = Builders<TEntity>.Filter.Where(e => e.Id == entity.Id);
	// 	var items = col.Find(filter).FirstOrDefaultAsync(cancellationToken);

	// 	// entity.ConcurrencyToken = _ctContext.GetNextConcurrencyToken();
	// 	// entity.CreatedBy = UserProvider.GetCurrentUserId();
	// 	// entity.CreatedAt = DateTime.UtcNow;

	// 	return base.CreateAsync(entity, cancellationToken);
	// }
}
