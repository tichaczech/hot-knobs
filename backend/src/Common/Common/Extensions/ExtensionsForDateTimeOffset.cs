namespace thc.HotKnobs.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="DateTimeOffset"/> struct.
/// </summary>
public static class ExtensionsForDateTimeOffset
{
	/// <summary>
	/// Truncates a <see cref="DateTimeOffset"/> value to the nearest second.
	/// </summary>
	/// <param name="dateTimeOffset">The <see cref="DateTimeOffset"/> instance to truncate.</param>
	/// <returns>A new <see cref="DateTimeOffset"/> instance with the milliseconds and sub-millisecond ticks set to zero.</returns>
	public static DateTimeOffset TruncateToSeconds(this DateTimeOffset dateTimeOffset)
	{
		return dateTimeOffset.AddTicks(-(dateTimeOffset.Ticks % TimeSpan.TicksPerSecond));
	}
}

