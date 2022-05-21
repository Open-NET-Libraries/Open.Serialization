using Open.Serialization.Json.System.Converters;
using System;
using System.Text.Json;

namespace Open.Serialization.Json.System;

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

	public static IJsonDeserialize<TValue> GetDeserializer<TValue>()
		=> DeserializerOptions.GetSerializer<TValue>();

	public static IJsonDeserialize GetDeserializer()
		=> DeserializerOptions.GetSerializer();

	public static TValue Deserialize<TValue>(string? value)
		=> DeserializerOptions.Deserialize<TValue>(value!);

	public static TValue Deserialize<TValue>(ReadOnlySpan<byte> value)
		=> DeserializerOptions.Deserialize<TValue>(value);
}
