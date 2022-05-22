using System;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters;

/// <summary>
/// Converter for nullable doubles that rounds to a maximum number of digits after the decimal.
/// </summary>
public class JsonNullableDoubleRoundingConverter : JsonNullableDoubleConverter
{
	/// <summary>
	/// The maximum number of digits after the decimal.
	/// </summary>
	public int Maximum { get; }

	/// <summary>
	/// Constructs a <see cref="JsonNullableDoubleRoundingConverter"/>.
	/// </summary>
	/// <param name="maximum">The maximum number of digits after the decimal.</param>
	/// <exception cref="ArgumentOutOfRangeException">If the maximum is less than zero.</exception>
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
