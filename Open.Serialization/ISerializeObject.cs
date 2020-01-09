using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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

#if NETSTANDARD2_1
		/// <inheritdoc cref="ISerialize" />
		string? Serialize<T>(T item)
			=> Serialize(item, typeof(T));

		/// <inheritdoc cref="ISerializeObjectAsync.SerializeAsync(Stream, object, Type, CancellationToken)"/>
		ValueTask SerializeAsync(Stream target, object? item, Type type, CancellationToken cancellationToken = default)
			=> DefaultMethods.SerializeAsync(this, target, item, type);

		/// <inheritdoc cref="ISerializeAsync.SerializeAsync{T}(Stream, T, CancellationToken)" />
		virtual ValueTask SerializeAsync<T>(Stream target, T item, CancellationToken cancellationToken)
			=> SerializeAsync(target, item, typeof(T), cancellationToken);
#endif
	}
}
