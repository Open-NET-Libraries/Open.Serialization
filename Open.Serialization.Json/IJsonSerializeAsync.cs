namespace Open.Serialization.Json
{
	public interface IJsonSerializeAsync : ISerializeAsync
	{
	}

	public interface IJsonSerializeAsync<in T> : ISerializeAsync<T>
	{
	}
}
