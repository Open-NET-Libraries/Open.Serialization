using Newtonsoft.Json;
using Open.Serialization.Json.Newtonsoft.Converters;

namespace Open.Serialization.Json.Newtonsoft
{
	public static class RelaxedJson
	{
		internal static JsonSerializerSettings Options(
			bool indent = false)
			=> new JsonSerializerSettings()
			{
				Formatting = indent ? Formatting.Indented : Formatting.None
			}
			.AddConverter(JsonNullableDoubleConverter.Instance)
			.NormalizeDecimals();

		static readonly JsonSerializerSettings DeserializerOptions
			= Options().SetNullValueHandling(NullValueHandling.Ignore);

		public static TValue Deserialize<TValue>(string value)
			=> DeserializerOptions.Deserialize<TValue>(value);
	}
}
