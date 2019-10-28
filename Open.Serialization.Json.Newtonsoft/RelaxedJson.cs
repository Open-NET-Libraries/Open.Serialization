using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Open.Serialization.Json.Newtonsoft.Converters;

namespace Open.Serialization.Json.Newtonsoft
{
	public static class RelaxedJson
	{
		internal static JsonSerializerSettings Options(
			bool indent = false)
			=> new JsonSerializerSettings()
			{
				Formatting = indent ? Formatting.Indented : Formatting.None,
				ContractResolver = new DefaultContractResolver()
				{
					NamingStrategy = new DefaultNamingStrategy() { ProcessDictionaryKeys = false }
				}
			}
			.AddConverter(JsonNullableDoubleConverter.Instance)
			.NormalizeDecimals();

		static readonly JsonSerializerSettings DeserializerOptions
			= Options().SetNullValueHandling(NullValueHandling.Ignore);

		public static TValue Deserialize<TValue>(string value)
			=> DeserializerOptions.Deserialize<TValue>(value);
	}
}
