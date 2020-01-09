using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Base class for implementing a serializer/deserializer.
	/// </summary>
	public abstract class SerializerBase : ISerializer, IAsyncSerializer
	{
		/// <inheritdoc />
		public abstract T Deserialize<T>(string? value);

		/// <inheritdoc />
		public virtual async ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			string text;
			using (var reader = new StreamReader(stream))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return Deserialize<T>(text);
		}

		/// <inheritdoc />
		public abstract string? Serialize<T>(T item);

		/// <inheritdoc />
		public virtual async ValueTask SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken = default)
		{
			var text = Serialize(item);
			using var writer = new StreamWriter(stream);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}

		/// <summary>
		/// Creates a type specific serializer using this as the underlying serializer.
		/// </summary>
		/// <returns>A type specific serializer.</returns>
		public ISerializer<T> Cast<T>()
			=> new Serializer<T>(Deserialize<T>, Serialize, DeserializeAsync<T>, SerializeAsync);
	}

	/// <summary>
	/// Base calss for implementing a spedific generic type serializer/deserializer.
	/// </summary>
	public abstract class SerializerBase<T> : ISerializer<T>, IAsyncSerializer<T>
	{
		/// <inheritdoc />
		public abstract T Deserialize(string? value);

		/// <inheritdoc />
		public virtual async ValueTask<T> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
		{
			string text;
			using (var reader = new StreamReader(stream))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return Deserialize(text);
		}

		/// <inheritdoc />
		public abstract string? Serialize(T item);

		/// <inheritdoc />
		public virtual async ValueTask SerializeAsync(Stream stream, T item, CancellationToken cancellationToken = default)
		{
			var text = Serialize(item);
			using var writer = new StreamWriter(stream);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}
	}
}
