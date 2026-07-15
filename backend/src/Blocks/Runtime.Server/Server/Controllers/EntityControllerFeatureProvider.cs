using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace thc.HotKnobs.Runtime.Server.Controllers;

public class EntityControllerFeatureProvider : ControllerFeatureProvider
{
	protected override bool IsController(TypeInfo typeInfo)
	{
		ArgumentNullException.ThrowIfNull(typeInfo);

		var isControllerBase = typeInfo.IsAssignableTo(typeof(ControllerBase));
		var isSubClass = typeInfo.IsSubclassOfGeneric(typeof(EntityController<,,,,>));

		var isController = isControllerBase && !typeInfo.IsAbstract && !typeInfo.IsGenericTypeDefinition;

		var hasApiAttribute = typeInfo.IsDefined(typeof(ApiControllerAttribute));

		var isEntityController = !typeInfo.IsAbstract && isControllerBase && isSubClass;

		return isController || hasApiAttribute || isEntityController || base.IsController(typeInfo);
	}
}

public static class ExtensionForTypeInfo
{
	public static bool IsSubclassOfGeneric(this Type type, Type baseType)
	{
		ArgumentNullException.ThrowIfNull(type);
		ArgumentNullException.ThrowIfNull(baseType);

		if (type == typeof(object) || type.BaseType == null)
			return false;

		if (!baseType.IsGenericType)
			return false;

		if (type.IsGenericType && type.GetGenericTypeDefinition() == baseType)
			return true;

		return type.BaseType.IsSubclassOfGeneric(baseType) || type.GetInterfaces().Any(t => t.IsSubclassOfGeneric(baseType));
	}
}
