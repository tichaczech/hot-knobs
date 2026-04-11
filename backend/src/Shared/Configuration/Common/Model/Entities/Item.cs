using mojeEUC.Model.Entities;

namespace mojeEUC.Shared.Configuration.Model.Entities;

public class Item : Entity
{
	public IDictionary<string, object>? Data { get; set; }

	public string? DeviceId { get; set; }

	public string Name { get; set; }

	public string? PatientId { get; set; }

	public string Type { get; set; }

	public string UserId { get; set; }
}
