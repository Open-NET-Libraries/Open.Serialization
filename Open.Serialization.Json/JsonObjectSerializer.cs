using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public class JsonObjectSerializer : ObjectSerializer, IJsonObjectSerializer, IJsonAsyncObjectSerializer
	{
		/// <inheritdoc />
		public JsonObjectSerializer(
			Func<string?, Type, object?>? deserializer,
			Func<object?, Type, string?>? serializer = null,
			Func<Stream, Type, CancellationToken, ValueTask<object?>>? deserializerAsync = null,
			Func<Stream, object?, Type, CancellationToken, ValueTask>? serializerAsync = null)
			: base(deserializer, serializer, deserializerAsync, serializerAsync)
		{
		}
	}
}
