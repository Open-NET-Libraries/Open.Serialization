namespace Open.Serialization.Json;

/// <inheritdoc />
public interface IJsonSerializeAsync : ISerializeAsync
{
}

/// <inheritdoc />
public interface IJsonSerializeAsync<in T> : ISerializeAsync<T>
{
}
