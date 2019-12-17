using Newtonsoft.Json;
using System;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonNullableDoubleRoundingConverter : JsonNullableDoubleConverter
	{
		public int Maximum { get; }
		public JsonNullableDoubleRoundingConverter(int maximum)
		{
			if (maximum < 0)
				throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
			Maximum = maximum;
		}

		public override double? ReadJson(JsonReader reader, Type objectType, double? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var value = base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer);
			return value.HasValue ? Math.Round(value.Value, Maximum) : value;
		}

		public override void WriteJson(JsonWriter writer, double? value, JsonSerializer serializer)
		{
			if (value.HasValue)
				value = Math.Round(value.Value, Maximum);

			base.WriteJson(writer, value, serializer);
		}
	}
}