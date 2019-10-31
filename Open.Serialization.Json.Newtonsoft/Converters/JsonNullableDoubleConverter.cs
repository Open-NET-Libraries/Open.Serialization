using System;
using Newtonsoft.Json;

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
			=> reader.TokenType switch
			{
				JsonToken.Null => default,
				JsonToken.Undefined => default,
				_ => Convert.ToDouble(reader.Value)
			};

		public override void WriteJson(JsonWriter writer, double? value, JsonSerializer serializer)
		{
			if (value.HasValue) writer.WriteValue(value.Value);
			else writer.WriteRawValue(null);
		}
	}
}
