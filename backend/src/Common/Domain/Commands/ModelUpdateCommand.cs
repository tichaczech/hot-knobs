using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

public abstract record ModelUpdateCommand<TUpdateModel, TModel> : IModelUpdateCommand<TUpdateModel, TModel>
	where TModel : class, IModel
	where TUpdateModel : class
{
	public TUpdateModel UpdateModel { get; set; } = default!;
}
