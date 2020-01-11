using Open.Serialization.Json.System.Converters;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System
{
	public static class SerializationsExtensions
	{
		public static Func<string?, T> GetDeserialize<T>(this JsonSerializerOptions options)
			=> json => JsonSerializer.Deserialize<T>(json, options);

		public static Func<T, string?> GetSerialize<T>(this JsonSerializerOptions options)
			=> item => JsonSerializer.Serialize(item, options);

		public static Func<object?, string?> GetSerialize(this JsonSerializerOptions options)
			=> item => JsonSerializer.Serialize(item, options);

		public static IJsonSerializer GetSerializer(this JsonSerializerOptions options)
			=> new JsonSerializerInternal(options);

		public static IJsonSerializer<T> GetSerializer<T>(this JsonSerializerOptions options)
			=> new JsonSerializerInternal(options).Cast<T>();

		public static IJsonSerializerFactory GetSerializerFactory(this JsonSerializerOptions options)
			=> new JsonSerializerFactory(options);

		public static string? Serialize<TValue>(this JsonSerializerOptions options, TValue value)
			=> JsonSerializer.Serialize(value, options);
		public static string? Serialize(this JsonSerializerOptions options, object? value)
			=> JsonSerializer.Serialize(value, options);
		public static TValue Deserialize<TValue>(this JsonSerializerOptions options, string? value)
			=> JsonSerializer.Deserialize<TValue>(value, options);
		public static TValue Deserialize<TValue>(this JsonSerializerOptions options, ReadOnlySpan<byte> value)
			=> JsonSerializer.Deserialize<TValue>(value, options);

		public static JsonSerializerOptions Clone(this JsonSerializerOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			var clone = new JsonSerializerOptions
			{
				AllowTrailingCommas = options.AllowTrailingCommas,
				DefaultBufferSize = options.DefaultBufferSize,
				DictionaryKeyPolicy = options.DictionaryKeyPolicy,
				Encoder = options.Encoder,
				IgnoreNullValues = options.IgnoreNullValues,
				IgnoreReadOnlyProperties = options.IgnoreReadOnlyProperties,
				MaxDepth = options.MaxDepth,
				PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive,
				PropertyNamingPolicy = options.PropertyNamingPolicy,
				ReadCommentHandling = options.ReadCommentHandling,
				WriteIndented = options.WriteIndented
			};

			foreach (var converter in options.Converters)
				clone.Converters.Add(converter);

			return clone;
		}

		public static JsonSerializerOptions SetPropertyNameCaseInsensitive(this JsonSerializerOptions options, bool value)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			options.PropertyNameCaseInsensitive = value;
			return options;
		}

		public static JsonSerializerOptions SetIgnoreNullValues(this JsonSerializerOptions options, bool value = true)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			options.IgnoreNullValues = value;
			return options;
		}

		public static JsonSerializerOptions UseUnsafeEncoding(this JsonSerializerOptions options)
		{
			if (options is null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
			return options;
		}

		public static JsonSerializerOptions AddConverter(this JsonSerializerOptions options, JsonConverter converter)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			options.Converters.Add(converter);
			return options;
		}

		static JsonSerializerOptions RoundDoublesCore(this JsonSerializerOptions options, int maxDecimals)
		{
			var existing = options.Converters.FirstOrDefault(c => c is JsonConverter<double>);
			if (existing == null)
				return options.AddConverter(new JsonDoubleRoundingConverter(maxDecimals));
			if (existing is JsonDoubleRoundingConverter e && e.Maximum == maxDecimals)
				return options;
			throw new InvalidOperationException("A specific double converter already exists.");
		}

		static JsonSerializerOptions RoundNullableDoublesCore(this JsonSerializerOptions options, int maxDecimals)
		{
			JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<double?>);
			if (existing is JsonNullableDoubleConverter && existing.GetType() == typeof(JsonNullableDoubleConverter))
			{
				options.Converters.Remove(existing);
				existing = null;
			}
			if (existing == null)
				return options.AddConverter(new JsonNullableDoubleRoundingConverter(maxDecimals));
			if (existing is JsonNullableDoubleRoundingConverter e && e.Maximum == maxDecimals)
				return options;
			throw new InvalidOperationException("A specific double converter already exists.");
		}

		public static JsonSerializerOptions RoundDoubles(this JsonSerializerOptions options, int maxDecimals)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			return options
				.RoundDoublesCore(maxDecimals)
				.RoundNullableDoublesCore(maxDecimals);
		}

		public static JsonSerializerOptions NormalizeDecimals(this JsonSerializerOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal>);
			var existingNullable = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);

			if (existing is JsonDecimalConverter && existingNullable is JsonNullableDecimalConverter)
				return options;

			if (existing == null && existingNullable == null)
				return options
					.AddConverter(JsonDecimalConverter.Instance)
					.AddConverter(JsonNullableDecimalConverter.Instance);

			if (existing != null)
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
			if (existing == null)
				return options.AddConverter(new JsonDecimalRoundingConverter(maxDecimals));
			if (existing is JsonDecimalRoundingConverter e && e.Maximum == maxDecimals)
				return options;
			throw new InvalidOperationException("A specific decimal converter already exists.");
		}

		static JsonSerializerOptions RoundNullableDecimalsCore(this JsonSerializerOptions options, int maxDecimals)
		{
			JsonConverter? existing = options.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);
			if (existing is JsonNullableDecimalConverter && existing.GetType() == typeof(JsonNullableDecimalConverter))
			{
				options.Converters.Remove(existing);
				existing = null;
			}
			if (existing == null)
				return options.AddConverter(new JsonNullableDecimalRoundingConverter(maxDecimals));
			if (existing is JsonNullableDecimalRoundingConverter e && e.Maximum == maxDecimals)
				return options;
			throw new InvalidOperationException("A specific Nullable<decimal> converter already exists.");
		}

		public static JsonSerializerOptions RoundDecimals(this JsonSerializerOptions options, int maxDecimals)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			Contract.EndContractBlock();

			return options
				.RoundDecimalsCore(maxDecimals)
				.RoundNullableDecimalsCore(maxDecimals);
		}
	}
}
