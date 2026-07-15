using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Update command for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public abstract record ModelUpdateCommand<TModel> : IModelUpdateCommand<TModel>
	where TModel : class, IModel;

/// <summary>
/// Update command for <see cref="IModel"/> with a specific update model.
/// </summary>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
/// <typeparam name="TUpdateModel">Type of the update model.</typeparam>
public abstract record ModelUpdateCommand<TUpdateModel, TModel> : IModelUpdateCommand<TUpdateModel, TModel>
	where TModel : class, IModel
	where TUpdateModel : class
{
	/// <inheritdoc />
	[Required]
	public TUpdateModel UpdateModel { get; init; } = default!;
}
