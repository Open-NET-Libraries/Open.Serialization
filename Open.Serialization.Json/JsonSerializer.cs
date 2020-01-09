using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public class JsonSerializer<T> : Serializer<T>, IJsonSerializer<T>, IJsonAsyncSerializer<T>
	{
		/// <inheritdoc />
		public JsonSerializer(
			Func<string?, T>? deserializer,
			Func<T, string?>? serializer = null,
			Func<Stream, CancellationToken, ValueTask<T>>? deserializerAsync = null,
			Func<Stream, T, CancellationToken, ValueTask>? serializerAsync = null)
			: base(deserializer, serializer, deserializerAsync, serializerAsync)
		{
		}
	}
}
