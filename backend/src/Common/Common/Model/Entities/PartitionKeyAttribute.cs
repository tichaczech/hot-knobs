namespace thc.HotKnobs.Model.Entities;

/// <summary>
/// Attribute to mark property as partition key.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class PartitionKeyAttribute : Attribute { }
