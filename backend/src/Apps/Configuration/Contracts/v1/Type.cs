// using mojeEUC.Contracts;

// namespace mojeEUC.Shared.Configuration.Contracts.v1;

// public abstract class ConfigurationTypeContract
// {
// 	public string DisplayName { get; set; }

// 	public string Name { get; set; }
// }

// public class ConfigurationTypeCreateRequest : ConfigurationItemContract, ICreateRequest { }

// public abstract class ConfigurationTypeUpdateRequest : ConfigurationItemContract, IUpdateRequest { }

// public class ConfigurationTypeResponse : ConfigurationItemContract, IResponse
// {
// 	public string Id { get; set; }

// 	public bool IsActive { get; set; }
// }

// public interface IConfigurationTypeService : IService<ConfigurationTypeCreateRequest, ConfigurationTypeUpdateRequest, ConfigurationTypeResponse>
// {
//     Task<ConfigurationItemResponse> GetByNameAsync(string name, CancellationToken cancellationToken = default);
// }
