using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Open.Serialization.Json.Newtonsoft.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Open.Serialization.Json.Newtonsoft;

/// <summary>
/// Extensions for Newtonsof.Json serialization with Open.Serialization.Json.
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

	public static Func<string?, T> GetDeserialize<T>(this JsonSerializerSettings settings)
		=> json => JsonConvert.DeserializeObject<T>(json!, settings)!;

	public static Func<T, string?> GetSerialize<T>(this JsonSerializerSettings settings)
		=> item => JsonConvert.SerializeObject(item, settings);

	public static Func<object?, string?> GetSerialize(this JsonSerializerSettings settings)
		=> item => JsonConvert.SerializeObject(item, settings);

	public static IJsonSerializer GetSerializer(this JsonSerializerSettings settings)
		=> new JsonSerializerInternal(settings);

	public static IJsonSerializer<T> GetSerializer<T>(this JsonSerializerSettings settings)
		=> new JsonSerializerInternal(settings).Cast<T>();

	public static IJsonSerializerFactory GetSerializerFactory(this JsonSerializerSettings settings)
		=> new JsonSerializerFactory(settings);

	public static string? Serialize<TValue>(this JsonSerializerSettings settings, TValue value)
		=> JsonConvert.SerializeObject(value, settings);

	public static string? Serialize(this JsonSerializerSettings settings, object? value)
		=> JsonConvert.SerializeObject(value, settings);

	public static TValue Deserialize<TValue>(this JsonSerializerSettings settings, string? value)
		=> JsonConvert.DeserializeObject<TValue>(value!, settings)!;

	public static JsonSerializerSettings Clone(this JsonSerializerSettings settings)
	{
		if (settings is null) throw new ArgumentNullException(nameof(settings));
		Contract.EndContractBlock();

		var clone = new JsonSerializerSettings
		{
			StringEscapeHandling = settings.StringEscapeHandling,
			FloatParseHandling = settings.FloatParseHandling,
			FloatFormatHandling = settings.FloatFormatHandling,
			DateParseHandling = settings.DateParseHandling,
			DateTimeZoneHandling = settings.DateTimeZoneHandling,
			DateFormatHandling = settings.DateFormatHandling,
			Formatting = settings.Formatting,
			MaxDepth = settings.MaxDepth,
			DateFormatString = settings.DateFormatString,
			Context = settings.Context,
			Error = settings.Error,
			SerializationBinder = settings.SerializationBinder,
			TraceWriter = settings.TraceWriter,
			Culture = settings.Culture,
			ReferenceResolverProvider = settings.ReferenceResolverProvider,
			EqualityComparer = settings.EqualityComparer,
			ContractResolver = settings.ContractResolver,
			ConstructorHandling = settings.ConstructorHandling,
			TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling,
			MetadataPropertyHandling = settings.MetadataPropertyHandling,
			TypeNameHandling = settings.TypeNameHandling,
			PreserveReferencesHandling = settings.PreserveReferencesHandling,
			DefaultValueHandling = settings.DefaultValueHandling,
			NullValueHandling = settings.NullValueHandling,
			ObjectCreationHandling = settings.ObjectCreationHandling,
			MissingMemberHandling = settings.MissingMemberHandling,
			ReferenceLoopHandling = settings.ReferenceLoopHandling,
			CheckAdditionalContent = settings.CheckAdditionalContent
		};

		settings.Converters ??= new List<JsonConverter>();
		foreach (var converter in settings.Converters)
			clone.Converters.Add(converter);

		return clone;
	}

	public static JsonSerializerSettings SetNullValueHandling(this JsonSerializerSettings settings, NullValueHandling value)
	{
		if (settings is null) throw new ArgumentNullException(nameof(settings));
		Contract.EndContractBlock();

		settings.NullValueHandling = value;
		return settings;
	}

	public static JsonSerializerSettings AddConverter(this JsonSerializerSettings settings, JsonConverter converter)
	{
		if (settings is null) throw new ArgumentNullException(nameof(settings));
		Contract.EndContractBlock();

		settings.Converters.Add(converter);
		return settings;
	}

	static JsonSerializerSettings RoundDoublesCore(this JsonSerializerSettings settings, int maxDecimals)
		=> settings.Converters.FirstOrDefault(c => c is JsonConverter<double>) switch
		{
			null => settings.AddConverter(new JsonDoubleRoundingConverter(maxDecimals)),
			JsonDoubleRoundingConverter e when e.Maximum == maxDecimals => settings,
			_ => throw new InvalidOperationException("A specific double converter already exists.")
		};

	static JsonSerializerSettings RoundNullableDoublesCore(this JsonSerializerSettings settings, int maxDecimals)
	{
		JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<double?>);
		if (existing is JsonNullableDoubleConverter && existing.GetType() == typeof(JsonNullableDoubleConverter))
		{
			settings.Converters.Remove(existing);
			existing = null;
		}

		return existing switch
		{
			null => settings.AddConverter(new JsonNullableDoubleRoundingConverter(maxDecimals)),
			JsonNullableDoubleRoundingConverter e when e.Maximum == maxDecimals => settings,
			_ => throw new InvalidOperationException("A specific double converter already exists.")
		};
	}

	public static JsonSerializerSettings RoundDoubles(this JsonSerializerSettings settings, int maxDecimals)
	{
		if (settings is null) throw new ArgumentNullException(nameof(settings));
		Contract.EndContractBlock();

		return settings
			.RoundDoublesCore(maxDecimals)
			.RoundNullableDoublesCore(maxDecimals);
	}

	public static JsonSerializerSettings NormalizeDecimals(this JsonSerializerSettings settings)
	{
		if (settings is null) throw new ArgumentNullException(nameof(settings));
		Contract.EndContractBlock();

		JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal>);
		var existingNullable = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);

		if (existing is JsonDecimalConverter && existingNullable is JsonNullableDecimalConverter)
			return settings;

		if (existing is null && existingNullable is null)
		{
			return settings
				.AddConverter(JsonDecimalConverter.Instance)
				.AddConverter(JsonNullableDecimalConverter.Instance);
		}

		if (existing is not null)
			throw new InvalidOperationException("A specific decimal converter already exists.");

		throw new InvalidOperationException("A specific Nullable<decimal> converter already exists.");
	}

	static JsonSerializerSettings RoundDecimalsCore(this JsonSerializerSettings settings, int maxDecimals)
	{
		JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal>);
		if (existing is JsonDecimalConverter && existing.GetType() == typeof(JsonDecimalConverter))
		{
			settings.Converters.Remove(existing);
			existing = null;
		}

		return existing switch
		{
			null => settings.AddConverter(new JsonDecimalRoundingConverter(maxDecimals)),
			JsonDecimalRoundingConverter e when e.Maximum == maxDecimals => settings,
			_ => throw new InvalidOperationException("A specific decimal converter already exists.")
		};
	}

	static JsonSerializerSettings RoundNullableDecimalsCore(this JsonSerializerSettings settings, int maxDecimals)
	{
		JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);
		if (existing is JsonNullableDecimalConverter && existing.GetType() == typeof(JsonNullableDecimalConverter))
		{
			settings.Converters.Remove(existing);
			existing = null;
		}

		return existing switch
		{
			null => settings.AddConverter(new JsonNullableDecimalRoundingConverter(maxDecimals)),
			JsonNullableDecimalRoundingConverter e when e.Maximum == maxDecimals => settings,
			_ => throw new InvalidOperationException("A specific Nullable<decimal> converter already exists.")
		};
	}

	public static JsonSerializerSettings RoundDecimals(this JsonSerializerSettings settings, int maxDecimals)
	{
		if (settings is null) throw new ArgumentNullException(nameof(settings));
		Contract.EndContractBlock();

		return settings
			.RoundDecimalsCore(maxDecimals)
			.RoundNullableDecimalsCore(maxDecimals);
	}
}
