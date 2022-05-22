using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace Open.Serialization.Json.Newtonsoft.Converters;

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

	public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader is null) throw new ArgumentNullException(nameof(reader));
		Contract.EndContractBlock();

		return reader.Value switch
		{
			double d => Math.Round(d, Maximum),
			decimal d => ConvertToDouble(Math.Round(d, Maximum)),
			sbyte i => i,
			byte i => i,
			short i => i,
			ushort i => i,
			int i => i,
			uint i => i,
			long i => i,
			ulong i => i,
			BigInteger i => (double)i,
			_ => Math.Round(ConvertToDouble(reader.Value!), Maximum),
		};
	}

	public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		writer.WriteValue(Math.Round(value, Maximum));
	}
}
