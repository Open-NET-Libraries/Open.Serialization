using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;

namespace Open.Serialization.Json.Utf8Json;

internal class JsonSerializerInternal : JsonObjectSerializerBase, IJsonSerializer
{
	readonly IJsonFormatterResolver _resolver;
	readonly bool _indent;
	internal JsonSerializerInternal(IJsonFormatterResolver resolver, bool indent)
	{
		_resolver = resolver;
		_indent = indent;
	}

	public override T Deserialize<T>(string? value)
		=> JsonSerializer.Deserialize<T>(value, _resolver);

	public new Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();
		return JsonSerializer.DeserializeAsync<T>(stream, _resolver);
	}

	ValueTask<T> IDeserializeAsync.DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken)
		=> new(DeserializeAsync<T>(stream, cancellationToken));

	public override string Serialize<T>(T item)
	{
		var json = JsonSerializer.ToJsonString(item, _resolver);
		return _indent ? JsonSerializer.PrettyPrint(json) : json;
	}

	public new Task SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();
		if (!_indent) return JsonSerializer.SerializeAsync(stream, item);

		var result = JsonSerializer.PrettyPrintByteArray(JsonSerializer.Serialize(item));
		return stream.WriteAsync(result, 0, result.Length, cancellationToken);
	}

	ValueTask ISerializeAsync.SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken)
		=> new(SerializeAsync(stream, item, cancellationToken));

	public override object? Deserialize(string? value, Type type)
		=> JsonSerializer.NonGeneric.Deserialize(type, value, _resolver);

	public override string? Serialize(object? item, Type type)
	{
		var json = JsonSerializer.NonGeneric.ToJsonString(type, item, _resolver);
		return _indent ? JsonSerializer.PrettyPrint(json) : json;
	}

	public override ValueTask<object?> DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default)
		=> new(JsonSerializer.NonGeneric.DeserializeAsync(type, source, _resolver));

	public override ValueTask SerializeAsync(Stream target, object? item, Type type, CancellationToken cancellationToken = default)
		=> _indent
			? base.SerializeAsync(target, item, type, cancellationToken)
			: new ValueTask(JsonSerializer.NonGeneric.SerializeAsync(type, target, item, _resolver));

	public new JsonSerializer<T> Cast<T>()
		=> new(Deserialize<T>, Serialize, ((IDeserializeAsync)this).DeserializeAsync<T>, ((ISerializeAsync)this).SerializeAsync);
}
