using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace thc.HotKnobs.Runtime;

/// <summary>
/// Attribute to specify the HTTP methods allowed for a controller.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class HttpMethodFilterAttribute : Attribute
{
	/// <summary>
	/// Gets the HTTP methods allowed for a controller.
	/// </summary>
	public string[] Methods { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="HttpMethodFilterAttribute"/> class.
	/// </summary>
	/// <param name="methods"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public HttpMethodFilterAttribute(params string[] methods) => Methods = methods ?? throw new ArgumentNullException(nameof(methods));
}

/// <summary>
/// Convention to apply filters to the application's controllers and actions.
/// </summary>
/// <param name="filterTypes"></param>
public class FilterConvention(IEnumerable<Type> filterTypes) : IApplicationModelConvention
{
	/// <summary>
	/// Initializes a new instance of the <see cref="FilterConvention"/> class.
	/// </summary>
	/// <param name="application"></param>
	public void Apply(ApplicationModel application)
	{
		ArgumentNullException.ThrowIfNull(application);

		foreach (var action in application.Controllers.SelectMany(controller => controller.Actions))
		{
			foreach (var filterType in filterTypes)
			{
				if (ShouldApplyFilter(action, filterType))
					action.Filters.Add(new ServiceFilterAttribute(filterType));
			}
		}
	}

	private static bool ShouldApplyFilter(ActionModel action, MemberInfo filterType)
	{
		ArgumentNullException.ThrowIfNull(action);

		var allowedMethodsAttribute = filterType.GetCustomAttribute<HttpMethodFilterAttribute>();
		if (allowedMethodsAttribute == null)
		{
			return false;
		}

		var actionHttpMethods = action.Attributes.OfType<IActionHttpMethodProvider>().SelectMany(a => a.HttpMethods);

		return actionHttpMethods.Any(method => allowedMethodsAttribute.Methods.Contains(method));
	}
}
