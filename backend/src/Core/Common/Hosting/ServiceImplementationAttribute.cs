namespace Fand.Runtime.Hosting;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ServiceImplementationAttribute : Attribute
{
	public Type ImplementedService { get; }

	public ServiceImplementationAttribute(Type implementedService)
	{
		ImplementedService = implementedService;
	}
}

