namespace Open.Serialization.Json
{
	public abstract class JsonSerializerFactoryBase : IJsonSerializerFactory
	{
		protected abstract JsonSerializerBase GetDeserializerInternal(bool caseSensitive);

		public IJsonDeserialize GetDeserializer(bool caseSensitive = false)
			=> GetDeserializerInternal(caseSensitive);

		public IJsonDeserializeAsync GetAsyncDeserializer(bool caseSensitive = false)
			=> GetDeserializerInternal(caseSensitive);

		protected abstract JsonSerializerBase GetSerializerInternal(IJsonSerializationOptions options);

		public IJsonSerialize GetSerializer(IJsonSerializationOptions options = null)
			=> GetSerializerInternal(options);

		public IJsonSerializeAsync GetAsyncSerializer(IJsonSerializationOptions options = null)
			=> GetSerializerInternal(options);
	}
}
