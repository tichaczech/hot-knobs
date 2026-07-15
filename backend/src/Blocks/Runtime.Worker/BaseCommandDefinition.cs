using System.CommandLine;
using System.CommandLine.Binding;

namespace thc.HotKnobs.Runtime.Worker;

public static class BaseVerbOptions
{
#pragma warning disable CA2211
	public static Option<bool> DryRunOption = new(["-d", "--dryRun"], "Dry run") { IsRequired = false };
	public static Option<bool> VerboseOption = new(["-v", "--verbose"], "Prints all messages to standard output") { IsRequired = false };
#pragma warning restore CA2211
}

public class BaseVerbBinder<TVerb> : BinderBase<TVerb> where TVerb : BaseVerb, new()
{
	private readonly Option<bool> _dryRunOption;
	private readonly Option<bool> _verboseOption;

	public BaseVerbBinder(Option<bool> dryRunOption, Option<bool> verboseOption)
	{
		_dryRunOption = dryRunOption;
		_verboseOption = verboseOption;
	}

	protected override TVerb GetBoundValue(BindingContext bindingContext)
	{
		ArgumentNullException.ThrowIfNull(bindingContext);

		return new TVerb { DryRun = bindingContext.ParseResult.GetValueForOption(_dryRunOption), Verbose = bindingContext.ParseResult.GetValueForOption(_verboseOption) };
	}
}

public class BaseVerb
{
	public bool DryRun { get; set; }
	public bool Verbose { get; set; }
}
