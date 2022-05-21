using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;

namespace Open.Serialization.Json.Newtonsoft.Converters;

/// <summary>
/// A converter for ensuring proper standardized processing of decimals.
/// </summary>
public class JsonDecimalConverter : JsonValueConverterBase<decimal>
{
	/// <summary>
	/// Constructs a <see cref="JsonDecimalConverter"/>.
	/// </summary>
	protected JsonDecimalConverter()
	{
		// Prevent unnecessary replication.
	}

	/// <summary>
	/// Shared instance of this converter.
	/// </summary>
	public static readonly JsonDecimalConverter Instance
		= new();

	/// <inheritdoc cref="Normalize(string?)"/>
#if NETSTANDARD2_1
	[return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("value")]
#endif
	public static string? Normalize(decimal? value)
		=> Normalize(value?.ToString());

	/// <summary>
	/// Normalizes a decimal by removing any unnecessary trailing zeros or decimal.
	/// </summary>
#if NETSTANDARD2_1
	[return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("decimalString")]
#endif
	public static string? Normalize(string? decimalString)
		=> string.IsNullOrEmpty(decimalString) || decimalString!.IndexOf('.') == -1
			? decimalString
			: decimalString.AsSpan().TrimEnd('0').TrimEnd('.').ToString();

	/// <inheritdoc />
	public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader is null) throw new ArgumentNullException(nameof(reader));
		Contract.EndContractBlock();

		return ConvertToDecimal(reader.Value!);
	}

	/// <inheritdoc />
	public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		writer.WriteRawValue(Normalize(value));
	}
}
