using System;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters
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

		public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> Math.Round(reader.GetDecimal(), Maximum);

		public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
			=> base.Write(writer, Math.Round(value, Maximum), options);
	}
}