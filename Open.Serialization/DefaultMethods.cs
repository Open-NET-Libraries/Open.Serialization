using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Default utiltiy methods for use with implementing interfaces/classes.
	/// </summary>
	// Note: not implemented as extensions as it would cause ambiguity collisions
	// But retained public for use externally.
	public static class DefaultMethods
	{
		/// <inheritdoc cref="IDeserializeObjectAsync.DeserializeAsync(Stream, Type, CancellationToken))" />
		public static async ValueTask<object?> DeserializeAsync(IDeserializeObject deserializer, Stream source, Type type)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			if (source is null) throw new ArgumentNullException(nameof(source));
			if (type is null) throw new ArgumentNullException(nameof(type));
			string text;
			using (var reader = new StreamReader(source))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return deserializer.Deserialize(text, type);
		}

		/// <inheritdoc cref="ISerializeObjectAsync.SerializeAsync(Stream, object, Type, CancellationToken)"/>
		public static async ValueTask SerializeAsync(ISerializeObject serializer, Stream target, object? item, Type type)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			if (target is null) throw new ArgumentNullException(nameof(target));
			if (type is null) throw new ArgumentNullException(nameof(type));
			var text = serializer.Serialize(item, type);
			using var writer = new StreamWriter(target);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}

		/// <inheritdoc cref="IDeserializeAsync.DeserializeAsync{T}(Stream, CancellationToken)"/>
		public static async ValueTask<T> DeserializeAsync<T>(IDeserialize deserializer, Stream source)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			if (source is null) throw new ArgumentNullException(nameof(source));
			string text;
			using (var reader = new StreamReader(source))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return deserializer.Deserialize<T>(text);
		}

		/// <inheritdoc cref="ISerializeAsync.SerializeAsync{T}(Stream, T, CancellationToken)"/>
		public static async ValueTask SerializeAsync<T>(ISerialize serializer, Stream target, T item)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			if (target is null)	throw new ArgumentNullException(nameof(target));

			var text = serializer.Serialize(item);
			using var writer = new StreamWriter(target);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}


		/// <inheritdoc cref="IDeserializeAsync{T}.DeserializeAsync(Stream, CancellationToken)"/>
		public static async ValueTask<T> DeserializeAsync<T>(IDeserialize<T> deserializer, Stream source)
		{
			if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
			if (source is null) throw new ArgumentNullException(nameof(source));
			string text;
			using (var reader = new StreamReader(source))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return deserializer.Deserialize(text);
		}

		/// <inheritdoc cref="ISerializeAsync{T}.SerializeAsync(Stream, T, CancellationToken)"/>
		public static async ValueTask SerializeAsync<T>(ISerialize<T> serializer, Stream target, T item)
		{
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			if (target is null) throw new ArgumentNullException(nameof(target));

			var text = serializer.Serialize(item);
			using var writer = new StreamWriter(target);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}

	}
}
