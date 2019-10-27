using System;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters
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

		public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Number)
				return Math.Round(reader.GetDecimal(), Maximum);

			return base.Read(ref reader, typeToConvert, options);
		}

		public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
		{
			if (value.HasValue)
				value = Math.Round(value.Value, Maximum);

			base.Write(writer, value, options);
		}
	}
}