using System.Text.Json;
using System.Threading;

namespace Open.Serialization.Json.System
{
	public class JsonSerializerFactory : IJsonSerializerFactory
	{
		static readonly JsonSerializerOptions DefaultOptions = RelaxedJson.Options();
		readonly JsonSerializerOptions _options;
		public JsonSerializerFactory(JsonSerializerOptions defaultOptions = null)
		{
			_options = defaultOptions?.Clone() ?? DefaultOptions;
		}

		JsonSerializerInternal _default;
		JsonSerializerInternal _caseSensitive;
		JsonSerializerInternal _ignoreCase;

		JsonSerializerInternal GetSerializerCore(bool caseSensitive)
			=> caseSensitive
				? LazyInitializer.EnsureInitialized(ref _caseSensitive,
					() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(false)))
				: LazyInitializer.EnsureInitialized(ref _ignoreCase,
					() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(true)));
		
		public IDeserialize GetDeserializer(bool caseSensitive = false)
			=> GetSerializerCore(caseSensitive);

		public IDeserializeAsync GetAsyncDeserializer(bool caseSensitive = false)
			=> GetSerializerCore(caseSensitive);

		JsonSerializerInternal GetSerializerCore(IJsonSerializationOptions options)
		{
			if (options == null)
				return LazyInitializer.EnsureInitialized(ref _default, () => new JsonSerializerInternal(_options));

			var o = _options.Clone();
			o.IgnoreNullValues = options.OmitNull;
			o.WriteIndented = options.Indent;
			o.DictionaryKeyPolicy = options.CamelCaseKeys ? JsonNamingPolicy.CamelCase : null;
			o.PropertyNamingPolicy = options.CamelCaseProperties ? JsonNamingPolicy.CamelCase : null;

			return new JsonSerializerInternal(o);
		}

		public ISerialize GetSerializer(IJsonSerializationOptions options = null)
			=> GetSerializerCore(options);

		public ISerializeAsync GetAsyncSerializer(IJsonSerializationOptions options = null)
			=> GetSerializerCore(options);
	}
}
