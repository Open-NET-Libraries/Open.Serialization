using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading;

namespace Open.Serialization.Json.Newtonsoft
{
	public class JsonSerializerFactory : JsonSerializerFactoryBase
	{
		static readonly JsonSerializerSettings DefaultOptions = RelaxedJson.Options();
		readonly JsonSerializerSettings _settings;
		public JsonSerializerFactory(JsonSerializerSettings? defaultOptions = null)
		{
			_settings = defaultOptions?.Clone() ?? DefaultOptions;
		}

		JsonSerializerInternal? _default;
#pragma warning disable CS8603 // Possible null reference return.
		JsonSerializerInternal Default => LazyInitializer.EnsureInitialized(ref _default, () => new JsonSerializerInternal(_settings));
#pragma warning restore CS8603 // Possible null reference return.

		public override IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		{
			if (caseSensitive)
				throw new NotSupportedException("Newtonsoft does not support case-sensitive deserialization.");

			if (options == null) return Default;

			if (options.CamelCaseKeys == true && options.CamelCaseProperties != true)
				throw new NotSupportedException("Camel casing keys but not properties is not supported.");

			var o = _settings.Clone();
			if (options.CamelCaseKeys == true)
			{
				o.ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy
					{
						ProcessDictionaryKeys = true
					}
				};
			}
			else if (options.CamelCaseProperties == true)
			{
				o.ContractResolver = new CamelCasePropertyNamesContractResolver();
			}
			else if (options.CamelCaseProperties == false)
			{
				o.ContractResolver = new DefaultContractResolver();
			}

			if (options.OmitNull.HasValue)
				o.NullValueHandling = options.OmitNull.Value ? NullValueHandling.Ignore : NullValueHandling.Include;

			if (options.Indent.HasValue)
				o.Formatting = options.Indent.Value ? Formatting.Indented : Formatting.None;

			return new JsonSerializerInternal(o);
		}
	}
}
