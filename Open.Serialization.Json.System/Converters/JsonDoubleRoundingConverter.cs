using System;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters
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

		public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> Math.Round(reader.GetDouble(), Maximum);

		public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
			=> writer.WriteNumberValue(Math.Round(value, Maximum));
	}
}