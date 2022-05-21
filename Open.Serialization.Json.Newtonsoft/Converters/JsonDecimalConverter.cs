using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace Open.Serialization.Json.Newtonsoft.Converters;

public class JsonDecimalConverter : JsonValueConverterBase<decimal>
{
	protected JsonDecimalConverter()
	{
		// Prevent unnecessary replication.
	}

	public static readonly JsonDecimalConverter Instance
		= new();

	public static string? Normalize(decimal? value)
		=> Normalize(value?.ToString());

	public static string? Normalize(string? decimalString)
		=> string.IsNullOrEmpty(decimalString) || decimalString!.IndexOf('.') == -1
			? decimalString
			: decimalString.AsSpan().TrimEnd('0').TrimEnd('.').ToString();

	public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader is null) throw new ArgumentNullException(nameof(reader));
		Contract.EndContractBlock();

		return ConvertToDecimal(reader.Value);
	}

	public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		writer.WriteRawValue(Normalize(value));
	}
}
