namespace Open.Serialization.Json
{
	/// <summary>
	/// Factory for generating JSON generic serializers and deserializers.
	/// </summary>
	public interface IJsonSerializationFactory
	{
		/// <summary>
		/// Returns the requested deserializer
		/// </summary>
		IJsonDeserialize GetDeserializer(bool caseSensitive = false);

		/// <summary>
		/// Returns the requested deserializer
		/// </summary>
		IJsonDeserializeAsync GetAsyncDeserializer(bool caseSensitive = false);

		/// <summary>
		/// Returns the requested serializer
		/// </summary>
		IJsonSerialize GetSerializer(IJsonSerializationOptions? options = null);

		/// <summary>
		/// Returns the requested serializer
		/// </summary>
		IJsonSerializeAsync GetAsyncSerializer(IJsonSerializationOptions? options = null);
	}
}
