namespace Open.Serialization.Json
{
	/// <summary>
	/// Factory for generating JSON serializers and deserializers.
	/// </summary>
	public interface IJsonObjectSerializationFactory
	{
		/// <summary>
		/// Returns the requested deserializer
		/// </summary>
		IJsonDeserializeObject GetDeserializer(bool caseSensitive = false);

		/// <summary>
		/// Returns the requested deserializer
		/// </summary>
		IJsonDeserializeObjectAsync GetAsyncDeserializer(bool caseSensitive = false);

		/// <summary>
		/// Returns the requested serializer
		/// </summary>
		IJsonSerializeObject GetSerializer(IJsonSerializationOptions? options = null);

		/// <summary>
		/// Returns the requested serializer
		/// </summary>
		IJsonSerializeObjectAsync GetAsyncSerializer(IJsonSerializationOptions? options = null);
	}
}
