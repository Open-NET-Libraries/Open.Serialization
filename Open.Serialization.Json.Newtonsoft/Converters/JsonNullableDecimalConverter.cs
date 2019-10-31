using System;
using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonNullableDecimalConverter : JsonValueConverterBase<decimal?>
	{
		protected JsonNullableDecimalConverter()
		{
			// Prevent unnecessary replication.
		}

		public static readonly JsonNullableDecimalConverter Instance
			= new JsonNullableDecimalConverter();

		public override decimal? ReadJson(JsonReader reader, Type objectType, decimal? existingValue, bool hasExistingValue, JsonSerializer serializer)
			=> reader.TokenType switch
			{
				JsonToken.Null => default,
				JsonToken.Float => Convert.ToDecimal(reader.Value),
				_ => throw new JsonException($"Unexpected token type: {reader.TokenType}, Actual: {reader.Value.GetType()}")
			};

	public override void WriteJson(JsonWriter writer, decimal? value, JsonSerializer serializer)
		=> writer.WriteRawValue(JsonDecimalConverter.Normalize(value));
	}
}
