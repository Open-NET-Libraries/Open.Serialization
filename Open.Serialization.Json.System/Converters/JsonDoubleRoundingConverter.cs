using System;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Open.Serialization.Json.System.Converters;

public class JsonDoubleRoundingConverter : JsonValueConverterBase<double>
{
	public int Maximum { get; }
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
