using Newtonsoft.Json;
using System;

namespace Open.Serialization.Json.Newtonsoft.Converters;

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
	public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
		=> Math.Round(base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer), Maximum);

	/// <inheritdoc />
	public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
		=> base.WriteJson(writer, Math.Round(value, Maximum), serializer);
}
