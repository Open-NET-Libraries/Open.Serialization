using System;
using Utf8Json;

namespace Open.Serialization.Json.Utf8Json
{
	public static class SerializationExtensions
	{
		public static Func<string?, T> GetDeserialize<T>(this IJsonFormatterResolver options)
			=> json => JsonSerializer.Deserialize<T>(json, options);

		public static Func<T, string?> GetSerialize<T>(this IJsonFormatterResolver options, bool indent = false)
			=> item =>
			{
				var result = JsonSerializer.ToJsonString(item, options);
				return indent ? JsonSerializer.PrettyPrint(result) : result;
			};

		public static Func<object?, string?> GetSerialize(this IJsonFormatterResolver options, bool indent = false)
			=> item =>
			{
				var result = JsonSerializer.ToJsonString(item, options);
				return indent ? JsonSerializer.PrettyPrint(result) : result;
			};

		public static IJsonSerializer GetSerializer(this IJsonFormatterResolver options, bool indent = false)
			=> new JsonSerializerInternal(options, indent);

		public static IJsonSerializer<T> GetSerializer<T>(this IJsonFormatterResolver options, bool indent = false)
			=> new JsonSerializerInternal(options, indent).Cast<T>();

		public static IJsonSerializerFactory GetSerializerFactory(this IJsonFormatterResolver options)
			=> new JsonSerializerFactory(options);

		public static string? Serialize<TValue>(this IJsonFormatterResolver options, TValue value)
			=> JsonSerializer.ToJsonString(value, options);
		public static string? Serialize(this IJsonFormatterResolver options, object? value)
			=> JsonSerializer.ToJsonString(value, options);
		public static TValue Deserialize<TValue>(this IJsonFormatterResolver options, string? value)
			=> JsonSerializer.Deserialize<TValue>(value, options);

	}
}
