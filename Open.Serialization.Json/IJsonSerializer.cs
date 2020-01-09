namespace Open.Serialization.Json
{
	// Provided as a means of specificity when setting up DI.

	/// <inheritdoc />
	public interface IJsonSerializer : ISerializer, IJsonSerialize, IJsonDeserialize, IJsonDeserializeAsync, IJsonSerializeAsync
	{
	}

	/// <inheritdoc />
	public interface IJsonSerializer<T> : ISerializer<T>, IJsonSerialize<T>, IJsonDeserialize<T>, IJsonDeserializeAsync<T>, IJsonSerializeAsync<T>
	{
	}
}
