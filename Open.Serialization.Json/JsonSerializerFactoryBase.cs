namespace Open.Serialization.Json
{
	public abstract class JsonSerializerFactoryBase : IJsonSerializerFactory
	{
		protected abstract SerializerBase GetDeserializerInternal(bool caseSensitive);

		public IDeserialize GetDeserializer(bool caseSensitive = false)
			=> GetDeserializerInternal(caseSensitive);

		public IDeserializeAsync GetAsyncDeserializer(bool caseSensitive = false)
			=> GetDeserializerInternal(caseSensitive);

		protected abstract SerializerBase GetSerializerInternal(IJsonSerializationOptions options);

		public ISerialize GetSerializer(IJsonSerializationOptions options = null)
			=> GetSerializerInternal(options);

		public ISerializeAsync GetAsyncSerializer(IJsonSerializationOptions options = null)
			=> GetSerializerInternal(options);
	}
}
