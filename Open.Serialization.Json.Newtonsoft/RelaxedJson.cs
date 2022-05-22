using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Open.Serialization.Json.Newtonsoft.Converters;

namespace Open.Serialization.Json.Newtonsoft;

/// <summary>
/// Shortcut for accessing default relaxed JSON options.
/// </summary>
public static class RelaxedJson
{
	internal static JsonSerializerSettings Options(
		bool indent = false)
		=> new JsonSerializerSettings()
		{
			Formatting = indent ? Formatting.Indented : Formatting.None,
			FloatParseHandling = FloatParseHandling.Decimal,
			ContractResolver = new DefaultContractResolver()
			{
				NamingStrategy = new DefaultNamingStrategy() { ProcessDictionaryKeys = false }
			}
		}
		.AddConverter(JsonNullableDoubleConverter.Instance)
		.NormalizeDecimals();

	/// <summary>
	/// Returns an <see cref="IJsonDeserialize"/>.s
	/// </summary>
	public static IJsonDeserialize<TValue> GetDeserializer<TValue>()
		=> DeserializerOptions.GetSerializer<TValue>();

	/// <summary>
	/// Returns an <see cref="IJsonDeserialize"/>.
	/// </summary>
	public static IJsonDeserialize GetDeserializer()
		=> DeserializerOptions.GetSerializer();

	static readonly JsonSerializerSettings DeserializerOptions
		= Options().SetNullValueHandling(NullValueHandling.Ignore);

	/// <inheritdoc cref="IDeserialize.Deserialize{T}(string)"/>
	public static TValue Deserialize<TValue>(string value)
		=> DeserializerOptions.Deserialize<TValue>(value);
}
