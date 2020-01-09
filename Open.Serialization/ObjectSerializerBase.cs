using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Base class for serializing and deserializing non-generic objects.
	/// </summary>
	public abstract class ObjectSerializerBase : SerializerBase, IObjectSerializer, IAsyncObjectSerializer
	{
		/// <inheritdoc />
		public abstract string? Serialize(object? item, Type type);

		/// <inheritdoc />
		public abstract object? Deserialize(string? value, Type type);

		/// <inheritdoc />
		public virtual async ValueTask SerializeAsync(Stream target, object? item, Type type, CancellationToken cancellationToken = default)
		{
			var text = Serialize(item, type);
			using var writer = new StreamWriter(target);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async virtual ValueTask<object?> DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default)
		{
			string text;
			using (var reader = new StreamReader(source))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return Deserialize(text, type);
		}

		/// <inheritdoc />
		public override string? Serialize<T>(T item)
			=> Serialize(item, typeof(T));

		/// <inheritdoc />
		public override T Deserialize<T>(string? value)
			=> (T)Deserialize(value, typeof(T))!;

		/// <inheritdoc />
		public override ValueTask SerializeAsync<T>(Stream target, T item, CancellationToken cancellationToken = default)
			=> SerializeAsync(target, item, typeof(T), cancellationToken);

		/// <inheritdoc />
		public override async ValueTask<T> DeserializeAsync<T>(Stream source, CancellationToken cancellationToken = default)
			=> (T)(await DeserializeAsync(source, typeof(T), cancellationToken))!;
	}
}
