using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Open.Serialization.Json.Newtonsoft
{
	public static class CamelCaseJson
	{
		public static JsonSerializerSettings Default(bool indent = false)
		{
			var options = RelaxedJson.Options(indent);
			options.ContractResolver = new CamelCasePropertyNamesContractResolver()
			{
				NamingStrategy = new CamelCaseNamingStrategy() { ProcessDictionaryKeys = false }
			};
			return options;
		}

		public static JsonSerializerSettings Minimal(bool indent = false)
		{
			var options = Default(indent);
			options.NullValueHandling = NullValueHandling.Ignore;
			return options;
		}
	}
}
