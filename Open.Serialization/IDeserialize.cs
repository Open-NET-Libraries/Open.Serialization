namespace Open.Serialization
{
	public interface IDeserialize
	{
		T Deserialize<T>(string value);
	}

	public interface IDeserialize<out T>
	{
		T Deserialize(string value);
	}
}
