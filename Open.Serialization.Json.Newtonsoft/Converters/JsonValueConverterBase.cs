using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Open.Serialization.Json.Newtonsoft.Converters;

public abstract class JsonValueConverterBase<T> : JsonConverter<T>
{
	// Avoids stack overflow.
	private static readonly JsonSerializer Deserializer = JsonSerializer.Create();

	public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
		=> Deserializer.Deserialize<T>(reader);

	public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
#pragma warning disable CA1062 // Validate arguments of public methods
		=> writer.WriteRawValue(value?.ToString());
#pragma warning restore CA1062 // Validate arguments of public methods

	protected static decimal ConvertToDecimal(object value) => value switch
	{
		decimal d => d,
		BigInteger i => (decimal)i,
		IConvertible _ => Convert.ToDecimal(value),
		_ => throw new ArgumentException("Unable to convert to decimal.", nameof(value)),
	};

	protected static double ConvertToDouble(object value) => value switch
	{
		double d => d,
		BigInteger i => (double)i,
		IConvertible _ => Convert.ToDouble(value),
		_ => throw new ArgumentException("Unable to convert to double.", nameof(value)),
	};
}
