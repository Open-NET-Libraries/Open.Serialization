using System;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public class Serializer<T> : SerializerBase<T>
	{
		private readonly Func<T, string> _serializer;
		private readonly Func<string, T> _deserializer;
		private readonly Func<T, ValueTask<string>> _serializerAsync;
		private readonly Func<string, ValueTask<T>> _deserializerAsync;

		public Serializer(
			Func<string, T> deserializer,
			Func<T, string> serializer = null,
			Func<string, ValueTask<T>> deserializerAsync = null,
			Func<T, ValueTask<string>> serializerAsync = null)
		{
			// It's supported to instantiate a deserializer without a serializer.
			_deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
			_serializer = serializer;
			_deserializerAsync = deserializerAsync ?? base.DeserializeAsync;
			_serializerAsync = serializerAsync ?? base.SerializeAsync;
		}

		public override T Deserialize(string value) => _deserializer(value);
		public override ValueTask<T> DeserializeAsync(string value) => _deserializerAsync(value);

		public override string Serialize(T item) => _serializer(item);
		public override ValueTask<string> SerializeAsync(T item) => _serializerAsync(item);

	}
}
