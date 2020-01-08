using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public abstract class ObjectSerializerBase : IObjectSerializer, IAsyncObjectSerializer
	{
		public abstract string? Serialize(object? item, Type type);
		public abstract object? Deserialize(string? value, Type type);

		public virtual async ValueTask SerializeAsync(Stream target, object? item, Type type, CancellationToken cancellationToken = default)
		{
			var text = Serialize(item, type);
			using var writer = new StreamWriter(target);
			await writer.WriteAsync(text).ConfigureAwait(false);
		}

		public async virtual ValueTask<object?> DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default)
		{
			string text;
			using (var reader = new StreamReader(source))
				text = await reader.ReadToEndAsync().ConfigureAwait(false);
			return Deserialize(text, type);
		}
	}
}
