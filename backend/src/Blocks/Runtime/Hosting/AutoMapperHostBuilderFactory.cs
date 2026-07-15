// using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;
// using System.Reflection;

// using AutoMapper;

// using Fand.Runtime.Hosting;

// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;

// using thc.HotKnobs.Runtime.AutoMapper;

// namespace thc.HotKnobs.Runtime.Hosting;

// /// <summary>
// /// <see cref="IHostApplicationBuilder"/> implementation for AutoMapper.
// /// </summary>
// public class AutoMapperHostBuilderFactory : IHostApplicationBuilderFactory
// {
// 	/// <inheritdoc />
// 	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

// 	/// <inheritdoc />
// 	public string Name => "default";

// 	/// <inheritdoc />
// 	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
// 	{
// 		ArgumentNullException.ThrowIfNull(builder);
// 		ArgumentNullException.ThrowIfNull(options);

// 		options.Logger?.LogInformation("Configuring AutoMapper...");

// 		// Scan assemblies for metadata classes at startup
// 		var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToArray();
// 		foreach (var assembly in assemblies)
// 		{
// 			AddMetadataValidations(assembly);
// 		}

// 		_ = builder.Services.AddSingleton<IMapper>(options =>
// 		{
// 			var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToArray(); ;
// 			var configuration = new MapperConfiguration(cfg =>
// 			{
// 				cfg.AddMaps(assemblies);
// 			}, options.GetRequiredService<ILoggerFactory>());

// 			var mapper = configuration.CreateMapper();
// 			var logger = options.GetRequiredService<ILogger<ValidatingMapper>>();

// 			return new ValidatingMapper(mapper, logger);
// 		});

// 		return builder;
// 	}

// 	// need to add metadata validations manually
// 	private static void AddMetadataValidations(params Assembly[] assembliesToScan)
// 	{
// 		foreach (var assembly in assembliesToScan)
// 		{
// 			var typesWithMetadata = assembly.GetTypes()
// 				.Where(type => type.IsClass)
// 				.SelectMany(type => type.GetCustomAttributes<MetadataTypeAttribute>(false)
// 				.Select(metaAttr => new { Type = type, MetaAttr = metaAttr }));

// 			foreach (var item in typesWithMetadata)
// 				RegisterMetadataClass(item.Type, item.MetaAttr);
// 		}
// 	}

// 	private static void RegisterMetadataClass(Type currentClass, MetadataTypeAttribute metaAttribute)
// 	{
// 		TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(currentClass, metaAttribute.MetadataClassType), currentClass);
// 	}
// }

