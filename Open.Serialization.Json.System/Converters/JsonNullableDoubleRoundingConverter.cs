using System;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters;

public class JsonNullableDoubleRoundingConverter : JsonNullableDoubleConverter
{
	public int Maximum { get; }
	public JsonNullableDoubleRoundingConverter(int maximum)
	{
		if (maximum < 0)
			throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
		Maximum = maximum;
	}

	/// <inheritdoc />
	public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.TokenType == JsonTokenType.Number
			? Math.Round(reader.GetDouble(), Maximum)
			: base.Read(ref reader, typeToConvert, options);

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
	{
		if (value.HasValue)
			value = Math.Round(value.Value, Maximum);

		base.Write(writer, value, options);
	}
}
