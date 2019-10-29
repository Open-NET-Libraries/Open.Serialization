using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;

namespace Open.Serialization.Json.Utf8Json
{
	internal class JsonSerializerInternal : JsonSerializerBase, IJsonSerializer
	{
		readonly IJsonFormatterResolver _resolver;
		readonly bool _indent;
		internal JsonSerializerInternal(IJsonFormatterResolver resolver, bool indent)
		{
			_resolver = resolver;
			_indent = indent;
		}

		public override T Deserialize<T>(string value)
			=> JsonSerializer.Deserialize<T>(value, _resolver);

		public new Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
			=> JsonSerializer.DeserializeAsync<T>(stream, _resolver);

		ValueTask<T> IDeserializeAsync.DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken)
			=> new ValueTask<T>(DeserializeAsync<T>(stream, cancellationToken));

		public override string Serialize<T>(T item)
		{
			var json = JsonSerializer.ToJsonString(item, _resolver);
			return _indent ? JsonSerializer.PrettyPrint(json) : json;
		}

		public new Task SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken = default)
		{
			if (!_indent) return JsonSerializer.SerializeAsync(stream, item);

			var result = JsonSerializer.PrettyPrintByteArray(JsonSerializer.Serialize(item));
			return stream.WriteAsync(result, 0, result.Length, cancellationToken);
		}

		ValueTask ISerializeAsync.SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken)
			=> new ValueTask(SerializeAsync(stream, item, cancellationToken));

	}

	internal class JsonSerializerInternal<T> : JsonSerializerBase<T>, IJsonSerializer<T>
	{
		readonly IJsonFormatterResolver _resolver;
		readonly bool _indent;
		internal JsonSerializerInternal(IJsonFormatterResolver resolver, bool indent)
		{
			_resolver = resolver;
			_indent = indent;
		}

		public override T Deserialize(string value)
			=> JsonSerializer.Deserialize<T>(value, _resolver);

		public new Task SerializeAsync(Stream stream, T item, CancellationToken cancellationToken = default)
		{
			if (!_indent) return JsonSerializer.SerializeAsync(stream, item);

			var result = JsonSerializer.PrettyPrintByteArray(JsonSerializer.Serialize(item));
			return stream.WriteAsync(result, 0, result.Length, cancellationToken);
		}

		ValueTask ISerializeAsync<T>.SerializeAsync(Stream stream, T item, CancellationToken cancellationToken)
			=> new ValueTask(SerializeAsync(stream, item, cancellationToken));

		public override string Serialize(T item)
			=> JsonSerializer.ToJsonString(item, _resolver);

		public new Task<T> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
			=> JsonSerializer.DeserializeAsync<T>(stream, _resolver);

		ValueTask<T> IDeserializeAsync<T>.DeserializeAsync(Stream stream, CancellationToken cancellationToken)
			=> new ValueTask<T>(DeserializeAsync(stream, cancellationToken));
	}
}
