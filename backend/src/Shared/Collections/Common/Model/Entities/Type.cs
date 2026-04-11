// using mojeEUC.Model.Entities;

// namespace mojeEUC.Shared.Collections.Model.Entities;

// public class CollectionType : EntityWithLastModified
// {
// 	public string DisplayName { get; set; }

// 	public string Name { get; set; }

// 	public IEnumerable<CollectionProperty>? Properties { get; set; }

// 	public T Clone<T>()
// 		where T : CollectionType, new()
// 	{
// 		return new T()
// 		{
// 			DisplayName = DisplayName,
// 			Name = Name,
// 			Version = Version,
// 			Owner = Owner,
// 			Properties = Properties,
// 		};
// 	}
// }

// public class CollectionProperty
// {
// 	public string DisplayName { get; set; }

// 	public string Name { get; set; }

// 	public CollectionPropertyType Type { get; set; }

// 	public IEnumerable<CollectionProperty>? Properties { get; set; }

// 	public CollectionPropertyType? ItemType { get; set; }
// }

// public enum CollectionPropertyType
// {
// 	@Array,
// 	@Bool,
// 	@Date,
// 	@DateTime,
// 	@Double,
// 	@Int,
// 	@Object,
// 	@String,
// 	@Time,
// }
