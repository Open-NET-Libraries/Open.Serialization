using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Open.Serialization.Json.Newtonsoft.Converters;

/// <summary>
/// Base JsonConverter for NewtsonSoft.Json.
/// </summary>
public abstract class JsonValueConverterBase<T> : JsonConverter<T>
{
	// Avoids stack overflow.
	private static readonly JsonSerializer Deserializer = JsonSerializer.Create();

	/// <inheritdoc />
	public override T ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
		=> Deserializer.Deserialize<T>(reader)!;

	/// <inheritdoc />
	public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
#pragma warning disable CA1062 // Validate arguments of public methods
		=> writer.WriteRawValue(value?.ToString());
#pragma warning restore CA1062 // Validate arguments of public methods

	/// <summary>
	/// Special convert to decimal from object.
	/// </summary>
	protected static decimal ConvertToDecimal(object value) => value switch
	{
		decimal d => d,
		BigInteger i => (decimal)i,
		IConvertible _ => Convert.ToDecimal(value),
		_ => throw new ArgumentException("Unable to convert to decimal.", nameof(value)),
	};

	/// <summary>
	/// Special convert to decimal from object.
	/// </summary>
	protected static double ConvertToDouble(object value) => value switch
	{
		double d => d,
		BigInteger i => (double)i,
		IConvertible _ => Convert.ToDouble(value),
		_ => throw new ArgumentException("Unable to convert to double.", nameof(value)),
	};
}
