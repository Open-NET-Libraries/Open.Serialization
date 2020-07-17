using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization.Extensions
{
	/// <summary>
	/// Default extensions for pure serialization interfaces. 
	/// </summary>
	public static class SerializationExtensions
	{
		/// <inheritdoc cref="IDeserialize.Deserialize{T}(string)" />
		public static T Deserialize<T>(this IDeserializeObject deserializer, string? value)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			return deserializer is IDeserialize d
				? d.Deserialize<T>(value)
				: (T)deserializer.Deserialize(value, typeof(T))!;
		}

		/// <inheritdoc cref="ISerialize.Serialize{T}(T)" />
		public static string? Serialize<T>(this ISerializeObject serializer, T item)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return serializer is ISerialize s
				? s.Serialize(item)
				: serializer.Serialize(item, typeof(T));
		}

		/// <inheritdoc cref="IDeserializeAsync.DeserializeAsync{T}(Stream, CancellationToken)" />
		public static async ValueTask<T> DeserializeAsync<T>(this IDeserializeObject deserializer, Stream source, CancellationToken cancellationToken = default)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			return deserializer is IDeserializeAsync d
				? await d.DeserializeAsync<T>(source, cancellationToken).ConfigureAwait(false)
				: (T)(await DeserializeAsync(deserializer, source, typeof(T), cancellationToken).ConfigureAwait(false))!;
		}

		/// <inheritdoc cref="IDeserializeObjectAsync.DeserializeAsync(Stream, Type, CancellationToken)" />
		public static ValueTask<object?> DeserializeAsync(this IDeserializeObject deserializer, Stream source, Type type, CancellationToken cancellationToken = default)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			return deserializer is IDeserializeObjectAsync d
				? d.DeserializeAsync(source, type, cancellationToken)
				: DefaultMethods.DeserializeAsync(deserializer, source, type);
		}

		/// <inheritdoc cref="IDeserializeAsync.DeserializeAsync{T}(Stream, CancellationToken)" />
		public static async ValueTask<T> DeserializeAsync<T>(this IDeserializeObjectAsync deserializer, Stream source, CancellationToken cancellationToken = default)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			return deserializer is IDeserializeAsync d
				? await d.DeserializeAsync<T>(source, cancellationToken).ConfigureAwait(false)
				: (T)(await deserializer.DeserializeAsync(source, typeof(T), cancellationToken).ConfigureAwait(false))!;
		}

		/// <inheritdoc cref="ISerializeAsync.SerializeAsync{T}(Stream, T, CancellationToken)"/>
		public static ValueTask SerializeAsync<T>(this ISerialize serializer, Stream target, T item, CancellationToken cancellationToken = default)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return serializer is ISerializeAsync s
				? s.SerializeAsync(target, item, cancellationToken)
				: DefaultMethods.SerializeAsync(serializer, target, item);
		}

		/// <inheritdoc cref="ISerializeObjectAsync.SerializeAsync(Stream, object, Type, CancellationToken)"/>
		public static ValueTask SerializeAsync(this ISerializeObject serializer, Stream target, object? item, Type type, CancellationToken cancellationToken = default)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return serializer is ISerializeObjectAsync s
				? s.SerializeAsync(target, item, type, cancellationToken)
				: DefaultMethods.SerializeAsync(serializer, target, item, type);
		}

		/// <inheritdoc cref="ISerializeAsync.SerializeAsync{T}(Stream, T, CancellationToken)" />
		public static ValueTask SerializeAsync<T>(this ISerializeObject serializer, Stream target, T item, CancellationToken cancellationToken = default)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return serializer is ISerializeAsync s
				? s.SerializeAsync(target, item, cancellationToken)
				: SerializeAsync(serializer, target, item, typeof(T), cancellationToken);
		}

		/// <inheritdoc cref="IDeserializeAsync.DeserializeAsync{T}(Stream, CancellationToken)"/>
		public static ValueTask<T> DeserializeAsync<T>(this IDeserialize deserializer, Stream source, CancellationToken cancellationToken = default)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			return deserializer is IDeserializeAsync d
				? d.DeserializeAsync<T>(source, cancellationToken)
				: DefaultMethods.DeserializeAsync<T>(deserializer, source);
		}

		/// <inheritdoc cref="IDeserializeAsync{T}.DeserializeAsync(Stream, CancellationToken)"/>
		public static ValueTask<T> DeserializeAsync<T>(this IDeserialize<T> deserializer, Stream source, CancellationToken cancellationToken = default)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			return deserializer is IDeserializeAsync<T> d
				? d.DeserializeAsync(source, cancellationToken)
				: DefaultMethods.DeserializeAsync(deserializer, source);
		}

		/// <inheritdoc cref="ISerializeAsync{T}.SerializeAsync(Stream, T, CancellationToken)"/>
		public static ValueTask SerializeAsync<T>(this ISerialize<T> serializer, Stream target, T item, CancellationToken cancellationToken = default)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return serializer is ISerializeAsync s
				? s.SerializeAsync(target, item, cancellationToken)
				: DefaultMethods.SerializeAsync(serializer, target, item);
		}

		/// <inheritdoc cref="ISerializeAsync.SerializeAsync{T}(Stream, T, CancellationToken)" />
		public static ValueTask SerializeAsync<T>(this ISerializeObjectAsync serializer, Stream target, T item, CancellationToken cancellationToken)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return serializer is ISerializeAsync s
				? s.SerializeAsync(target, item, cancellationToken)
				: serializer.SerializeAsync(target, item, typeof(T), cancellationToken);
		}


		/// <summary>
		/// Creates a type specific serializer using this as the underlying serializer.
		/// </summary>
		/// <returns>A type specific serializer.</returns>
		public static Serializer<T> Cast<T>(this ISerializer serializer)
		{
			if (serializer is null)
				throw new ArgumentNullException(nameof(serializer));

			if (serializer is SerializerBase sb)
				return sb.Cast<T>();

			return serializer is IAsyncSerializer a
				? new Serializer<T>(
					serializer.Deserialize<T>, serializer.Serialize<T>,
					a.DeserializeAsync<T>, a.SerializeAsync<T>)
				: new Serializer<T>(
					serializer.Deserialize<T>, serializer.Serialize<T>);
		}
	}
}
