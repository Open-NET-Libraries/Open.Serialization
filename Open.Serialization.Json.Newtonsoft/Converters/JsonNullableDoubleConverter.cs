using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonNullableDoubleConverter : JsonValueConverterBase<double?>
	{
		protected JsonNullableDoubleConverter()
		{
			// Prevent unnecessary replication.
		}

		public static readonly JsonNullableDoubleConverter Instance
			= new JsonNullableDoubleConverter();


		public override double? ReadJson(JsonReader reader, Type objectType, double? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader is null) throw new ArgumentNullException(nameof(reader));
			Contract.EndContractBlock();

			return reader.TokenType switch
			{
				JsonToken.Null => default,
				JsonToken.Undefined => default,
				_ => Convert.ToDouble(reader.Value)
			};
		}

		public override void WriteJson(JsonWriter writer, double? value, JsonSerializer serializer)
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			Contract.EndContractBlock();

			if (value.HasValue) writer.WriteValue(value.Value);
			else writer.WriteRawValue(null);
		}
	}
}
