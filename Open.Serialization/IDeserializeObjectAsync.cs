using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Interface for asynchronously deserializing any string to an object when given a type.
	/// </summary>
	public interface IDeserializeObjectAsync : IDeserializeAsync
	{
		/// <summary>
		/// Deserializes a stream to the specified type.
		/// </summary>
		/// <param name="source">The stream to deserialize.</param>
		/// <param name="type">The expected type.</param>
		/// <param name="cancellationToken">An optional cancellation token.</param>
		/// <returns>The deserialized result.</returns>
		ValueTask<object?> DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default);

#if NETSTANDARD2_1
		async ValueTask<T> IDeserializeAsync.DeserializeAsync<T>(Stream source, CancellationToken cancellationToken)
			=> (T)(await DeserializeAsync(source, typeof(T), cancellationToken))!;
#endif
	}
}
