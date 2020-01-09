namespace Open.Serialization.Json
{
	// Provided as a means of specificity when setting up DI.

	/// <inheritdoc />
	public interface IJsonObjectSerializer : IObjectSerializer, IJsonSerializeObject, IJsonDeserializeObject, IJsonDeserializeObjectAsync, IJsonSerializeObjectAsync
	{
	}
}
