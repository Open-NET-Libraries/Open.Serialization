using System;
using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System.Converters;

public class JsonNullableDoubleConverter : JsonConverter<double?>
{
	protected JsonNullableDoubleConverter()
	{
		// Prevent unnecessary replication.
	}

	public static readonly JsonNullableDoubleConverter Instance
		= new();

	public override bool CanConvert(Type objectType)
		=> objectType == typeof(double?);

	public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.TokenType switch
		{
			JsonTokenType.Null => default,
			JsonTokenType.Number => reader.GetDouble(),
			_ => throw new JsonException("Unexpected token type."),
		};

	public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		if (value.HasValue) writer.WriteNumberValue(value.Value);
		else writer.WriteNullValue();
	}
}
