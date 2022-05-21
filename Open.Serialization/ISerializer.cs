namespace Open.Serialization;

/// <summary>
/// Interface for serializing any object.
/// </summary>
public interface ISerializer : ISerialize, IDeserialize
{
}

/// <summary>
/// Interface for serializing a specific type.
/// </summary>
/// <typeparam name="T">The type to serialize</typeparam>
public interface ISerializer<T> : ISerialize<T>, IDeserialize<T>
{
}

/// <summary>
/// Interface for serializing any object asyncronously.
/// </summary>
public interface IAsyncSerializer : ISerializeAsync, IDeserializeAsync
{
}

/// <summary>
/// Interface for serializing a specific type asyncronously.
/// </summary>
/// <typeparam name="T">The type to serialize</typeparam>
public interface IAsyncSerializer<T> : ISerializeAsync<T>, IDeserializeAsync<T>
{
}
