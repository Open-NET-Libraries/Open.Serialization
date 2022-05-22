using System;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters;

/// <summary>
/// Converter for doubles that rounds to a maximum number of digits after the decimal.
/// </summary>
public class JsonDoubleRoundingConverter : JsonValueConverterBase<double>
{
	/// <summary>
	/// The maximum number of digits after the decimal.
	/// </summary>
	public int Maximum { get; }

	/// <summary>
	/// Constructs a <see cref="JsonDoubleRoundingConverter"/>.
	/// </summary>
	/// <param name="maximum">The maximum number of digits after the decimal.</param>
	/// <exception cref="ArgumentOutOfRangeException">If the maximum is less than zero.</exception>
	public JsonDoubleRoundingConverter(int maximum)
	{
		if (maximum < 0)
			throw new ArgumentOutOfRangeException(nameof(maximum), maximum, "Must be at least zero.");
		Maximum = maximum;
	}

	/// <inheritdoc />
	public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> Math.Round(reader.GetDouble(), Maximum);

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		writer.WriteNumberValue(Math.Round(value, Maximum));
	}
}
