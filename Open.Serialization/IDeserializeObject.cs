using System;

namespace Open.Serialization
{
	/// <summary>
	/// Interface for deserializing any string to an object when given a type.
	/// </summary>
	public interface IDeserializeObject : IDeserialize
	{
		/// <summary>
		/// Deserializes a string value to the specified type.
		/// </summary>
		/// <param name="value">The string to deserialize.</param>
		/// <param name="type">The expected type.</param>
		/// <returns>The deserialized result.</returns>
		object? Deserialize(string? value, Type type);

#if NETSTANDARD2_1
		T IDeserialize.Deserialize<T>(string? value)
			=> (T)Deserialize(value, typeof(T))!;
#endif

	}
}
