﻿using System;
using System.Threading;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Open.Serialization.Json.Utf8Json
{
	public class JsonSerializerFactory : IJsonSerializerFactory
	{
		readonly IJsonFormatterResolver _resolver;
		readonly bool _indent;
		public JsonSerializerFactory(IJsonFormatterResolver? defaultResolver = null, bool indent = false)
		{
			_resolver = defaultResolver ?? StandardResolver.Default;
			if (_resolver == StandardResolver.ExcludeNullSnakeCase || _resolver == StandardResolver.SnakeCase)
				throw new ArgumentOutOfRangeException(nameof(defaultResolver), "Snake case is not supported.");
			_indent = indent;
		}

		JsonSerializerInternal? _default;
		JsonSerializerInternal Default => LazyInitializer.EnsureInitialized(ref _default, () => new JsonSerializerInternal(_resolver, _indent))!;

		public IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		{
			if (caseSensitive)
				throw new NotSupportedException("Utf8Json does not support case-sensitive deserialization.");

			if (options == null)
				return Default;

			if (options.CamelCaseKeys == true)
				throw new NotSupportedException("Utf8Json does not support camel casing keys.");

			var omitNull = options.OmitNull == true || _resolver == StandardResolver.ExcludeNull || _resolver == StandardResolver.ExcludeNullCamelCase;
			var camelCase = options.CamelCaseProperties == true || _resolver == StandardResolver.CamelCase || _resolver == StandardResolver.ExcludeNullCamelCase;

			return camelCase
				? new JsonSerializerInternal(omitNull ? StandardResolver.ExcludeNullCamelCase : StandardResolver.CamelCase, options.Indent ?? _indent)
				: new JsonSerializerInternal(omitNull ? StandardResolver.ExcludeNull : StandardResolver.Default, options.Indent ?? _indent);
		}
	}
}
