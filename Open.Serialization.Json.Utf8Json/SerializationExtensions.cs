using Microsoft.Extensions.DependencyInjection;
using System;
using Utf8Json;

namespace Open.Serialization.Json.Utf8Json;

/// <summary>
/// Extensions for Utf8Json serialization with Open.Serialization.Json.
/// </summary>
public static class SerializationExtensions
{
	/// <summary>
	/// Adds a generic serializer <see cref="IJsonSerializer"/>and non-generic <see cref="IJsonObjectSerializer"/> to the service collection.
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
	/// Adds a generic serializer <see cref="IJsonSerializer{T}"/> to the service collection.
	/// </summary>
	/// <inheritdoc cref="AddJsonSerializer(IServiceCollection, IJsonSerializationOptions?)"/>
	public static IServiceCollection AddJsonSerializer<T>(this IServiceCollection services, IJsonSerializationOptions? options = null)
	{
		var factory = JsonSerializerFactory.Default;
		services.AddSingleton<IJsonSerializer<T>>(factory.GetSerializerInternal(options).Cast<T>());
		return services;
	}

	/// <summary>
	/// Returns a delegate for deserializing a <see cref="string"/> to <typeparamref name="T"/>.
	/// </summary>
	public static Func<string?, T> GetDeserialize<T>(this IJsonFormatterResolver options)
		=> json => JsonSerializer.Deserialize<T>(json, options);

	/// <summary>
	/// Returns a delegate for serializing <typeparamref name="T"/> to a <see cref="string"/>.
	/// </summary>
	public static Func<T, string?> GetSerialize<T>(this IJsonFormatterResolver options, bool indent = false)
		=> item =>
		{
			var result = JsonSerializer.ToJsonString(item, options);
			return indent ? JsonSerializer.PrettyPrint(result) : result;
		};

	/// <summary>
	/// Returns a delegate for serializing <see cref="object"/> to <see cref="string"/>.
	/// </summary>
	public static Func<object?, string?> GetSerialize(this IJsonFormatterResolver options, bool indent = false)
		=> item =>
		{
			var result = JsonSerializer.ToJsonString(item, options);
			return indent ? JsonSerializer.PrettyPrint(result) : result;
		};

	/// <summary>
	/// Returns an <see cref="IJsonSerializer"/> with the providied options.
	/// </summary>
	public static IJsonSerializer GetSerializer(this IJsonFormatterResolver options, bool indent = false)
		=> new JsonSerializerInternal(options, indent);

	/// <summary>
	/// Returns an <see cref="IJsonSerializer{T}"/> with the providied options.
	/// </summary>
	public static IJsonSerializer<T> GetSerializer<T>(this IJsonFormatterResolver options, bool indent = false)
		=> new JsonSerializerInternal(options, indent).Cast<T>();

	/// <summary>
	/// Returns an <see cref="IJsonSerializerFactory"/> with the providied options.
	/// </summary>
	public static IJsonSerializerFactory GetSerializerFactory(this IJsonFormatterResolver options)
		=> new JsonSerializerFactory(options);

	/// <summary>
	/// Serializes a <typeparamref name="TValue"/> to a <see cref="string"/> using the provided options.
	/// </summary>
	public static string? Serialize<TValue>(this IJsonFormatterResolver options, TValue value)
		=> JsonSerializer.ToJsonString(value, options);

	/// <summary>
	/// Serializes an <see cref="object"/> to a <see cref="string"/> using the provided options.
	/// </summary>
	public static string? Serialize(this IJsonFormatterResolver options, object? value)
		=> JsonSerializer.ToJsonString(value, options);

	/// <summary>
	/// Deserializes a <see cref="string"/> to <typeparamref name="TValue"/> using the provided options.
	/// </summary>
	public static TValue Deserialize<TValue>(this IJsonFormatterResolver options, string? value)
		=> JsonSerializer.Deserialize<TValue>(value, options);
}
