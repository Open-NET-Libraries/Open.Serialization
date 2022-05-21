using Microsoft.Extensions.DependencyInjection;
using Open.Serialization.Json.System.Converters;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System;

/// <summary>
/// Extensions for System.Text.Json serialization with Open.Serialization.Json.
/// </summary>
public static class SerializationsExtensions
{
	/// <summary>
	/// Adds a generic serializer <see cref="IJsonSerializer"/>and non-generic <see cref="IJsonObjectSerializer"/> to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="options">The options overrides.</param>
	/// <param name="caseSensitive">Indicates whether the serializer should be case sensitive.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddJsonSerializer(this IServiceCollection services, IJsonSerializationOptions? options = null, bool caseSensitive = false)
	{
		var factory = JsonSerializerFactory.Default;
		services.AddSingleton(factory.GetSerializer(options, caseSensitive));
		return services;
	}

	/// <summary>
	/// Adds a generic serializer <see cref="IJsonSerializer"/>) to the service collection.
	/// </summary>
	/// <inheritdoc cref="AddJsonSerializer(IServiceCollection, IJsonSerializationOptions?, bool)" />
	public static IServiceCollection AddJsonSerializer<T>(this IServiceCollection services, IJsonSerializationOptions? options = null, bool caseSensitive = false)
	{
		var factory = JsonSerializerFactory.Default;
		services.AddSingleton<IJsonSerializer<T>>(factory.GetSerializerInternal(options, caseSensitive).Cast<T>());
		return services;
	}

	/// <summary>
	/// Returns a delegate that can deserialize <typeparamref name="T"/> with the given options.
	/// </summary>
	public static Func<string, T> GetDeserialize<T>(this JsonSerializerOptions options)
		=> json => JsonSerializer.Deserialize<T>(json, options)!;

	/// <summary>
	/// Returns a delegate that can serialze a <see cref="string"/> to <typeparamref name="T"/> with the given options.
	/// </summary>
	public static Func<T, string?> GetSerialize<T>(this JsonSerializerOptions options)
		=> item => JsonSerializer.Serialize(item, options)!;

	/// <summary>
	/// Returns a delegate that can serialze a <see cref="string"/> to an <see cref="object"/> with the given options.
	/// </summary>
	public static Func<object?, string?> GetSerialize(this JsonSerializerOptions options)
		=> item => JsonSerializer.Serialize(item, options);

	/// <summary>
	/// Returns a <see cref="IJsonSerializer"/> with the given options.
	/// </summary>
	public static IJsonSerializer GetSerializer(this JsonSerializerOptions options)
		=> new JsonSerializerInternal(options);

	/// <summary>
	/// Returns a <see cref="IJsonSerializer{T}"/> with the given options.
	/// </summary>
	public static IJsonSerializer<T> GetSerializer<T>(this JsonSerializerOptions options)
		=> new JsonSerializerInternal(options).Cast<T>();

	/// <summary>
	/// Returns a <see cref="IJsonSerializerFactory"/> with the given options.
	/// </summary>
	public static IJsonSerializerFactory GetSerializerFactory(this JsonSerializerOptions options)
		=> new JsonSerializerFactory(options);

	/// <summary>
	/// Serializes <paramref name="value"/> to a <see cref="string"/> using the given options.
	/// </summary>
	public static string? Serialize<TValue>(this JsonSerializerOptions options, TValue value)
		=> JsonSerializer.Serialize(value, options);

	/// <summary>
	/// Serializes <paramref name="value"/> to a <see cref="string"/> using the given options.
	/// </summary>
	public static string? Serialize(this JsonSerializerOptions options, object? value)
		=> JsonSerializer.Serialize(value, options);

	/// <summary>
	/// Deserializes <paramref name="value"/> to a <typeparamref name="TValue"/> using the given options.
	/// </summary>
	public static TValue Deserialize<TValue>(this JsonSerializerOptions options, string value)
		=> JsonSerializer.Deserialize<TValue>(value, options)!;

	/// <summary>
	/// Deserializes <paramref name="value"/> to a <typeparamref name="TValue"/> using the given options.
	/// </summary>
	public static TValue Deserialize<TValue>(this JsonSerializerOptions options, ReadOnlySpan<byte> value)
		=> JsonSerializer.Deserialize<TValue>(value, options)!;

	/// <summary>
	/// Returns a shallow copy of the options.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions Clone(this JsonSerializerOptions options)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		var clone = new JsonSerializerOptions
		{
			AllowTrailingCommas = options.AllowTrailingCommas,
			DefaultBufferSize = options.DefaultBufferSize,
			DictionaryKeyPolicy = options.DictionaryKeyPolicy,
			Encoder = options.Encoder,
			IgnoreNullValues = options.IgnoreNullValues,
			IgnoreReadOnlyFields = options.IgnoreReadOnlyFields,
			IgnoreReadOnlyProperties = options.IgnoreReadOnlyProperties,
			IncludeFields = options.IncludeFields,
			MaxDepth = options.MaxDepth,
			NumberHandling = options.NumberHandling,
			PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive,
			PropertyNamingPolicy = options.PropertyNamingPolicy,
			ReadCommentHandling = options.ReadCommentHandling,
			WriteIndented = options.WriteIndented,
			DefaultIgnoreCondition = options.DefaultIgnoreCondition,
			ReferenceHandler = options.ReferenceHandler
		};

		foreach (var converter in options.Converters)
			clone.Converters.Add(converter);

