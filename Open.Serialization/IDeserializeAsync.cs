using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization;

/// <summary>
/// Interface for asynchronously deserializing any given generic type.
/// </summary>
public interface IDeserializeAsync
{
	/// <summary>
	/// Deserializes a stream to the specified type.
	/// </summary>
	/// <param name="source">The stream to deserialize.</param>
	/// <param name="cancellationToken">An optional cancellation token.</param>
	/// <returns>The deserialized result.</returns>
	ValueTask<T> DeserializeAsync<T>(Stream source, CancellationToken cancellationToken = default);
}

/// <summary>
/// Interface for asynchronously deserializing a predefined specific generic type.
/// </summary>
public interface IDeserializeAsync<T>
{
	/// <summary>
	/// Deserializes a stream to the specified type.
	/// </summary>
	/// <param name="source">The stream to deserialize.</param>
	/// <param name="cancellationToken">An optional cancellation token.</param>
	/// <returns>The deserialized result.</returns>
	ValueTask<T> DeserializeAsync(Stream source, CancellationToken cancellationToken = default);
}
