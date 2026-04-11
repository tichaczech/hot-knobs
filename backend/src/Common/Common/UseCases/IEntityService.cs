using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.UseCases;

/// <summary>
/// Entity create model interface.
/// </summary>
public interface IEntityCreateModel : ICreateModel { }

/// <summary>
/// Entity update model interface.
/// </summary>
public interface IEntityUpdateModel : IUpdateModel { }

/// <summary>
/// Base entity use cases.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TCreateModel"></typeparam>
/// <typeparam name="TUpdateModel"></typeparam>
public interface IEntityUseCases<TEntity, TCreateModel, TUpdateModel> : IModelUseCases<TEntity, TCreateModel, TUpdateModel>
	where TEntity : Entity
	where TCreateModel : class, IEntityCreateModel
	where TUpdateModel : class, IEntityUpdateModel
{
}
