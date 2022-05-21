using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization;

/// <summary>
/// A class for serializing and deserializing a predefined generic type.
/// </summary>
public class Serializer<T> : SerializerBase<T>
{
	private readonly Func<string?, T>? _deserializer;
	private readonly Func<T, string?>? _serializer;
	private readonly Func<Stream, CancellationToken, ValueTask<T>> _deserializerAsync;
	private readonly Func<Stream, T, CancellationToken, ValueTask> _serializerAsync;

	/// <summary>
	/// Constructs a serializer/deserializer using the provided serialization functions.
	/// </summary>
	public Serializer(
		Func<string?, T>? deserializer,
		Func<T, string?>? serializer = null,
		Func<Stream, CancellationToken, ValueTask<T>>? deserializerAsync = null,
		Func<Stream, T, CancellationToken, ValueTask>? serializerAsync = null)
	{
		if (deserializer is null && serializer is null && deserializerAsync is null && serializerAsync is null)
			throw new ArgumentNullException(nameof(deserializer), "At least one of the serialization or deserialization functions must not be null.");
		Contract.EndContractBlock();

		_deserializer = deserializer;
		_serializer = serializer;
		_deserializerAsync = deserializerAsync ?? base.DeserializeAsync;
		_serializerAsync = serializerAsync ?? base.SerializeAsync;
	}

	/// <inheritdoc />
	public override T Deserialize(string? value)
		=> _deserializer is null
		? throw new NullReferenceException("No deserializer function was supplied.")
		: _deserializer(value);

	/// <inheritdoc />
	public override ValueTask<T> DeserializeAsync(Stream source, CancellationToken cancellationToken = default)
		=> _deserializerAsync(source, cancellationToken);

	/// <inheritdoc />
	public override string? Serialize(T item)
		=> _serializer is null
		? throw new NullReferenceException("No serializer function was supplied.")
		: _serializer(item);

	/// <inheritdoc />
	public override ValueTask SerializeAsync(Stream target, T item, CancellationToken cancellationToken = default)
		=> _serializerAsync(target, item, cancellationToken);
}
