using Open.Serialization.Json.System.Converters;
using System;
using System.Text.Json;

namespace Open.Serialization.Json.System;

/// <summary>
/// Shortcut for accessing default relaxed JSON options.
/// </summary>
public static class RelaxedJson
{
	internal static JsonSerializerOptions Options(
		bool indent = false,
		bool caseSensitive = false)
		=> new JsonSerializerOptions()
		{
			PropertyNameCaseInsensitive = !caseSensitive,
			AllowTrailingCommas = true,
			WriteIndented = indent
		}
		.AddConverter(JsonNullableDoubleConverter.Instance)
		.NormalizeDecimals();

	static readonly JsonSerializerOptions DeserializerOptions
		= Options().SetIgnoreNullValues();

	/// <summary>
	/// Returns an <see cref="IJsonDeserialize{TValue}"/>.
	/// </summary>
	public static IJsonDeserialize<TValue> GetDeserializer<TValue>()
		=> DeserializerOptions.GetSerializer<TValue>();

	/// <summary>
	/// Returns an <see cref="IJsonDeserialize"/>.
	/// </summary>
	public static IJsonDeserialize GetDeserializer()
		=> DeserializerOptions.GetSerializer();

	/// <summary>
	/// Deserializes <paramref name="value"/> to <typeparamref name="TValue"/>.
	/// </summary>
	public static TValue Deserialize<TValue>(string value)
		=> DeserializerOptions.Deserialize<TValue>(value);

	/// <summary>
	/// Deserializes <paramref name="value"/> to <typeparamref name="TValue"/>.
	/// </summary>
	public static TValue Deserialize<TValue>(ReadOnlySpan<char> value)
		=> DeserializerOptions.Deserialize<TValue>(value);

	/// <summary>
	/// Deserializes <paramref name="value"/> to <typeparamref name="TValue"/>.
	/// </summary>
	public static TValue Deserialize<TValue>(ReadOnlySpan<byte> value)
		=> DeserializerOptions.Deserialize<TValue>(value);
}
