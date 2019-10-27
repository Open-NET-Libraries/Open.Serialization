using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Open.Serialization.Json.Newtonsoft
{
	public static class CamelCaseJson
	{
		public static JsonSerializerSettings Default(bool indent = false)
		{
			var options = RelaxedJson.Options(indent);
			options.ContractResolver = new CamelCasePropertyNamesContractResolver();
			return options;
		}

		public static JsonSerializerSettings Minimal(bool indent = false)
		{
			var options = Default(indent);
			options.Formatting = indent ? Formatting.Indented : Formatting.None;
			return options;
		}
	}
}
