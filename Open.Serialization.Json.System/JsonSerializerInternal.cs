using System.Text.Json;

namespace Open.Serialization.Json.System
{
	internal class JsonSerializerInternal : SerializerBase, IJsonSerializer
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
	}

	internal class JsonSerializerInternal<T> : Serializer<T>, IJsonSerializer<T>
	{
		internal JsonSerializerInternal(JsonSerializerOptions options)
			:base(options.GetDeserialize<T>(), options.GetSerialize<T>())
		{
		}
	}
}
