using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization;

/// <summary>
/// A class for serializing and deserializing objects.
/// </summary>
public abstract class ObjectSerializer : ObjectSerializerBase
{
	private readonly Func<string?, Type, object?>? _deserializer;
	private readonly Func<object?, Type, string?>? _serializer;
	private readonly Func<Stream, Type, CancellationToken, ValueTask<object?>> _deserializerAsync;
	private readonly Func<Stream, object?, Type, CancellationToken, ValueTask> _serializerAsync;

	/// <summary>
	/// Constructs a serializer/deserializer using the provided serialization functions.
	/// </summary>
	protected ObjectSerializer(
		Func<string?, Type, object?>? deserializer,
		Func<object?, Type, string?>? serializer = null,
		Func<Stream, Type, CancellationToken, ValueTask<object?>>? deserializerAsync = null,
		Func<Stream, object?, Type, CancellationToken, ValueTask>? serializerAsync = null)
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
	public override object? Deserialize(string? value, Type type)
		=> _deserializer is null
		? throw new NullReferenceException("No deserializer function was supplied.")
		: _deserializer(value, type);

	/// <inheritdoc />
	public override ValueTask<object?> DeserializeAsync(Stream stream, Type type, CancellationToken cancellationToken = default)
		=> _deserializerAsync(stream, type, cancellationToken);

	/// <inheritdoc />
	public override string? Serialize(object? item, Type type)
		=> _serializer is null
		? throw new NullReferenceException("No serializer function was supplied.")
		: _serializer(item, type);

	/// <inheritdoc />
	public override ValueTask SerializeAsync(Stream stream, object? item, Type type, CancellationToken cancellationToken = default)
		=> _serializerAsync(stream, item, type, cancellationToken);
}
