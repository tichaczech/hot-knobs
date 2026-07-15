using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Create command for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public abstract record ModelCreateCommand<TModel> : IModelCreateCommand<TModel>
	where TModel : class, IModel;

/// <summary>
/// Create command for <see cref="IModel"/> with a specific create model.
/// </summary>
/// <typeparam name="TCreateModel">Type of the create model.</typeparam>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public abstract record ModelCreateCommand<TCreateModel, TModel> : IModelCreateCommand<TCreateModel, TModel>
	where TCreateModel : class
	where TModel : class, IModel
{
	/// <inheritdoc />
	[Required]
	public TCreateModel CreateModel { get; init; } = default!;
}
