﻿using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization.Json.System;

internal class JsonSerializerInternal : JsonSerializerBase, IJsonSerializer
{
	readonly JsonSerializerOptions _options;
	internal JsonSerializerInternal(JsonSerializerOptions options)
	{
		_options = options ?? throw new ArgumentNullException(nameof(options));
	}

	public override T Deserialize<T>(string value)
		=> JsonSerializer.Deserialize<T>(value!, _options)!;

	ValueTask ISerializeAsync.SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken)
		=> new(JsonSerializer.SerializeAsync(stream, item, _options, cancellationToken));

	public new Task SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken = default)
		=> JsonSerializer.SerializeAsync(stream, item, _options, cancellationToken);

	public override string Serialize<T>(T item)
		=> JsonSerializer.Serialize(item, _options);

	public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		=> JsonSerializer.DeserializeAsync<T>(stream, _options)!;
}
