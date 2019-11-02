using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Open.Serialization.Json.Newtonsoft.Converters;

namespace Open.Serialization.Json.Newtonsoft
{
	public static class Extensions
	{
		public static Func<string, T> GetDeserialize<T>(this JsonSerializerSettings settings)
			=> json => JsonConvert.DeserializeObject<T>(json, settings);

		public static Func<T, string> GetSerialize<T>(this JsonSerializerSettings settings)
			=> item => JsonConvert.SerializeObject(item, settings);

		public static Func<object, string> GetSerialize(this JsonSerializerSettings settings)
			=> item => JsonConvert.SerializeObject(item, settings);

		public static IJsonSerializer GetSerializer(this JsonSerializerSettings settings)
			=> new JsonSerializerInternal(settings);

		public static IJsonSerializer<T> GetSerializer<T>(this JsonSerializerSettings settings)
			=> new JsonSerializerInternal<T>(settings);

		public static IJsonSerializerFactory GetSerializerFactory(this JsonSerializerSettings settings)
			=> new JsonSerializerFactory(settings);

		public static string Serialize<TValue>(this JsonSerializerSettings settings, TValue value)
			=> JsonConvert.SerializeObject(value, settings);
		public static string Serialize(this JsonSerializerSettings settings, object value)
			=> JsonConvert.SerializeObject(value, settings);
		public static TValue Deserialize<TValue>(this JsonSerializerSettings settings, string value)
			=> JsonConvert.DeserializeObject<TValue>(value, settings);

		public static JsonSerializerSettings Clone(this JsonSerializerSettings settings)
		{
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
			settings.NullValueHandling = value;
			return settings;
		}

		public static JsonSerializerSettings AddConverter(this JsonSerializerSettings settings, JsonConverter converter)
		{
			settings.Converters.Add(converter);
			return settings;
		}

		static JsonSerializerSettings RoundDoublesCore(this JsonSerializerSettings settings, int maxDecimals)
		{
			var existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<double>);
			if (existing == null)
				return settings.AddConverter(new JsonDoubleRoundingConverter(maxDecimals));
			if (existing is JsonDoubleRoundingConverter e && e.Maximum == maxDecimals)
				return settings;
			throw new InvalidOperationException("A specific double converter already exists.");
		}

		static JsonSerializerSettings RoundNullableDoublesCore(this JsonSerializerSettings settings, int maxDecimals)
		{
			JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<double?>);
			if (existing is JsonNullableDoubleConverter && existing.GetType() == typeof(JsonNullableDoubleConverter))
			{
				settings.Converters.Remove(existing);
				existing = null;
			}
			if (existing == null)
				return settings.AddConverter(new JsonNullableDoubleRoundingConverter(maxDecimals));
			if (existing is JsonNullableDoubleRoundingConverter e && e.Maximum == maxDecimals)
				return settings;
			throw new InvalidOperationException("A specific double converter already exists.");
		}

		public static JsonSerializerSettings RoundDoubles(this JsonSerializerSettings settings, int maxDecimals)
			=> settings
				.RoundDoublesCore(maxDecimals)
				.RoundNullableDoublesCore(maxDecimals);

		public static JsonSerializerSettings NormalizeDecimals(this JsonSerializerSettings settings)
		{
			JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal>);
			var existingNullable = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);

			if (existing is JsonDecimalConverter && existingNullable is JsonNullableDecimalConverter)
				return settings;

			if (existing == null && existingNullable == null)
				return settings
					.AddConverter(JsonDecimalConverter.Instance)
					.AddConverter(JsonNullableDecimalConverter.Instance);

			if (existing != null)
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
			if (existing == null)
				return settings.AddConverter(new JsonDecimalRoundingConverter(maxDecimals));
			if (existing is JsonDecimalRoundingConverter e && e.Maximum == maxDecimals)
				return settings;
			throw new InvalidOperationException("A specific decimal converter already exists.");
		}

		static JsonSerializerSettings RoundNullableDecimalsCore(this JsonSerializerSettings settings, int maxDecimals)
		{
			JsonConverter? existing = settings.Converters.FirstOrDefault(c => c is JsonConverter<decimal?>);
			if (existing is JsonNullableDecimalConverter && existing.GetType() == typeof(JsonNullableDecimalConverter))
			{
				settings.Converters.Remove(existing);
				existing = null;
			}
			if (existing == null)
				return settings.AddConverter(new JsonNullableDecimalRoundingConverter(maxDecimals));
			if (existing is JsonNullableDecimalRoundingConverter e && e.Maximum == maxDecimals)
				return settings;
			throw new InvalidOperationException("A specific Nullable<decimal> converter already exists.");
		}

		public static JsonSerializerSettings RoundDecimals(this JsonSerializerSettings settings, int maxDecimals)
			=> settings
				.RoundDecimalsCore(maxDecimals)
				.RoundNullableDecimalsCore(maxDecimals);
	}
}
