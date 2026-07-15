using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Models;
using thc.HotKnobs.Runtime;
using thc.HotKnobs.Runtime.Persistence;
using thc.HotKnobs.Runtime.Security;

namespace thc.HotKnobs.Model.Repository;

public class PostgresRepository<TEntity, TContext> : Repository<TEntity, TContext>
	where TEntity : Entity
	where TContext : PostgresRepositoryContext
{
	public PostgresRepository(TContext context, IConcurrencyTokenContext concurrencyTokenContext, IUserProvider userProvider) : base(context, concurrencyTokenContext, userProvider)
	{
	}
}
