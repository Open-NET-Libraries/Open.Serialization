namespace Open.Serialization.Json
{
	public interface IJsonDeserialize : IDeserialize
	{
	}

	public interface IJsonDeserialize<out T> : IDeserialize<T>
	{
	}
}
