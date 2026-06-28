namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IEntityDeleteCommand"/> command.
/// </summary>
/// <typeparam name="TEntityDeleteCommand"></typeparam>
public interface IEntityDeleteCommandHandler<in TEntityDeleteCommand> : IModelDeleteCommandHandler<TEntityDeleteCommand>
	where TEntityDeleteCommand : IEntityDeleteCommand;
