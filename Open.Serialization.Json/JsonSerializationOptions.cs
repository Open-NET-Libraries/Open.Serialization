namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public class JsonSerializationOptions : IJsonSerializationOptions
	{
		public bool? CamelCaseProperties { get; set; }

		public bool? CamelCaseKeys { get; set; }

		public bool? OmitNull { get; set; }

		public bool? Indent { get; set; }
	}
}
