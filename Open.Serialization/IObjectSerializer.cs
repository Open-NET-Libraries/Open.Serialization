namespace Open.Serialization
{
	interface IObjectSerializer : ISerializeObject, IDeserializeObject
	{
	}

	interface IAsyncObjectSerializer : ISerializeObjectAsync, IDeserializeObjectAsync
	{
	}
}
