using System;
using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonDoubleRoundingConverter : JsonValueConverterBase<double>
	{
		public readonly int Maximum;
		public JsonDoubleRoundingConverter(int maximum)
		{
			if (maximum < 0)
				throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
			Maximum = maximum;
		}

		public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
			=> Math.Round((double)reader.Value, Maximum);

		public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
			=> writer.WriteValue(Math.Round(value, Maximum));
	}
}