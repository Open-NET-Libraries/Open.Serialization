using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public abstract class SerializerBase : ISerializer, IAsyncSerializer
	{
		public abstract T Deserialize<T>(string? value);

		public virtual async ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			string text;
			using (var reader = new StreamReader(stream))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return Deserialize<T>(text);
		}

		public abstract string? Serialize<T>(T item);

		public virtual async ValueTask SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken = default)
		{
			var text = Serialize(item);
			using var writer = new StreamWriter(stream);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}

		public ISerializer<T> Cast<T>()
			=> new Serializer<T>(Deserialize<T>, Serialize, DeserializeAsync<T>, SerializeAsync);
	}

	public abstract class SerializerBase<T> : ISerializer<T>, IAsyncSerializer<T>
	{
		public abstract T Deserialize(string? value);

		public virtual async ValueTask<T> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
		{
			string text;
			using (var reader = new StreamReader(stream))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return Deserialize(text);
		}

		public abstract string? Serialize(T item);

		public virtual async ValueTask SerializeAsync(Stream stream, T item, CancellationToken cancellationToken = default)
		{
			var text = Serialize(item);
			using var writer = new StreamWriter(stream);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}
	}
}
