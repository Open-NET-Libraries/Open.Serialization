namespace Open.Serialization.Json
{
	public abstract class JsonSerializerFactoryBase : IJsonSerializerFactory
	{
		public abstract IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false);
	}
}
