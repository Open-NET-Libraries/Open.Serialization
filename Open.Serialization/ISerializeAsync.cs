using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Interface for asynchronously serializing a given generic type.
	/// </summary>
	public interface ISerializeAsync
	{
		/// <summary>
		/// Serializes the provided item to a stream.
		/// </summary>
		/// <param name="target">The destination stream.</param>
		/// <param name="item">The item to serialize.</param>
		/// <param name="cancellationToken">An optional cancellation token.</param>
		ValueTask SerializeAsync<T>(Stream target, T item, CancellationToken cancellationToken = default);
	}

	/// <summary>
	/// Interface for asynchronously serializing a predefined specific generic type.
	/// </summary>
	public interface ISerializeAsync<in T>
	{
		/// <summary>
		/// Serializes the provided item to a stream.
		/// </summary>
		/// <param name="target">The destination stream.</param>
		/// <param name="item">The item to serialize.</param>
		/// <param name="cancellationToken">An optional cancellation token.</param>
		ValueTask SerializeAsync(Stream target, T item, CancellationToken cancellationToken = default);
	}
}
