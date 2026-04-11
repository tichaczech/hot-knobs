using thc.HotKnobs.Model.Entities;
using thc.HotKnobs.Runtime;
using thc.HotKnobs.Runtime.Persistence;
using thc.HotKnobs.Runtime.Security;

namespace thc.HotKnobs.Model.Repository;

public class CosmosRepository<TEntity, TContext>(TContext context, IConcurrencyTokenContext concurrencyTokenContext, IUserProvider userProvider) : Repository<TEntity, TContext>(context, concurrencyTokenContext, userProvider)
	where TEntity : Entity
	where TContext : CosmosRepositoryContext
{
}
