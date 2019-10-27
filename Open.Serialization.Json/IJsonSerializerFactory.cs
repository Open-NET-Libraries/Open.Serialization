namespace Open.Serialization.Json
{
	public interface IJsonSerializerFactory
	{
		IDeserialize GetDeserializer(bool caseSensitive = false);
		IDeserializeAsync GetAsyncDeserializer(bool caseSensitive = false);

		ISerialize GetSerializer(IJsonSerializationOptions options = null);
		ISerializeAsync GetAsyncSerializer(IJsonSerializationOptions options = null);
	}
}
