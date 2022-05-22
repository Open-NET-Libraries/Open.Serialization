using System;
using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System.Converters;

/// <summary>
/// Converter for nullable decimals.
/// </summary>
public class JsonNullableDecimalConverter : JsonConverter<decimal?>
{
	/// <summary>
	/// Constructs a <see cref="JsonNullableDecimalConverter"/>.
	/// </summary>
	protected JsonNullableDecimalConverter()
	{
		// Prevent unnecessary replication.
	}

	/// <summary>
	/// The shared instance of this converter.
	/// </summary>
	public static readonly JsonNullableDecimalConverter Instance
		= new();

	/// <inheritdoc />
	public override bool CanConvert(Type objectType)
		=> objectType == typeof(decimal?) || objectType == typeof(decimal);

	/// <inheritdoc />
	public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.TokenType switch
		{
			JsonTokenType.Null => null,
			JsonTokenType.Number => reader.GetDecimal(),
			_ => throw new JsonException("Unexpected token type."),
		};

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		if (value.HasValue) JsonDecimalConverter.Instance.Write(writer, value.Value, options);
		else writer.WriteNullValue();
	}
}
