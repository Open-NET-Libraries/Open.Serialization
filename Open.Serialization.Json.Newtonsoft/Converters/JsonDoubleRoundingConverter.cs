using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonDoubleRoundingConverter : JsonValueConverterBase<double>
	{
		public int Maximum { get; }
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

			return reader.Value is decimal d
				? Convert.ToDouble(Math.Round(d, Maximum))
				: Math.Round(Convert.ToDouble(reader.Value), Maximum);
		}

		public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			Contract.EndContractBlock();

			writer.WriteValue(Math.Round(value, Maximum));
		}
	}
}