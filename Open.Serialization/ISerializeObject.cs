using System;

namespace Open.Serialization
{
	/// <summary>
	/// Interface for serializing an object to a string when given a type.
	/// </summary>
	public interface ISerializeObject
	{
		/// <summary>
		/// Serializes the provided item to a string.
		/// </summary>
		/// <param name="item">The item to deserialze.</param>
		/// <param name="type">The expected type.</param>
		/// <returns>The serialized string.</returns>
		string? Serialize(object? item, Type type);
	}
}
