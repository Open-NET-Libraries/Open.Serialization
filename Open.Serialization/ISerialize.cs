namespace Open.Serialization
{
	public interface ISerialize
	{
		string? Serialize<T>(T item);
	}

	public interface ISerialize<in T>
	{
		string? Serialize(T item);
	}
}
