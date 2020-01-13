using Microsoft.Extensions.DependencyInjection;
using System;
using Utf8Json;

namespace Open.Serialization.Json.Utf8Json
{
	public static class SerializationExtensions
	{
		/// <summary>
		/// Adds a generic serializer (<code>IJsonSerializer</code>) and non-generic (<code>IJsonObjectSerializer</code>) to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="options">The options overrides.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddJsonSerializer(this IServiceCollection services, IJsonSerializationOptions? options = null)
		{
			var factory = JsonSerializerFactory.Default;
			var serializer = factory.GetSerializerInternal(options);
			services.AddSingleton<IJsonSerializer>(serializer);
			services.AddSingleton<IJsonObjectSerializer>(serializer);
			return services;
		}

		/// <summary>
		/// Adds a generic serializer (<code>IJsonSerializer<typeparamref name="T"/></code>) to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="options">The options overrides.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddJsonSerializer<T>(this IServiceCollection services, IJsonSerializationOptions? options = null)
		{
			var factory = JsonSerializerFactory.Default;
			services.AddSingleton<IJsonSerializer<T>>(factory.GetSerializerInternal(options).Cast<T>());
			return services;
		}

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
