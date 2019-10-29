using System;
using System.Threading;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Open.Serialization.Json.Utf8Json
{
	public class JsonSerializerFactory : JsonSerializerFactoryBase
	{
		readonly IJsonFormatterResolver _resolver;
		readonly bool _indent;
		public JsonSerializerFactory(IJsonFormatterResolver defaultResolver = null, bool indent = false)
		{
			_resolver = defaultResolver ?? StandardResolver.Default;
			_indent = indent;
		}

		JsonSerializerInternal _default;
		JsonSerializerInternal Default => LazyInitializer.EnsureInitialized(ref _default, () => new JsonSerializerInternal(_resolver, _indent));

		public override IJsonSerializer GetSerializer(IJsonSerializationOptions options = null, bool caseSensitive = false)
		{
			if (caseSensitive)
				throw new NotSupportedException("Utf8Json does not support case-sensitive deserialization.");

			if (options == null)
				return Default;

			if (options.CamelCaseKeys)
				throw new NotSupportedException("Utf8Json does not support camel casing keys.");

			return options.CamelCaseProperties
				? new JsonSerializerInternal(options.OmitNull ? StandardResolver.ExcludeNullCamelCase : StandardResolver.CamelCase, options.Indent)
				: new JsonSerializerInternal(options.OmitNull ? StandardResolver.ExcludeNull : StandardResolver.Default, options.Indent);
		}
	}
}
