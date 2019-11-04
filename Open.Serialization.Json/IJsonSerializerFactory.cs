namespace Open.Serialization.Json
{
	public interface IJsonSerializerFactory
	{
		IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false);
	}
}
