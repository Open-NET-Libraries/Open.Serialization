using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading;

namespace Open.Serialization.Json.Newtonsoft
{
	public class JsonSerializerFactory : IJsonSerializerFactory, IJsonObjectSerializerFactory
	{
		static readonly JsonSerializerSettings DefaultOptions = RelaxedJson.Options();
		readonly JsonSerializerSettings _settings;
		public JsonSerializerFactory(JsonSerializerSettings? defaultOptions)
		{
			_settings = defaultOptions?.Clone() ?? DefaultOptions;
		}

		public JsonSerializerFactory() : this(null)
		{
		}

		JsonSerializerInternal? _defaultSerializer;
		internal JsonSerializerInternal DefaultSerializer
			=> LazyInitializer.EnsureInitialized(ref _defaultSerializer, () => new JsonSerializerInternal(_settings))!;

		static JsonSerializerFactory? _default;
		public static JsonSerializerFactory Default
			=> LazyInitializer.EnsureInitialized(ref _default)!;

#if NETSTANDARD2_1
		[return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("options")]
#endif
		public JsonSerializerSettings? GetJsonSerializerSettings(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		{
			if (caseSensitive)
				throw new NotSupportedException("Newtonsoft does not support case-sensitive deserialization.");

			if (options == null) return null;

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

			return o;
		}

		internal JsonSerializerInternal GetSerializerInternal(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		{
			var o = GetJsonSerializerSettings(options, caseSensitive);
			return o == null ? DefaultSerializer : new JsonSerializerInternal(o);
		}

		public IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
			=> GetSerializerInternal(options, caseSensitive);

		public IJsonObjectSerializer GetObjectSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
			=> GetSerializerInternal(options, caseSensitive);
	}
}
