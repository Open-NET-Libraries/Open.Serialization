using System;
using System.Threading;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Open.Serialization.Json.Utf8Json
{
	public class JsonSerializerFactory : IJsonSerializerFactory
	{
		readonly IJsonFormatterResolver _resolver;
		readonly bool _indent;
		public JsonSerializerFactory(IJsonFormatterResolver? defaultResolver, bool indent = false)
		{
			_resolver = defaultResolver ?? StandardResolver.Default;
			if (_resolver == StandardResolver.ExcludeNullSnakeCase || _resolver == StandardResolver.SnakeCase)
				throw new ArgumentOutOfRangeException(nameof(defaultResolver), "Snake case is not supported.");
			_indent = indent;
		}

		public JsonSerializerFactory() : this(null)
		{
		}

		JsonSerializerInternal? _defaultSerializer;
		JsonSerializerInternal DefaultSerializer
			=> LazyInitializer.EnsureInitialized(ref _defaultSerializer, () => new JsonSerializerInternal(_resolver, _indent))!;

		static JsonSerializerFactory? _default;
		public static JsonSerializerFactory Default
			=> LazyInitializer.EnsureInitialized(ref _default)!;

		internal JsonSerializerInternal GetSerializerInternal(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		{
			if (caseSensitive)
				throw new NotSupportedException("Utf8Json does not support case-sensitive deserialization.");

			if (options == null)
				return DefaultSerializer;

			if (options.CamelCaseKeys == true)
				throw new NotSupportedException("Utf8Json does not support camel casing keys.");

			var omitNull = options.OmitNull == true || _resolver == StandardResolver.ExcludeNull || _resolver == StandardResolver.ExcludeNullCamelCase;
			var camelCase = options.CamelCaseProperties == true || _resolver == StandardResolver.CamelCase || _resolver == StandardResolver.ExcludeNullCamelCase;

			return camelCase
				? new JsonSerializerInternal(omitNull ? StandardResolver.ExcludeNullCamelCase : StandardResolver.CamelCase, options.Indent ?? _indent)
				: new JsonSerializerInternal(omitNull ? StandardResolver.ExcludeNull : StandardResolver.Default, options.Indent ?? _indent);
		}

		public IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
			=> GetSerializerInternal(options, caseSensitive);

		public IJsonObjectSerializer GetObjectSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
			=> GetSerializerInternal(options, caseSensitive);
	}
}
