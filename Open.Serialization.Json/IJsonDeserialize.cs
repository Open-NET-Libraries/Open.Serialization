namespace Open.Serialization.Json;

/// <inheritdoc />
public interface IJsonDeserialize : IDeserialize
{
}

/// <inheritdoc />
public interface IJsonDeserialize<out T> : IDeserialize<T>
{
}
