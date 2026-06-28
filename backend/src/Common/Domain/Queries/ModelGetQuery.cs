using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public abstract record ModelGetQuery<TModel> : IModelGetQuery<TModel>
	where TModel : class, IModel;
