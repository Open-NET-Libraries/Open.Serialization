using System;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters;

public class JsonDecimalConverter : JsonValueConverterBase<decimal>
{
	protected JsonDecimalConverter()
	{
		// Prevent unnecessary replication.
	}

	public static readonly JsonDecimalConverter Instance
		= new();

	public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.GetDecimal();

	public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		var v = value / 1M;
		var truncated = decimal.Truncate(v);
		writer.WriteNumberValue(truncated == v ? truncated : v / 1.000000000000000000000000000000000m);
	}
}
