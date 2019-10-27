namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public class JsonSerialzationOptions : IJsonSerializationOptions
	{
		public bool CamelCaseProperties { get; set; }

		public bool CamelCaseKeys { get; set; }

		public bool OmitNull { get; set; }

		public bool Indent { get; set; }
	}
}
