using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization;

/// <summary>
/// Interface for asynchronously serializing an object to a string when given a type.
/// </summary>
public interface ISerializeObjectAsync
{
	/// <summary>
	/// Serializes the provided item to a stream.
	/// </summary>
	/// <param name="target">The destination stream.</param>
	/// <param name="item">The item to serialize.</param>
	/// <param name="type">The expected type.</param>
	/// <param name="cancellationToken">An optional cancellation token.</param>
	ValueTask SerializeAsync(Stream target, object? item, Type type, CancellationToken cancellationToken = default);
}
