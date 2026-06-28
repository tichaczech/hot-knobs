using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

public abstract record ModelCreateCommand<TCreateModel, TModel> : IModelCreateCommand<TCreateModel, TModel>
	where TCreateModel : class
	where TModel : class, IModel
{
	[Required]
	public required TCreateModel CreateModel { get; set; }
}
