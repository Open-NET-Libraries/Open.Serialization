using System.Text.Json;
using System.Threading;

namespace Open.Serialization.Json.System
{
	public class JsonSerializerFactory : JsonSerializerFactoryBase
	{
		static readonly JsonSerializerOptions DefaultOptions = RelaxedJson.Options();
		readonly JsonSerializerOptions _options;
		public JsonSerializerFactory(JsonSerializerOptions? defaultOptions = null)
		{
			_options = defaultOptions?.Clone() ?? DefaultOptions;
		}

		JsonSerializerInternal? _caseSensitive;
		JsonSerializerInternal? _ignoreCase;

		public override IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		{
			if (options == null)
			{
#pragma warning disable CS8603 // Possible null reference return.
				return caseSensitive
					? LazyInitializer.EnsureInitialized(ref _caseSensitive,
						() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(false)))
					: LazyInitializer.EnsureInitialized(ref _ignoreCase,
						() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(true)));
#pragma warning restore CS8603 // Possible null reference return.
			}

			var o = _options.Clone();
			o.IgnoreNullValues = options.OmitNull ?? o.IgnoreNullValues;
			o.WriteIndented = options.Indent ?? o.WriteIndented;
			o.DictionaryKeyPolicy = options.CamelCaseKeys == true ? JsonNamingPolicy.CamelCase : o.DictionaryKeyPolicy;
			o.PropertyNamingPolicy = options.CamelCaseProperties == true ? JsonNamingPolicy.CamelCase : o.PropertyNamingPolicy;

			return new JsonSerializerInternal(o);
		}
	}
}
