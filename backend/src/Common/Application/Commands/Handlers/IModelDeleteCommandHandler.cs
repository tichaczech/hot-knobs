using Mediator;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IModelDeleteCommand"/> command.
/// </summary>
/// <typeparam name="TModelDeleteCommand"></typeparam>
public interface IModelDeleteCommandHandler<in TModelDeleteCommand> : ICommandHandler<TModelDeleteCommand>
	where TModelDeleteCommand : IModelDeleteCommand;
