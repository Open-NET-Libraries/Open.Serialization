using System.IO;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public abstract class SerializerBase : ISerializer, IAsyncSerializer
	{
		public abstract T Deserialize<T>(string value);

		public virtual async ValueTask<T> DeserializeAsync<T>(Stream stream)
		{
			string text;
			using(var reader = new StreamReader(stream))
				text = await reader.ReadToEndAsync();
			return Deserialize<T>(text);
		}

		public abstract string Serialize<T>(T item);

		public virtual async ValueTask SerializeAsync<T>(Stream stream, T item)
		{
			var text = Serialize(item);
			using var writer = new StreamWriter(stream);
			await writer.WriteAsync(text);
		}

		public Serializer<T> Cast<T>()
			=> new Serializer<T>(Deserialize<T>, Serialize, DeserializeAsync<T>, SerializeAsync);

		ISerializer<T> ISerializer.Cast<T>() => Cast<T>();
		IAsyncSerializer<T> IAsyncSerializer.Cast<T>() => Cast<T>();
	}

	public abstract class SerializerBase<T> : ISerializer<T>, IAsyncSerializer<T>
	{
		public abstract T Deserialize(string value);

		public virtual async ValueTask<T> DeserializeAsync(Stream stream)
		{
			string text;
			using (var reader = new StreamReader(stream))
				text = await reader.ReadToEndAsync();
			return Deserialize(text);
		}

		public abstract string Serialize(T item);

		public virtual async ValueTask SerializeAsync(Stream stream, T item)
		{
			var text = Serialize(item);
			using var writer = new StreamWriter(stream);
			await writer.WriteAsync(text);
		}
	}
}
