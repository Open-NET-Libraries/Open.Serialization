namespace Open.Serialization.Json
{
	/// <summary>
	/// Factory for generating a JSON generic serializer/deserializer.
	/// </summary>
	public interface IJsonSerializerFactory
	{
		/// <summary>
		/// Returns the requested serializer
		/// </summary>
		IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false);
	}
}
