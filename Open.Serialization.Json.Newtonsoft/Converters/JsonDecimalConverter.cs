using Newtonsoft.Json;

namespace Open.Serialization.Json.Newtonsoft.Converters
{
	public class JsonDecimalConverter : JsonValueConverterBase<decimal>
	{
		protected JsonDecimalConverter()
		{
			// Prevent unnecessary replication.
		}

		public static readonly JsonDecimalConverter Instance
			= new JsonDecimalConverter();

		public static string Normalize(decimal? value)
			=> Normalize(value?.ToString());

		public static string Normalize(string decimalString)
			=> decimalString?.IndexOf('.') == -1 ? decimalString
				: decimalString?.TrimEnd('0').TrimEnd('.');

		public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
			=> writer.WriteRawValue(Normalize(value));			
	}
}
