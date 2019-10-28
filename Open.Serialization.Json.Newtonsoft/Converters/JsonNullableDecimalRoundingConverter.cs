using System;
using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonNullableDecimalRoundingConverter : JsonNullableDecimalConverter
	{
		public readonly int Maximum;
		public JsonNullableDecimalRoundingConverter(int maximum)
		{
			if (maximum < 0)
				throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
			Maximum = maximum;
		}

		public override decimal? ReadJson(JsonReader reader, Type objectType, decimal? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var value = base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer);
			return value.HasValue ? Math.Round(value.Value, Maximum) : value;
		}

		public override void WriteJson(JsonWriter writer, decimal? value, JsonSerializer serializer)
		{
			if (value.HasValue)
				value = Math.Round(value.Value, Maximum);

			base.WriteJson(writer, value, serializer);
		}
	}
}