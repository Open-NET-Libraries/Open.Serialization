using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public class Serializer<T> : SerializerBase<T>
	{
		private readonly Func<string?, T> _deserializer;
		private readonly Func<T, string?>? _serializer;
		private readonly Func<Stream, CancellationToken, ValueTask<T>> _deserializerAsync;
		private readonly Func<Stream, T, CancellationToken, ValueTask> _serializerAsync;

		public Serializer(
			Func<string?, T> deserializer,
			Func<T, string?>? serializer = null,
			Func<Stream, CancellationToken, ValueTask<T>>? deserializerAsync = null,
			Func<Stream, T, CancellationToken, ValueTask>? serializerAsync = null)
		{
			// It's supported to instantiate a deserializer without a serializer.
			_deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
			_serializer = serializer;
			_deserializerAsync = deserializerAsync ?? base.DeserializeAsync;
			_serializerAsync = serializerAsync ?? base.SerializeAsync;
		}

		public override T Deserialize(string? value)
			=> _deserializer(value);
		public override ValueTask<T> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
			=> _deserializerAsync(stream, cancellationToken);

		public override string? Serialize(T item) => _serializer == null
			? throw new NullReferenceException("No serializer function was supplied.")
			: _serializer(item);
		public override ValueTask SerializeAsync(Stream stream, T item, CancellationToken cancellationToken = default)
			=> _serializerAsync(stream, item, cancellationToken);

	}
}
