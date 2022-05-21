namespace Open.Serialization;

/// <summary>
/// Interface for defining an object serializer.
/// </summary>
public interface IObjectSerializer : ISerializeObject, IDeserializeObject
{
}

/// <summary>
/// Interface for defining an asynchronous object serializer.
/// </summary>
public interface IAsyncObjectSerializer : ISerializeObjectAsync, IDeserializeObjectAsync
{
}
