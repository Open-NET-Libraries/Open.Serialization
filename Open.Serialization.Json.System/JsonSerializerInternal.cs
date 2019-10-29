using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Open.Serialization.Json.System
{
	internal class JsonSerializerInternal : JsonSerializerBase
	{
		readonly JsonSerializerOptions _options;
		internal JsonSerializerInternal(JsonSerializerOptions options)
		{
			_options = options;
		}
		
		public override T Deserialize<T>(string value)
			=> JsonSerializer.Deserialize<T>(value, _options);

		public override string Serialize<T>(T item)
			=> JsonSerializer.Serialize(item, _options);

		public override ValueTask<T> DeserializeAsync<T>(Stream stream)
			=> JsonSerializer.DeserializeAsync<T>(stream, _options);
	}

	internal class JsonSerializerInternal<T> : Serializer<T>, IJsonSerializer<T>
	{
		internal JsonSerializerInternal(JsonSerializerOptions options)
			:base(options.GetDeserialize<T>(), options.GetSerialize<T>())
		{
		}
	}
}