		return clone;
	}

	/// <summary>
	/// Sets the <see cref="JsonSerializerOptions.PropertyNameCaseInsensitive"/> value.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions SetPropertyNameCaseInsensitive(this JsonSerializerOptions options, bool value)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		options.PropertyNameCaseInsensitive = value;
		return options;
	}

	/// <summary>
	/// <para>Sets the <see cref="JsonSerializerOptions.DefaultIgnoreCondition"/> value.</para>
	/// A <paramref name="value"/> of <see langword="true"/> sets the condtion to <see cref="JsonIgnoreCondition.WhenWritingNull"/>.<br/>
	/// A <paramref name="value"/> of <see langword="false"/> sets the condtion to <see cref="JsonIgnoreCondition.Never"/>.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions SetIgnoreNullValues(this JsonSerializerOptions options, bool value)
		=> SetIgnoreNullValues(options, value ? JsonIgnoreCondition.WhenWritingNull : JsonIgnoreCondition.Never);

	/// <summary>
	/// Sets the <see cref="JsonSerializerOptions.DefaultIgnoreCondition"/> value, defaulting to ignore nulls.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions SetIgnoreNullValues(this JsonSerializerOptions options, JsonIgnoreCondition value = JsonIgnoreCondition.WhenWritingNull)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		options.DefaultIgnoreCondition = value;
		return options;
	}

	/// <summary>
	/// Sets the <see cref="JsonSerializerOptions.Encoder"/> value to <see cref="JavaScriptEncoder.UnsafeRelaxedJsonEscaping"/>.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions UseUnsafeEncoding(this JsonSerializerOptions options)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
		return options;
	}

	/// <summary>
	/// Adds a converter to the options.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions AddConverter(this JsonSerializerOptions options, JsonConverter converter)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		options.Converters.Add(converter);
		return options;
	}

	static JsonSerializerOptions RoundDoublesCore(this JsonSerializerOptions options, int maxDecimals)
		=> options.Converters.FirstOrDefault(c => c is JsonConverter<double>) switch
		{
			null => options.AddConverter(new JsonDoubleRoundingConverter(maxDecimals)),
			JsonDoubleRoundingConverter e when e.Maximum == maxDecimals => options,
			_ => throw new InvalidOperationException("A specific double converter already exists.")
		};

	static JsonSerializerOptions RoundNullableDoublesCore(this JsonSerializerOptions options, int maxDecimals)
	{
		JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<double?>);
		if (existing is JsonNullableDoubleConverter && existing.GetType() == typeof(JsonNullableDoubleConverter))
		{
			options.Converters.Remove(existing);
			existing = null;
		}

		return existing switch
		{
			null => options.AddConverter(new JsonNullableDoubleRoundingConverter(maxDecimals)),
			JsonNullableDoubleRoundingConverter e when e.Maximum == maxDecimals => options,
			_ => throw new InvalidOperationException("A specific double converter already exists.")
		};
	}

	/// <summary>
	/// Adds a special converter that rounds double values to the <paramref name="maxDecimals"/> level.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions RoundDoubles(this JsonSerializerOptions options, int maxDecimals)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		return options
			.RoundDoublesCore(maxDecimals)
			.RoundNullableDoublesCore(maxDecimals);
	}

	/// <summary>
	/// Adds a special converter that normalizes decimals so they are consistent regardless of trailing zeros.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions NormalizeDecimals(this JsonSerializerOptions options)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal>);
		var existingNullable = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);

		if (existing is JsonDecimalConverter && existingNullable is JsonNullableDecimalConverter)
			return options;

		if (existing is null && existingNullable is null)
		{
			return options
				.AddConverter(JsonDecimalConverter.Instance)
				.AddConverter(JsonNullableDecimalConverter.Instance);
		}

		if (existing is not null)
			throw new InvalidOperationException("A specific decimal converter already exists.");

		throw new InvalidOperationException("A specific Nullable<decimal> converter already exists.");
	}

	static JsonSerializerOptions RoundDecimalsCore(this JsonSerializerOptions options, int maxDecimals)
	{
		JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal>);
		if (existing is JsonDecimalConverter && existing.GetType() == typeof(JsonDecimalConverter))
		{
			options.Converters.Remove(existing);
			existing = null;
		}

		return existing switch
		{
			null => options.AddConverter(new JsonDecimalRoundingConverter(maxDecimals)),
			JsonDecimalRoundingConverter e when e.Maximum == maxDecimals => options,
			_ => throw new InvalidOperationException("A specific decimal converter already exists.")
		};
	}

	static JsonSerializerOptions RoundNullableDecimalsCore(this JsonSerializerOptions options, int maxDecimals)
	{
		JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);
		if (existing is JsonNullableDecimalConverter && existing.GetType() == typeof(JsonNullableDecimalConverter))
		{
			options.Converters.Remove(existing);
			existing = null;
		}

		return existing switch
		{
			null => options.AddConverter(new JsonNullableDecimalRoundingConverter(maxDecimals)),
			JsonNullableDecimalRoundingConverter e when e.Maximum == maxDecimals => options,
			_ => throw new InvalidOperationException("A specific Nullable<decimal> converter already exists.")
		};
	}

	/// <summary>
	/// Adds a special converter that rounds decimal values to the <paramref name="maxDecimals"/> level.
	/// </summary>
	/// <exception cref="ArgumentNullException">If <paramref name="options"/> is null.</exception>
	public static JsonSerializerOptions RoundDecimals(this JsonSerializerOptions options, int maxDecimals)
	{
		if (options is null) throw new ArgumentNullException(nameof(options));
		Contract.EndContractBlock();

		return options
			.RoundDecimalsCore(maxDecimals)
			.RoundNullableDecimalsCore(maxDecimals);
	}
}
