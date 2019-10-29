using System.Text.Json;
using System.Threading;

namespace Open.Serialization.Json.System
{
	public class JsonSerializerFactory : JsonSerializerFactoryBase
	{
		static readonly JsonSerializerOptions DefaultOptions = RelaxedJson.Options();
		readonly JsonSerializerOptions _options;
		public JsonSerializerFactory(JsonSerializerOptions defaultOptions = null)
		{
			_options = defaultOptions?.Clone() ?? DefaultOptions;
		}

		JsonSerializerInternal _caseSensitive;
		JsonSerializerInternal _ignoreCase;

		public override IJsonSerializer GetSerializer(IJsonSerializationOptions options = null, bool caseSensitive = false)
		{
			if (options == null)
			{
				return caseSensitive
					? LazyInitializer.EnsureInitialized(ref _caseSensitive,
						() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(false)))
					: LazyInitializer.EnsureInitialized(ref _ignoreCase,
						() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(true)));
			}

			var o = _options.Clone();
			o.IgnoreNullValues = options.OmitNull;
			o.WriteIndented = options.Indent;
			o.DictionaryKeyPolicy = options.CamelCaseKeys ? JsonNamingPolicy.CamelCase : null;
			o.PropertyNamingPolicy = options.CamelCaseProperties ? JsonNamingPolicy.CamelCase : null;

			return new JsonSerializerInternal(o);
		}
	}
}
