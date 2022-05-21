namespace Open.Serialization;

/// <summary>
/// Interface for serializing a given generic type.
/// </summary>
public interface ISerialize
{
	/// <summary>
	/// Serializes the provided item to a string.
	/// </summary>
	/// <param name="item">The item to deserialze.</param>
	/// <returns>The serialized string.</returns>
	string? Serialize<T>(T item);
}

/// <summary>
/// Interface for serializing a predefined specific generic type.
/// </summary>
public interface ISerialize<in T>
{
	/// <summary>
	/// Serializes the provided item to a string.
	/// </summary>
	/// <param name="item">The item to deserialze.</param>
	/// <returns>The serialized string.</returns>
	string? Serialize(T item);
}
