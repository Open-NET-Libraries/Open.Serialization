using System;
using System.IO;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public class Serializer<T> : SerializerBase<T>
	{
		private readonly Func<string, T> _deserializer;
		private readonly Func<T, string> _serializer;
		private readonly Func<Stream, ValueTask<T>> _deserializerAsync;
		private readonly Func<Stream, T, ValueTask> _serializerAsync;

		public Serializer(
			Func<string, T> deserializer,
			Func<T, string> serializer = null,
			Func<Stream, ValueTask<T>> deserializerAsync = null,
			Func<Stream, T, ValueTask> serializerAsync = null)
		{
			// It's supported to instantiate a deserializer without a serializer.
			_deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
			_serializer = serializer;
			_deserializerAsync = deserializerAsync ?? base.DeserializeAsync;
			_serializerAsync = serializerAsync ?? base.SerializeAsync;
		}

		public override T Deserialize(string value) => _deserializer(value);
		public override ValueTask<T> DeserializeAsync(Stream stream) => _deserializerAsync(stream);

		public override string Serialize(T item) => _serializer(item);
		public override ValueTask SerializeAsync(Stream stream, T item) => _serializerAsync(stream, item);

	}
}
