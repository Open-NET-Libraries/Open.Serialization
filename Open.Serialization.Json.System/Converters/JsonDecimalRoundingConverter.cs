using System;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters;

/// <summary>
/// Converter for decimals that rounds to a maximum number of digits after the decimal.
/// </summary>
public class JsonDecimalRoundingConverter : JsonDecimalConverter
{
	/// <summary>
	/// The maximum number of digits after the decimal.
	/// </summary>
	public int Maximum { get; }

	/// <summary>
	/// Constructs a <see cref="JsonDecimalRoundingConverter"/>.
	/// </summary>
	/// <param name="maximum">The maximum number of digits after the decimal.</param>
	/// <exception cref="ArgumentOutOfRangeException">If the maximum is less than zero.</exception>
	public JsonDecimalRoundingConverter(int maximum)
	{
		if (maximum < 0)
			throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
		Maximum = maximum;
	}

	/// <inheritdoc />
	public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> Math.Round(reader.GetDecimal(), Maximum);

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
		=> base.Write(writer, Math.Round(value, Maximum), options);
}
