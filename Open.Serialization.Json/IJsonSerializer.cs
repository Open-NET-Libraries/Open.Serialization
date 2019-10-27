namespace Open.Serialization.Json
{
	// Provided as a means of specificity when setting up DI.

	public interface IJsonSerializer : ISerializer
	{
	}

	public interface IJsonSerializer<T> : ISerializer<T>
	{
	}
}
