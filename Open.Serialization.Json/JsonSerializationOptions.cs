namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public class JsonSerializationOptions : IJsonSerializationOptions
	{
		/// <inheritdoc />
		public bool? CamelCaseProperties { get; set; }

		/// <inheritdoc />
		public bool? CamelCaseKeys { get; set; }

		/// <inheritdoc />
		public bool? OmitNull { get; set; }

		/// <inheritdoc />
		public bool? Indent { get; set; }
	}
}
