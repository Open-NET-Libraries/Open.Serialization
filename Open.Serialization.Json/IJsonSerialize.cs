namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public interface IJsonSerialize : ISerialize
	{
	}

	/// <inheritdoc />
	public interface IJsonSerialize<in T> : ISerialize<T>
	{
	}
}
