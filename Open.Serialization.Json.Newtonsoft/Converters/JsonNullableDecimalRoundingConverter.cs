using Newtonsoft.Json;
using System;

namespace Open.Serialization.Json.Newtonsoft.Converters;

/// <summary>
/// Converter for nullable decimals that rounds to a maximum number of digits after the decimal.
/// </summary>
public class JsonNullableDecimalRoundingConverter : JsonNullableDecimalConverter
{
	/// <summary>
	/// The maximum number of digits after the decimal.
	/// </summary>
	public int Maximum { get; }

	/// <summary>
	/// Constructs a <see cref="JsonNullableDecimalRoundingConverter"/>.
	/// </summary>
	/// <param name="maximum">The maximum number of digits after the decimal.</param>
	/// <exception cref="ArgumentOutOfRangeException">If the maximum is less than zero.</exception>
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
