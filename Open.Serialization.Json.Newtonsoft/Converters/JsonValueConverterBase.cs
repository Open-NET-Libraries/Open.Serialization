using System;
using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public abstract class JsonValueConverterBase<T> : JsonConverter<T>
	{
		// Avoids stack overflow.
		private static readonly JsonSerializer Deserializer = JsonSerializer.Create();

		public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
			=> Deserializer.Deserialize<T>(reader);

		public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
			=> writer.WriteRawValue(value?.ToString());
	}
}