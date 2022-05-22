using System;

namespace Open.Serialization;

/// <summary>
/// Interface for deserializing any <see cref="string"/> to an <see cref="object"/> when given a type.
/// </summary>
public interface IDeserializeObject
{
	/// <summary>
	/// Deserializes a string value to the specified type.
	/// </summary>
	/// <param name="value">The string to deserialize.</param>
	/// <param name="type">The expected type.</param>
	/// <returns>The deserialized result.</returns>
	object? Deserialize(string value, Type type);
}

/// <summary>
/// Interface for deserializing a <see cref="ReadOnlySpan{T}"/> to an <see cref="object"/> when given a type.
/// </summary>
public interface IDeserializeSpanToObject
{
	/// <summary>
	/// Deserializes a span of characters to the specified type.
	/// </summary>
	/// <param name="value">The span to deserialize.</param>
	/// <param name="type">The expected type.</param>
	/// <returns>The deserialized result.</returns>
	object? Deserialize(ReadOnlySpan<char> value, Type type);
}
