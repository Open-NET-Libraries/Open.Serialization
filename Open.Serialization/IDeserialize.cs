namespace Open.Serialization;

/// <summary>
/// Interface for deserializing any given generic type.
/// </summary>
public interface IDeserialize
{
	/// <summary>
	/// Deserializes a string value to the specified type.
	/// </summary>
	/// <param name="value">The string to deserialize.</param>
	/// <returns>The deserialized result.</returns>
	T Deserialize<T>(string? value);
}

/// <summary>
/// Interface for deserializing a predefined specific generic type.
/// </summary>
public interface IDeserialize<out T>
{
	/// <summary>
	/// Deserializes a string value to the specified type.
	/// </summary>
	/// <param name="value">The string to deserialize.</param>
	/// <returns>The deserialized result.</returns>
	T Deserialize(string? value);
}
