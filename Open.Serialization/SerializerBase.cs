﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization;

/// <summary>
/// Base class for implementing a serializer/deserializer.
/// </summary>
public abstract class SerializerBase : ISerializer, IAsyncSerializer
{
	/// <inheritdoc />
	public abstract T Deserialize<T>(string value);

	/// <inheritdoc />
	public virtual ValueTask<T> DeserializeAsync<T>(Stream source, CancellationToken cancellationToken = default)
		=> DefaultMethods.DeserializeAsync<T>(this, source);

	/// <inheritdoc />
	public abstract string Serialize<T>(T item);

	/// <inheritdoc />
	public virtual ValueTask SerializeAsync<T>(Stream target, T item, CancellationToken cancellationToken = default)
		=> DefaultMethods.SerializeAsync(this, target, item);

	/// <summary>
	/// Creates a type specific serializer using this as the underlying serializer.
	/// </summary>
	/// <returns>A type specific serializer.</returns>
	public virtual Serializer<T> Cast<T>()
		=> new(Deserialize<T>, Serialize, DeserializeAsync<T>, SerializeAsync);
}

/// <summary>
/// Base calss for implementing a spedific generic type serializer/deserializer.
/// </summary>
public abstract class SerializerBase<T> : ISerializer<T>, IAsyncSerializer<T>
{
	/// <inheritdoc />
	public abstract T Deserialize(string value);

	/// <inheritdoc />
	public virtual T Deserialize(ReadOnlySpan<char> value)
		=> Deserialize(value.ToString());

	/// <inheritdoc />
	public virtual ValueTask<T> DeserializeAsync(Stream source, CancellationToken cancellationToken = default)
		=> DefaultMethods.DeserializeAsync(this, source);

	/// <inheritdoc />
	public abstract string Serialize(T item);

	/// <inheritdoc />
	public virtual ValueTask SerializeAsync(Stream target, T item, CancellationToken cancellationToken = default)
		=> DefaultMethods.SerializeAsync(this, target, item);
}
