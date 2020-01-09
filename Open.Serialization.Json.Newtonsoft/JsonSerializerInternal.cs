using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft
{
	internal class JsonSerializerInternal : JsonSerializerBase
	{
		readonly JsonSerializerSettings _settings;
		internal JsonSerializerInternal(JsonSerializerSettings settings)
		{
			_settings = settings;
		}

		public override T Deserialize<T>(string? value)
			=> JsonConvert.DeserializeObject<T>(value, _settings);

		public override string? Serialize<T>(T item)
			=> JsonConvert.SerializeObject(item, _settings);
	}

	internal class JsonSerializerInternal<T> : JsonSerializer<T>, IJsonSerializer<T>, IJsonAsyncSerializer<T>
	{
		internal JsonSerializerInternal(JsonSerializerSettings settings)
			: base(settings.GetDeserialize<T>(), settings.GetSerialize<T>())
		{
		}
	}
}
