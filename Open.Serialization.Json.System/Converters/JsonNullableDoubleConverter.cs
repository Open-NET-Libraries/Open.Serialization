using System;
using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System.Converters;

/// <summary>
/// Converter for nullable doubles.
/// </summary>
public class JsonNullableDoubleConverter : JsonConverter<double?>
{
	/// <summary>
	/// Constructs a <see cref="JsonNullableDoubleConverter"/>.
	/// </summary>
	protected JsonNullableDoubleConverter()
	{
		// Prevent unnecessary replication.
	}

	/// <summary>
	/// The shared instance for this converter.
	/// </summary>
	public static readonly JsonNullableDoubleConverter Instance
		= new();

	/// <inheritdoc />
	public override bool CanConvert(Type objectType)
		=> objectType == typeof(double?);

	/// <inheritdoc />
	public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.TokenType switch
		{
			JsonTokenType.Null => null,
			JsonTokenType.Number => reader.GetDouble(),
			_ => throw new JsonException("Unexpected token type."),
		};

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		if (value.HasValue) writer.WriteNumberValue(value.Value);
		else writer.WriteNullValue();
	}
}
