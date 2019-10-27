using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System.Converters
{
	public class JsonNullableDecimalConverter : JsonConverter<decimal?>
	{
		protected JsonNullableDecimalConverter()
		{
			// Prevent unnecessary replication.
		}

		public static readonly JsonNullableDecimalConverter Instance
			= new JsonNullableDecimalConverter();

		public override bool CanConvert(Type objectType)
			=> objectType == typeof(decimal?) || objectType == typeof(decimal);

		public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> reader.TokenType switch
			{
				JsonTokenType.Null => default,
				JsonTokenType.Number => reader.GetDecimal(),
				_ => throw new JsonException("Unexpected token type."),
			};

		public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
		{
			if (value.HasValue) JsonDecimalConverter.Instance.Write(writer, value.Value, options);
			else writer.WriteNullValue();
		}
	}
}
