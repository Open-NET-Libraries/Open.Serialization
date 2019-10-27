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
		public JsonSerializerFactory(JsonSerializerSettings defaultOptions = null)
		{
			_settings = defaultOptions?.Clone() ?? DefaultOptions;
		}

		JsonSerializerInternal _default;
		JsonSerializerInternal Default => LazyInitializer.EnsureInitialized(ref _default, () => new JsonSerializerInternal(_settings));

		protected override SerializerBase GetDeserializerInternal(bool caseSensitive)
			=> caseSensitive
			? throw new NotSupportedException("Newtonsoft does not support case-sensitive deserialization.")
			: Default;

		protected override SerializerBase GetSerializerInternal(IJsonSerializationOptions options)
		{
			if (options == null) return Default;

			if (options.CamelCaseKeys && !options.CamelCaseProperties)
				throw new NotSupportedException("Camel casing keys but not properties is not supported.");

			var o = _settings.Clone();
			if (options.CamelCaseKeys)
			{
				o.ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy
					{
						ProcessDictionaryKeys = true
					}
				};
			}
			else if (options.CamelCaseProperties)
			{
				o.ContractResolver = new CamelCasePropertyNamesContractResolver();
			}
			else
			{
				o.ContractResolver = new DefaultContractResolver();
			}


			o.NullValueHandling = options.OmitNull ? NullValueHandling.Ignore : NullValueHandling.Include;
			o.Formatting = options.Indent ? Formatting.Indented : Formatting.None;

			return new JsonSerializerInternal(o);
		}
	}
}
