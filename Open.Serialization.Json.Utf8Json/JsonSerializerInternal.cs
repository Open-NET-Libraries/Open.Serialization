using Utf8Json;

namespace Open.Serialization.Json.System
{
	internal class JsonSerializerInternal : SerializerBase, IJsonSerializer
	{
		readonly IJsonFormatterResolver _options;
		internal JsonSerializerInternal(IJsonFormatterResolver options)
		{
			_options = options;
		}
		
		public override T Deserialize<T>(string value)
			=> JsonSerializer.Deserialize<T>(value, _options);

		public override string Serialize<T>(T item)
			=> JsonSerializer.ToJsonString(item, _options);
	}

	internal class JsonSerializerInternal<T> : Serializer<T>, IJsonSerializer<T>
	{
		internal JsonSerializerInternal(IJsonFormatterResolver options)
			:base(options.GetDeserialize<T>(), options.GetSerialize<T>())
		{
		}
	}
}
