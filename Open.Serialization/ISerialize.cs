namespace Open.Serialization
{
	public interface ISerialize
	{
		string Serialize<T>(T item);
	}

	public interface ISerialize<T>
	{
		string Serialize(T item);
	}
}
