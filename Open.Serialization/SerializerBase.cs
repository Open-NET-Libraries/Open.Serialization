using System.Threading.Tasks;

namespace Open.Serialization
{
	public abstract class SerializerBase : ISerializer, IAsyncSerializer
	{
		public abstract T Deserialize<T>(string value);

		public virtual ValueTask<T> DeserializeAsync<T>(string value)
			=> new ValueTask<T>(Deserialize<T>(value));

		public abstract string Serialize<T>(T item);

		public virtual ValueTask<string> SerializeAsync<T>(T item)
			=> new ValueTask<string>(Serialize(item));
	}

	public abstract class SerializerBase<T> : ISerializer<T>, IAsyncSerializer<T>
	{
		public abstract T Deserialize(string value);

		public virtual ValueTask<T> DeserializeAsync(string value)
			=> new ValueTask<T>(Deserialize(value));

		public abstract string Serialize(T item);

		public virtual ValueTask<string> SerializeAsync(T item)
			=> new ValueTask<string>(Serialize(item));
	}
}
