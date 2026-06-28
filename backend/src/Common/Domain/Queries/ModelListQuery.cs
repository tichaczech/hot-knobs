using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public abstract record ModelListQuery<TRepresentation> : IModelListQuery<TRepresentation>
	where TRepresentation : class, IModel;
