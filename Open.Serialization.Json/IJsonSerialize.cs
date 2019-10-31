namespace Open.Serialization.Json
{
	public interface IJsonSerialize : ISerialize
	{
	}

	public interface IJsonSerialize<in T> : ISerialize<T>
	{
	}
}
