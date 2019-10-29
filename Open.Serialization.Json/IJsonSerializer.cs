namespace Open.Serialization.Json
{
	// Provided as a means of specificity when setting up DI.

	public interface IJsonSerializer : ISerializer, IJsonSerialize, IJsonDeserialize, IJsonDeserializeAsync, IJsonSerializeAsync
	{
	}

	public interface IJsonSerializer<T> : ISerializer<T>, IJsonSerialize<T>, IJsonDeserialize<T>, IJsonDeserializeAsync<T>, IJsonSerializeAsync<T>
	{
	}
}
