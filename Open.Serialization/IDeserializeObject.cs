using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Interface for deserializing any string to an object when given a type.
	/// </summary>
	public interface IDeserializeObject
	{
		/// <summary>
		/// Deserializes a string value to the specified type.
		/// </summary>
		/// <param name="value">The string to deserialize.</param>
		/// <param name="type">The expected type.</param>
		/// <returns>The deserialized result.</returns>
		object? Deserialize(string? value, Type type);

#if NETSTANDARD2_1
		/// <inheritdoc cref="IDeserialize.Deserialize{T}(string)" />
		T Deserialize<T>(string? value)
			=> (T)Deserialize(value, typeof(T))!;

		/// <inheritdoc cref="IDeserializeObjectAsync.DeserializeAsync(Stream, Type, CancellationToken)" />
		ValueTask<object?> DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default)
			=> DefaultMethods.DeserializeAsync(this, source, type);

		/// <inheritdoc cref="IDeserializeAsync.DeserializeAsync{T}(Stream, CancellationToken)" />
		async ValueTask<T> DeserializeAsync<T>(Stream source, CancellationToken cancellationToken)
			=> (T)(await DeserializeAsync(source, typeof(T), cancellationToken))!;
#endif
	}
}
