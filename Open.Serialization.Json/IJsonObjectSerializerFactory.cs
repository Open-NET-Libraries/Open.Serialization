namespace Open.Serialization.Json
{
	/// <summary>
	/// Factory for generating a JSON serializer/deserializer.
	/// </summary>
	public interface IJsonObjectSerializerFactory
	{
		/// <summary>
		/// Returns the requested serializer
		/// </summary>
		IJsonObjectSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false);
	}
}
