using System;
using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonDecimalRoundingConverter : JsonDecimalConverter
	{
		public readonly int Maximum;
		public JsonDecimalRoundingConverter(int maximum)
		{
			if (maximum < 0)
				throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
			Maximum = maximum;
		}

		public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
			=> Math.Round(base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer), Maximum);

		public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
			=> base.WriteJson(writer, Math.Round(value, Maximum), serializer);
	}
}